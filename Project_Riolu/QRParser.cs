using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using com.google.zxing.qrcode;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode.decoder;
using com.google.zxing.qrcode.detector;

using MC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace Project_Riolu
{
    public class QRParser
    {
        private string sID, tID, cookie;


        public QRParser()
        {
            sID = "";
            tID = "";
            cookie = "";
        }

        public void printByteArray(byte[] b)
        {
            if (b == null) return;
           
            //Console.WriteLine(ByteArrayExtensions.ToHexString(b));
            foreach(byte B in b)
            {
                Console.Write(Convert.ToString(B,16)+" ");
                Console.WriteLine(Convert.ToString(B, 2));
            }
        }

        //Get QR from HTTP requests.

        public Image getQRData()
        {
            /*Console.WriteLine("New QR Request");
            Console.WriteLine("Save ID = " + sID);
            Console.WriteLine("Team ID = " + tID);
            Console.WriteLine("Cookie = " + cookie);*/

            byte[] data = Encoding.ASCII.GetBytes("savedataId=" + sID + "&battleTeamCd=" + tID);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://3ds.pokemon-gl.com/frontendApi/battleTeam/getQr");
            request.Method = "POST";
            request.Accept = "*/*";
            request.Headers["Accept-Encoding"] = "gzip, deflate, br";
            request.Headers["Accept-Language"] = "en-US,en;q=0.8";
            request.KeepAlive = true;
            request.ContentLength = 73;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Cookie"] = cookie;
            request.Host = "3ds.pokemon-gl.com";
            request.Headers["Origin"] = "https://3ds.pokemon-gl.com/";
            request.Referer = "https://3ds.pokemon-gl.com/rentalteam/" + tID;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //add failing validation.
                    try
                    {
                        return Image.FromStream(stream);
                    }catch(Exception e)
                    {
                        //invalid QR
                        return null;
                    }
                   
                }
            }
        }

        public byte[] parseQR(Image q)
        {
            Bitmap bitmap = new Bitmap(q);
            var img = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
            var hybrid = new HybridBinarizer(img);
            BinaryBitmap binaryMap = new BinaryBitmap(hybrid);
            var reader = new QRCodeReader().decode(binaryMap, null);
            byte[] data = Array.ConvertAll(reader.RawBytes, (a) => (byte)(a));
            return data;        
        }

        public byte[] shiftArray(byte[] b)
        {
            byte[] array = new byte[507];
            byte lb = 0;
            byte rb = 0;
            for(int i=0;i<array.Length;i++)
            {
                byte B = b[i];
                lb = (byte)((B & 0xF0)>>4);
                array[i] = (byte)(rb << 4 | lb); 
                rb = (byte)((B & 0xF));
            }

            return array;
        }

        public byte[] qr_t(byte[] qr)
        {
            byte[] aes_ctr_key = StringExtentions.ToByteArray("0F8E2F405EAE51504EDBA7B4E297005B");
            
            byte[] metadata_flags = new byte[0x8];
            byte[] ctr_aes = new byte[0x10];
            byte[] data = new byte[0x1CE];
            byte[] sha1 = new byte[0x8];

            Array.Copy(qr, 0, metadata_flags, 0, 0x8);
            Array.Copy(qr, 0x8, ctr_aes, 0, 0x10);
            Array.Copy(qr, 0x18, data, 0, 0x1CE);
            Array.Copy(qr, 0x1E6, sha1, 0, 0x8);

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(false, new ParametersWithIV(new KeyParameter(aes_ctr_key), ctr_aes));

            return cipher.ProcessBytes(data);
        }

        public RentalTeam decryptQRCode(Image QR)
        {
            //Read the bytes of the QR code
            byte[] data = parseQR(QR);

            //All data is shifted to the left by 4. Shift the data to the correct location.
            data = shiftArray(data);

            //Data Printout
            //printByteArray(data);
            //Console.WriteLine(data.Length);
            //File.WriteAllBytes("data.txt", data);

            //ZXing has added the header bytes to the raw bytes. These are the first 3, so skip them.
            var qrue = data.Skip(3).ToArray();

            //MEME CRYPTO!!! De-Meme the data
            var qrt = MemeCrypto.VerifyMemeData(qrue);
            if (qrt == null)
            {
                Console.WriteLine("it failed");
                return null;
            }
            else
            {
                //Console.WriteLine("\n##################\nMemeCrypto Solved. Congrats. \n##################\n\nData:");
                //printByteArray(qrt);

                //unencrypt the data in the plaintext. 
                byte[] qrDec = qr_t(qrt);
                //Console.WriteLine("Decrypting the QR data. Size = " + Convert.ToString(qrDec.Length, 16));
                //printByteArray(qrDec);

                //build the rental team.
                return new RentalTeam(qrDec);
            }
            //Console.WriteLine(string.Join(" ", qr_t));
        }


        public void setsID(string newID)
        {
            sID = newID;
        }

        public void settID(string newID)
        {
            tID = newID;
        }

        public void setCookie(string newCookie)
        {
            cookie = newCookie;
        }
    }
}
