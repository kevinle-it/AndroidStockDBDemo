using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Newtonsoft.Json.Linq;

namespace StockDBDemo01
{
	[Activity(Label = "StockDBDemo01", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		//int count = 1;

		EditText stockSymbolEditText;
		Button showStockInfoBtn;
		TextView ceilingPrice, floorPrice, referencePrice, price, volume;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.myButton);

			//button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			stockSymbolEditText = FindViewById<EditText>(Resource.Id.stock_symbol);
			showStockInfoBtn = FindViewById<Button>(Resource.Id.show_stock_info_btn);
			ceilingPrice = FindViewById<TextView>(Resource.Id.ceiling_price);
			floorPrice = FindViewById<TextView>(Resource.Id.floor_price);
			referencePrice = FindViewById<TextView>(Resource.Id.reference_price);
			price = FindViewById<TextView>(Resource.Id.price);
			volume = FindViewById<TextView>(Resource.Id.volume);

			showStockInfoBtn.Click += showStockInfoBtn_Click;
		}

		public void showStockInfoBtn_Click(object o, EventArgs e)
		{
			string stockSymbol = stockSymbolEditText.Text;
			new GetStockInfoTask(this).Execute(stockSymbol);
		}

		public class GetStockInfoTask : AsyncTask<String, Java.Lang.Object, PriceBoardTable>
		{
			public string stockSymbol;
			public PriceBoardTable stockDetail = null;
			public MainActivity mainActivity;

			public GetStockInfoTask(MainActivity mainActivity)
			{
				this.mainActivity = mainActivity;
			}
			protected override void OnPreExecute()
			{
				base.OnPreExecute();
			}

			protected override PriceBoardTable RunInBackground(params string[] @params)
			{
				RestAPI api = new RestAPI();
				try
				{
					JObject jsonObj = api.GetStockDetails(@params[0]);
					JSONParser parser = new JSONParser();
					stockDetail = parser.parseStockDetails(jsonObj);
					Console.WriteLine(jsonObj);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				return stockDetail;
			}

			protected override void OnPostExecute(PriceBoardTable result)
			{
				base.OnPostExecute(result);

				mainActivity.ceilingPrice.Text = result.ceilingPrice.ToString();
				mainActivity.floorPrice.Text = result.floorPrice.ToString();
				mainActivity.referencePrice.Text = result.referencePrice.ToString();
				mainActivity.price.Text = result.price.ToString();
				mainActivity.volume.Text = result.volume.ToString();
			}
		}
	}
}

