using System;
using PartialZipLib;

namespace FirmwareDownloader {
    class Program {
        private static string GetIPSW(string device, string build) {
            return String.Format("http://api.ipsw.me/v2/{0}/{1}/url/dl", device, build);
        }

        public static void Main(string[] args) {
            if (args[3].Length == 0) args[3] = System.AppDomain.CurrentDomain.BaseDirectory;
            // If the amount of arguments is not exactly 4
            if (args.Length != 4) {
                Console.WriteLine("Error! need exactly 4 arguments\nUsage: {0} iPhone4,1 13G36 Firmware/dfu/iBSS.n94.RELEASE.dfu iBSS.n94.RELEASE.dfu", System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            } else {
                /*
                 * args[0] should be the device
                 * args[1] should be the build ID of the iOS version
                 * args[2] should be the file to grab
                 * args[3] should be the location where the file is going to be downloaded
                 * 
                 */
                bool result = PartialZip.DownloadFileFromZipURL(GetIPSW(args[0], args[1]), args[2], args[3]);
                Console.WriteLine("Downloading of {0} {1}", args[3], result ? "succeeded" : "did not succeed");
            }
            // For testing the application when changing code
            if(System.Diagnostics.Debugger.IsAttached) {
                bool result = PartialZip.DownloadFileFromZipURL(GetIPSW("iPhone4,1", "13G36"), "Firmware/dfu/iBSS.n94.RELEASE.dfu", "iBSS.n94.RELEASE.dfu");
                Console.WriteLine("Downloading of {0} {1}", "iBSS.n94.RELEASE.dfu", result ? "succeeded" : "did not succeed");
                Console.Read();
            }
        }
    }
}
