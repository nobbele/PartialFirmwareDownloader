# FirmwareDownloader
Download files from a iOS Firmware Archive using a modified version of [`PartialZip`](https://github.com/Neal/PartialZip) and [`SharpZipLib`](http://sharpziplib.com/).


## Usage

    FirmwareDownloader.exe <Device> <BuildID> <IPSWFilePath> <SavePath>

### Examples

    FirmwareDownloader iPhone4,1 13G36 Firmware/dfu/iBSS.n94.RELEASE.dfu iBSS.n94.RELEASE.dfu
	
	
## Developers

You can use the modified version of PartialZip by copying the SharpZipLib and PartialZip folder into your own project

### Usage

	using PartialZip;
	
	namespace PZip {
		class Program {
			public static void Main(string[] args) {
				PartialZip.DownloadFileFromZipURL(URLToZip, FilePathInZip, SaveLocation); // Returns True on success and False on error
			}
		}
	}
