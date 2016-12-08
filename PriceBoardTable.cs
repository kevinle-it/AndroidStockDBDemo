using System;
namespace StockDBDemo01
{
	public class PriceBoardTable
	{
		public string stockSymbol { get; set; }
		public int ceilingPrice { get; set; }
		public int floorPrice { get; set; }
		public int referencePrice { get; set; }
		public int price { get; set; }
		public int volume { get; set; }

		public PriceBoardTable(string stockSymbol, int ceilingPrice, int floorPrice, int referencePrice, int price, int volume)
		{
			this.stockSymbol = stockSymbol;
			this.ceilingPrice = ceilingPrice;
			this.floorPrice = floorPrice;
			this.referencePrice = referencePrice;
			this.price = price;
			this.volume = volume;
		}

		public PriceBoardTable()
		{
			this.stockSymbol = null;
			this.ceilingPrice = 0;
			this.floorPrice = 0;
			this.referencePrice = 0;
			this.price = 0;
			this.volume = 0;
		}
	}
}
