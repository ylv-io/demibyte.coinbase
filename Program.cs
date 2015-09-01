using System;
using System.Text;
using System.Security.Cryptography;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace demibyte.coinbase
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var host = "https://api.coinbase.com/";
			var apiKey = "yourApiKey";
			var apiSecret = "youApiSecret";

			var unixTimestamp = (Int32)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
			var currency = "USD";
			var message = string.Format ("{0}GET/v2/prices/spot?currency={1}", unixTimestamp.ToString (), currency);

			byte[] secretKey = Encoding.UTF8.GetBytes (apiSecret);
			HMACSHA256 hmac = new HMACSHA256 (secretKey);
			hmac.Initialize ();
			byte[] bytes = Encoding.UTF8.GetBytes (message);
			byte[] rawHmac = hmac.ComputeHash (bytes);
			var signature = rawHmac.ByteArrayToHexString ();

			var price = host
				.AppendPathSegment ("v2/prices/spot")
				.SetQueryParam ("currency", currency)
				.WithHeader ("CB-ACCESS-SIGN", signature)
				.WithHeader ("CB-ACCESS-TIMESTAMP", unixTimestamp)
				.WithHeader ("CB-ACCESS-KEY", apiKey)
				.GetJsonAsync<dynamic> ()
				.Result;

			Console.WriteLine (price.ToString (Formatting.None));
			Console.ReadLine ();
		}
	}
}