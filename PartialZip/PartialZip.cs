using System;
using System.IO;
using System.Net;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using RemoteZip;

namespace PartialZipLib {
    public static class PartialZip
    {
		private static void CopyStream(Stream input, Stream output)
		{
			int num = 0;
			byte[] buffer = new byte[0x2000];
			while (InlineAssignHelper<int>(ref num, input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, num);
			}
		}

		private static T InlineAssignHelper<T>(ref T target, T value)
		{
			target = value;
			return value;
		}

		public static bool DownloadFileFromZipURL(string ZipURL, string FilePathInZip, string LocalPath)
		{
            if(!isURLValid(ZipURL)) return false;

			bool ret = false;
			RemoteZipFile file = new RemoteZipFile();
			if (file.Load(ZipURL))
			{
				try
				{
					IEnumerator enumerator = file.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ZipEntry current = (ZipEntry)enumerator.Current;
						if (current.Name == FilePathInZip)
						{
							FileStream output = new FileStream(LocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
							CopyStream(file.GetInputStream(current), output);
							output.Close();
							ret = true;
						}
					}
					if (enumerator is IDisposable) (enumerator as IDisposable).Dispose();
				}
				catch (Exception) { }
			}
			return ret;
		}

		public static bool isURLValid(string url)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Timeout = 5000;
				request.Method = "HEAD";
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				int statusCode = (int)response.StatusCode;
				return (statusCode >= 100 && statusCode < 400);
			}
			catch (WebException) { }
			catch (Exception) { }
			return false;
		}
	}
}
