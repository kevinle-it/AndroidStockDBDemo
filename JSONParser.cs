using System;
using Newtonsoft.Json.Linq;

namespace StockDBDemo01
{
	public class JSONParser
	{
		public JSONParser()
		{
		}

		public PriceBoardTable parseStockDetails(JObject obj)
		{
			PriceBoardTable stockDetail = new PriceBoardTable();

			try
			{
				JObject jObj = (JObject) obj["Value"][0];
				stockDetail.ceilingPrice = (int) jObj["ceilingPrice"];
				stockDetail.floorPrice = (int) jObj["floorPrice"];
				stockDetail.referencePrice = (int) jObj["referencePrice"];
				stockDetail.price = (int) jObj["price"];
				stockDetail.volume = (int) jObj["volume"];
				//JSONObject jsonObj = obj.GetJSONArray("Value").GetJSONObject(0);
				//
				//stockDetail.ceilingPrice = jsonObj.GetInt("ceilingPrice");
				//stockDetail.floorPrice = jsonObj.GetInt("floorPrice");
				//stockDetail.referencePrice = jsonObj.GetInt("referencePrice");
				//stockDetail.price = jsonObj.GetInt("price");
				//stockDetail.volume = jsonObj.GetInt("volume");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return stockDetail;
		}
	}
}
