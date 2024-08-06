using BaoXia.ImageUtils.Models;
using BaoXia.Utils.Cache;

namespace BaoXia.ImageUtils;

public class QRCodeUtil
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	class QRCodeImageCardCreateParam(int cardSize, QRCodeImageCard.DPIRatio dpiRatio)
	{
		public int CardSize { get; set; } = cardSize;

		public QRCodeImageCard.DPIRatio DPIRatio { get; set; } = dpiRatio;
	}

	#endregion


	////////////////////////////////////////////////
	// @静态变量
	////////////////////////////////////////////////

	#region 静态变量

	private static double _qrCodePoolCacheValidSeconds = 3600.0;

	private static readonly ItemsCache
		<string, ObjectPool<QRCodeImageCard>, QRCodeImageCardCreateParam>
		_qrCodeImageCardPoolCache
		= new(
		(_, createParam) =>
		{
			if (createParam == null)
			{
				return null;
			}

			var qrCodeCardPool = new ObjectPool<QRCodeImageCard>(() =>
			{
				var qrCodeImageCard = new QRCodeImageCard(
					null,
					createParam.CardSize,
					createParam.DPIRatio);
				return qrCodeImageCard;
			});
			return qrCodeCardPool;
		},
		null,
		() => _qrCodePoolCacheValidSeconds);

	#endregion


	////////////////////////////////////////////////
	// @类方法
	////////////////////////////////////////////////

	#region 类方法

	public static async Task<ImageCardInfo?> CreateQRCodeImageWithUrlAsync(
		string? url,
		QRCodeImageCard.DPIRatio qrCodeDpiRatio = QRCodeImageCard.DPIRatio.PC,
		int qrCodeSize = QRCodeImageCard.SizeDefault,
		uint backgroundColorARGBHex = 0xFFFFFFFF,
		QRCodeImageCard.ErrorCorrectionCodeLevel eccLevel = QRCodeImageCard.ErrorCorrectionCodeLevel.Low,
		uint codeColorARGBHex = 0xFF000000,
		string? fileDownloadName = null,
		string imageType = "png",
		int imageQuality = 100)
	{
		if (string.IsNullOrEmpty(url))
		{
			return null;
		}

		var qrCodePool = _qrCodeImageCardPoolCache.Get(
			qrCodeSize + "@" + (int)qrCodeDpiRatio + "x",
			new(qrCodeSize, qrCodeDpiRatio));
		if (qrCodePool == null)
		{
			return null;
		}
		using var _ = qrCodePool.GetObjectToUsing(out var qrCodeImageCard);
		if (qrCodeImageCard == null)
		{
			return null;
		}

		var imageCardInfo = await qrCodeImageCard.ToImageCardInfoWithTextAsync(
			url,
			backgroundColorARGBHex,
			eccLevel,
			codeColorARGBHex,
			imageType,
			imageQuality,
			fileDownloadName);
		{ }
		return imageCardInfo;
	}

	public static double GetQrCodePoolCacheValidSeconds()
	{
		return _qrCodePoolCacheValidSeconds;
	}

	public static void SetQrCodePoolCacheValidSeconds(double qrCodePoolCacheValidSeconds)
	{
		_qrCodePoolCacheValidSeconds = qrCodePoolCacheValidSeconds;
	}

	#endregion

}