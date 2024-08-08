using BaoXia.ImageUtils.Models;
using BaoXia.Utils.Interfaces;
using SkiaSharp;
using SkiaSharp.QrCode;
using static BaoXia.ImageUtils.QRCodeImageCard;

namespace BaoXia.ImageUtils;
public class QRCodeImageCard(
	ILogFile? debugLogFile,
	int qrCodeSize = SizeDefault,
	DPIRatio canvasDPIZoomRatio = DPIRatio.Mobile,
	float canvasDPIDefault = ImageCard.CanvasDPIDefault,
	int canvasPaddingLeft = PaddingLeftDefault,
	int canvasPaddingTop = PaddingTopDefault,
	int canvasPaddingRight = PaddingRightDefault,
	int canvasPaddingBottom = PaddingBottomDefault,
	float textRenderOffsetY = ImageCard.TextRenderOffsetYDefault,
	double imageFileCacheValidSecondsDefault = ImageCard.ImageFileCacheValidSecondsDefault) : ImageCard(
		debugLogFile,
		null,
		qrCodeSize,
		qrCodeSize,
		canvasDPIDefault,
		(int)canvasDPIZoomRatio,
		canvasPaddingLeft,
		canvasPaddingTop,
		canvasPaddingRight,
		canvasPaddingBottom,
		textRenderOffsetY,
		imageFileCacheValidSecondsDefault)
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	public const int SizeDefault = 200;
	public const int HeightDefault = 200;

	public const int PaddingLeftDefault = 10;
	public const int PaddingTopDefault = 10;
	public const int PaddingRightDefault = 10;
	public const int PaddingBottomDefault = 10;

	public enum DPIRatio
	{
		PC = 1,
		Mobile = 2
	}

	/// <summary>
	/// ECCLevel
	/// </summary>
	public enum ErrorCorrectionCodeLevel
	{
		/// <summary>
		/// L（Low）：大约可以容忍7%的错误或损坏。
		/// 这是最低的错误更正级别，适用于对容错率要求不高的场景。
		/// </summary>
		Low = 1,

		/// <summary>
		/// 大约可以容忍15%的错误或损坏。
		/// 这是中等错误更正级别，适用于一般的应用场景。
		/// </summary>
		Medium = 2,

		/// <summary>
		/// Q（Quartile）：大约可以容忍25%的错误或损坏。
		/// 这一级别提供了较高的错误更正能力，适用于需要较高容错率的场景。
		/// </summary>
		Quartile = 3,

		/// <summary>
		/// H（High）：大约可以容忍30%的错误或损坏。这是最高的错误更正级别，提供了最强的容错能力，适用于极端环境或需要极高可靠性的场景。
		/// </summary>
		High = 4
	}

	#endregion


	////////////////////////////////////////////////
	// @重载
	////////////////////////////////////////////////

	#region 重载

	protected new async Task<ImageCardInfo?> ToImageCardInfoAsync<DataType, GetDataParamType>(
		string? imageType,
		int imageQuality,
		string? fileDownloadName,
		GetDataParamType? getDataParam,
		Func<HttpClient,
			Func<string?, Task<SKBitmap?>>,
			GetDataParamType?,
			Task<DataType?>> toGetDataAsync,
		Func<DataType?,
			SKCanvas,
			//
			int,
			int,
			//
			Padding,
			//
			float,
			//
			HttpClient,
			Func<string, SKBitmap>,
			Func<uint, SKColor>,
			Func<string, SKTypeface?>,
			float> toRenderCard)
	{
		return await base.ToImageCardInfoAsync(
			imageType,
			imageQuality,
			fileDownloadName,
			getDataParam,
			toGetDataAsync,
			toRenderCard);
	}

	public async Task<ImageCardInfo?> ToImageCardInfoWithTextAsync(
		string qrCodeText,
		uint? backgroundColorHex,
		ErrorCorrectionCodeLevel eccLevel,
		uint? codeColorHex,
		string? imageType,
		int imageQuality,
		string? fileDownloadName)
	{
		ECCLevel qrCodeECCLevel;
		switch (eccLevel)
		{
			default:
			case ErrorCorrectionCodeLevel.Low:
				{
					qrCodeECCLevel = ECCLevel.L;
				}
				break;
			case ErrorCorrectionCodeLevel.Medium:
				{
					qrCodeECCLevel = ECCLevel.M;
				}
				break;
			case ErrorCorrectionCodeLevel.Quartile:
				{
					qrCodeECCLevel = ECCLevel.Q;
				}
				break;
			case ErrorCorrectionCodeLevel.High:
				{
					qrCodeECCLevel = ECCLevel.H;
				}
				break;
		}

		return await ToImageCardInfoAsync<string, object>(
			imageType,
			imageQuality,
			fileDownloadName,
			null,
			(httpClient,
			imageDownloader,
			getDataParam) =>
			{
				return Task.FromResult<string?>(null);
			},
			(evaluationGameLadderInfo,
			    canvas,
			    canvasWidth,
			    canvasHeightMax,
			    canvasPadding,
			    textRenderOffsetY,
			    httpClient,
			    imageFileNamed,
			    colorFromArgbHex,
			    fontFamilyFileNamed) =>
			{
				var qrCodeFrame
				= SKRect.Create(
					canvasPadding.Left,
					canvasPadding.Top,
					canvasWidth - canvasPadding.Width,
					canvasHeightMax - canvasPadding.Height);
				var generator = new QRCodeGenerator();
				var qrImage = generator.CreateQrCode(
				    qrCodeText,
				    qrCodeECCLevel,
				    false,
				    false,
				    QRCodeGenerator.EciMode.Default,
				    -1,
				    0);
				var backgroundColor
				= backgroundColorHex != null
				? ColorFromArgbHex(backgroundColorHex.Value)
				: SKColors.Empty;
				var codeColor
				= codeColorHex != null
				? ColorFromArgbHex(codeColorHex.Value)
				: SKColors.Black;
				////////////////////////////////////////////////
				canvas.Render(
					qrImage,
					qrCodeFrame,
					backgroundColor,
					codeColor,
					SKColors.Transparent);
				////////////////////////////////////////////////
				return canvasHeightMax;
			});
	}

	#endregion

}