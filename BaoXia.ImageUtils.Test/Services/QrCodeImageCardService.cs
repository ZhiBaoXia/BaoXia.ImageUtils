using BaoXia.Service.Attributes;
using BaoXia.Utils.Extensions;

namespace BaoXia.ImageUtils.Test.Services;

[Service]
public class QrCodeImageCardService
	()
	: BaoXia.ImageUtils.Services.QrCodeImageCardService
	(() =>
	{
		return new()
		{
			QrCodeCacheNoneReadSecondsToRemove = 1800.0,
			QrCodeDpiRatioName = QRCodeImageCard.DPIRatio.PC.Name()!,
			QrCodeSize = 400,
			BackgroundColorARGBHex = 0xFFFFFFFF,
			EccLevelName = QRCodeImageCard.ErrorCorrectionCodeLevel.High.Name()!,
			CodeColorARGBHex = 0xFF000000,
			FileDownloadName = "qrCodeImage",
			ImageType = "png",
			ImageQuality = 100
		};
	})
{ }