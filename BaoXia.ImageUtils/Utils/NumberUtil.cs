namespace BaoXia.ImageUtils.Utils;

public class NumberUtil
{
	////////////////////////////////////////////////
	// @类方法
	////////////////////////////////////////////////

	#region 类方法

	public static uint UIntFromHexString(
		string? hexString,
		uint defaultValue = 0x0)
	{
		if (string.IsNullOrWhiteSpace(hexString))
		{
			return 0;
		}

		if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
		{
			hexString = hexString[2..];
		}

		if (hexString.Length > 8)
		{
			hexString = hexString.Substring(0, 8);
		}

		if (uint.TryParse(hexString,
			System.Globalization.NumberStyles.HexNumber,
			null,
			out uint hexNumber))
		{
			return hexNumber;
		}
		return defaultValue;
	}

	#endregion
}