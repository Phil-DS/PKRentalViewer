
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

class Export
{
    public static String exportToPokepaste(string title, string author, string notes, string data)
    {
        /*
         * url: 
         *  - title: string for Title
         *  - author: string for Author
         *  - notes: string for Notes
         *  - paste: the showdown paste
         */
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        byte[] url = Encoding.UTF8.GetBytes("title=" + HttpUtility.UrlPathEncode(title) + "&author=" + HttpUtility.UrlPathEncode(author) + "&notes=" + HttpUtility.UrlPathEncode(notes) + "&paste=" + HttpUtility.UrlPathEncode(data));

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pokepast.es/create");

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = url.Length;

        Console.WriteLine(url);

        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(url, 0, url.Length);
        }

        using (WebResponse response = request.GetResponse())
        {
            try
            {
                return response.ResponseUri.AbsoluteUri;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }
    }
}