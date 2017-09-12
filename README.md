# PKRentalViewer
A C# program to view Pokemon Rental Teams from the Global Link. This is done by reading and decrypting the Rental QR codes from the Global Link

## How to use

First, you must have a Pokemon Global Link account. If you do not, you will be unable to access the QR form of the rental teams.

1. Log into the Global Link
2. Go to the Rental Team you wish to get the spreads for.

### Via Clipboard
The easiest method.

3. Click the "Get QR code" Button.
4. Copy the QR code that pops up in the box.
5. Click "From Clipboard" in PK Rental Viewer

### Via HTTP
I would highly recommend against this one. It is fussy, and the Cookie doesn't last forever, which means you need to keep changing the cookie everytime. Though nice for very quick parsing.

3. Open up the developers tab in your web browser, usually by pressing F12, and navigate to "Network". 
4. Click the "Get QR code" Button.
5. You should see one of the requests made in network was "getQR". Click on this and make a note of: savedataId and Cookie. 
6. Input these onto your program.
7. Copy and paste the Battle team ID, which is found in the URL (rentalteam/BT-4535-BE45, and the battle team ID is BT-4535-BE45). 
8. Hit "Send request".

You ghould have the data all set. So far, this is only known to work in Chrome, but should work in other browsers.  

## Known Issues
 * Images for the Pokemon are not implemented as of yet
 * Some QR codes cause Reader errors.
 * Some form names might be incorrect for Showdown. If that happens, please inform me what is wrong, and I shall update.

## Dependencies

 * The AES-CTR uses [Bouncy Castle](http://www.bouncycastle.org/csharp/licence.html) to decrypt. This is licensed as stated [here](http://www.bouncycastle.org/csharp/licence.html)
 
 ## Other Libraries Used
 
 * The QR decoder is from [ZXing.Net](https://www.nuget.org/packages/ZXing.Net/). It is licensed under the [Apache 2.0 License](http://www.apache.org/licenses/LICENSE-2.0)
 * The MemeCrypto came from [PKHex](https://www.nuget.org/packages/ZXing.Net/). It is licensed under [GPL V3](https://github.com/kwsch/PKHeX/blob/master/LICENSE.md)

## Credits

* [@SciresM](https://twitter.com/sciresm?lang=en) for laying out the process for [Rental QR decoding.](https://gist.github.com/SciresM/f3d20f8c77f5514f2d142c9760939266)

## Update History
22/08/2017 Version 0.1: Added Hidden Power type to the move outputs if Hidden Power is found.
10/09/2017 Version 1.0: Added Forms and Abilities.
