using BaoXia.ImageUtils;
using BaoXia.Utils.Extensions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaoXia.ImageUtils.QRCodeImageCard;

namespace BaoXia.ImageUtils.ViewModels;

public class QrCodeServiceConfig
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	public const double QrCodeCacheNoneReadSecondsToRemoveDefault = 1800.0;


	public const QRCodeImageCard.DPIRatio QrCodeDpiRatioDefault = QRCodeImageCard.DPIRatio.PC;

	public static readonly string QrCodeDpiRatioNameDefault = QrCodeDpiRatioDefault.Name()!;


	public const int QrCodeSizeDefault = QRCodeImageCard.SizeDefault;

	public const uint BackgroundColorARGBHexDefault = 0xFFFFFFFF;


	public const QRCodeImageCard.ErrorCorrectionCodeLevel EccLevelDefault = QRCodeImageCard.ErrorCorrectionCodeLevel.High;

	public static readonly string EccLevelNameDefault = EccLevelDefault.Name()!;



	public const uint CodeColorARGBHexDefault = 0xFF000000;

	public const string FileDownloadNameDefault = "qrCode";

	public const string ImageTypeDefault = "png";

	public const int ImageQualityDefault = 100;

	#endregion


	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	public double QrCodeCacheNoneReadSecondsToRemove { get; set; } = QrCodeCacheNoneReadSecondsToRemoveDefault;

	public string QrCodeDpiRatioName { get; set; } = QrCodeDpiRatioNameDefault;

	public QRCodeImageCard.DPIRatio QrCodeDpiRatio { get; set; } = QRCodeImageCard.DPIRatio.PC;

	public int QrCodeSize { get; set; } = QrCodeSizeDefault;

	public uint BackgroundColorARGBHex { get; set; } = BackgroundColorARGBHexDefault;

	public string EccLevelName { get; set; } = EccLevelNameDefault;

	public uint CodeColorARGBHex { get; set; } = CodeColorARGBHexDefault;

	public string FileDownloadName { get; set; } = FileDownloadNameDefault;

	public string ImageType { get; set; } = ImageTypeDefault;

	public int ImageQuality { get; set; } = ImageQualityDefault;

	#endregion
}