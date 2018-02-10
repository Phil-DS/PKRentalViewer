# PKRentalViewer
A C# program to view Pokemon Rental Teams from the Global Link. This is done by reading and decrypting the Rental QR codes from the Global Link

### Via Clipboard
The easiest method. Just copy the QR code onto the clipboard and click "From Clipboard" in the program.

## Known Issues

* Some data validation is missing, and thus can crash at some points

## Dependencies

 * The AES-CTR uses [Bouncy Castle](http://www.bouncycastle.org/csharp/licence.html) to decrypt. This is licensed as stated [here](http://www.bouncycastle.org/csharp/licence.html)
 * The QR decoder is from [ZXing.Net](https://www.nuget.org/packages/ZXing.Net/). It is licensed under the [Apache 2.0 License](http://www.apache.org/licenses/LICENSE-2.0)
 * The MemeCrypto and the sprites came from [PKHex](https://github.com/kwsch/PKHeX). It is licensed under [GPL V3](https://github.com/kwsch/PKHeX/blob/master/LICENSE.md]

## Update History
10/02/2018 Version 2.0: USUM update. See release for change log.
22/08/2017 Version 1.1: Added Hidden Power type to the move outputs if Hidden Power is found. 
