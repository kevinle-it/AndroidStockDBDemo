using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace StockDBDemo01
{
	public class RestAPI
	{
		private const string urlString = "http://lmtri.somee.com/Handler1.ashx";
		private const int bufferSize = 4096;
		private string Load(string contents)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlString);
			req.AllowWriteStreamBuffering = true;
			req.Method = "POST";
			req.Timeout = 60000;
			Stream outStream = req.GetRequestStream();
			StreamWriter outStreamWriter = new StreamWriter(outStream);
			outStreamWriter.Write(contents);
			outStreamWriter.Flush();
			outStream.Close();
			WebResponse res = req.GetResponse();
			Stream httpStream = res.GetResponseStream();
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				byte[] buff = new byte[bufferSize];
				int readedBytes = httpStream.Read(buff, 0, buff.Length);
				while (readedBytes > 0)
				{
					memoryStream.Write(buff, 0, readedBytes);
					readedBytes = httpStream.Read(buff, 0, buff.Length);
				}
			}
			finally
			{
				if (httpStream != null)
				{
					httpStream.Close();
				}

				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			byte[] data = memoryStream.ToArray();
			string result = Encoding.UTF8.GetString(data, 0, data.Length);
			return result;
		}

		public JObject GetStockDetails(string stockSymbol)
		{
			JObject result = null;
			JObject o = new JObject();
			JObject p = new JObject();
			o["interface"] = "RestAPI";
			o["method"] = "GetStockDetails";
			p["stockSymbol"] = JToken.FromObject(stockSymbol);
			o["parameters"] = p;
			string s = JsonConvert.SerializeObject(o);
			string r = Load(s);
			result = JObject.Parse(r);
			return result;
		}
	}
}
