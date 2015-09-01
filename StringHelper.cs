using System;
using System.Linq;
using System.Text;

namespace demibyte.coinbase
{
	public static class StringHelper
	{
		public static byte[] HexStringToByteArray (this string hex)
		{
			return Enumerable.Range (0, hex.Length)
				.Where (x => x % 2 == 0)
				.Select (x => Convert.ToByte (hex.Substring (x, 2), 16))
				.ToArray ();
		}

		public static string ByteArrayToHexString (this byte[] array)
		{
			StringBuilder hex = new StringBuilder (array.Length * 2);
			foreach (byte b in array)
				hex.AppendFormat ("{0:x2}", b);
			return hex.ToString ();
		}
	}
}