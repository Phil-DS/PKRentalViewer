# PKRentalViewer
A C# program to view Pokemon Rental Teams from the Global Link. This is done by reading and decrypting the Rental QR codes from the Global Link

## How to use
### Via HTTP
You first need to log in to the Global Link. Then, navigate to a Rental Team. Open up the developers screen in your web browser, usually by pressing F12, and navigate to "Network". Then, click the button on the rental team to bring up the QR code. From there, you should see one of the requests made was "getQR". Click on this and make a note of: savedataId and Cookie. Input these onto your program. Next, copy and paste the Battle team ID, which is found in the URL (rentalteam/BT-4535-BE45, and the battle team ID is BT-4535-BE45). So far, this is only known to work in Chrome, but should work in other browsers.  

### Via Clipboard
The easiest method. Just copy the QR code onto the clipboard and click "From Clipboard" in the program.

## Known Issues
 * Images for the Pokemon are not implemented as of yet
 * Abilities are labeled at 1,2,4 (These still need to be converted)
 * Forms are not implemented as of yet.
 * Some teams have really weird IVs, and sometimes do not match up with what it looks like in game.
 * Some data validation is missing, and thus can crash at some points

## Dependencies

 * The AES-CTR uses [Bouncy Castle](http://www.bouncycastle.org/csharp/licence.html) to decrypt. This is licensed as stated [here](http://www.bouncycastle.org/csharp/licence.html)
 * The QR decoder is from [ZXing.Net](https://www.nuget.org/packages/ZXing.Net/). It is licensed under the [Apache 2.0 License](http://www.apache.org/licenses/LICENSE-2.0)
 * The MemeCrypto came from [PKHex](https://www.nuget.org/packages/ZXing.Net/). It is licensed under [GPL V3](https://github.com/kwsch/PKHeX/blob/master/LICENSE.md]
