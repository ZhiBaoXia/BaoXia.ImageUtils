using BaoXia.ImageUtils.Extensions;
using BaoXia.ImageUtils.Models;
using BaoXia.Utils.Cache;
using BaoXia.Utils.Interfaces;
using SkiaSharp;

namespace BaoXia.ImageUtils;
public class ImageCard
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	// 2779 = iPhone 14 Pro Max 的屏幕宽度。
	public const int CanvasWidthDefault = 414;
	// 2779 = iPhone 14 Pro Max 的屏幕高度。
	public const int CanvasHeightMaxDefault = 2778 * 3;

	public const float CanvasDPIDefault = 96.0F;
	public const int CanvasDPIZoomRatioDefault = 2;

	public const int CanvasPaddingLeftDefault = 20;
	public const int CanvasPaddingTopDefault = 20;
	public const int CanvasPaddingRightDefault = 20;
	public const int CanvasPaddingBottomDefault = 20;

	public const float TextRenderOffsetYDefault = -1.0F;

	public const double ImageFileCacheValidSecondsDefault = 3600.0;

	#endregion


	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	protected ILogFile? _debugLogFile;

	protected string? _resourceImagesDirectoryPath;

	protected int _canvasWidth;
	protected int _canvasHeightMax;
	protected Padding _canvasPadding;

	protected int _canvasDPIZoomRatio;
	protected float _canvasResolutionX;
	protected float _canvasResolutionY;

	protected int _canvasWidthInPixel;
	protected int _canvasHeightInPixel;

	protected float _textRenderOffsetY;


	////////////////////////////////////////////////


	protected readonly HttpClient _httpClient;


	protected ObjectPool<SKSurface> _canvasSurfaces;

	protected ItemsCacheAsync<string, SKBitmap, object?> _imageWithUrlKey;

	protected ItemsCache<string, SKTypeface?, object?> _fontFamiliesWithFontFileName;

	#endregion


	////////////////////////////////////////////////
	// @类方法
	////////////////////////////////////////////////

	#region 类方法

	protected static SKColor ColorFromArgbHex(uint hex)
	{
		return new SKColor(hex);
	}

	#endregion


	////////////////////////////////////////////////
	// @自身实现
	////////////////////////////////////////////////

	#region 自身实现

	public ImageCard(
		ILogFile? debugLogFile,
		string? resourceImagesDirectoryPath,
		//
		int canvasWidth = CanvasWidthDefault,
		int canvasHeightMax = CanvasHeightMaxDefault,
		//
		float canvasDPIDefault = CanvasDPIZoomRatioDefault,
		int canvasDPIZoomRatio = CanvasDPIZoomRatioDefault,
		//
		int canvasPaddingLeft = CanvasPaddingLeftDefault,
		int canvasPaddingTop = CanvasPaddingTopDefault,
		int canvasPaddingRight = CanvasPaddingRightDefault,
		int canvasPaddingBottom = CanvasPaddingBottomDefault,
		//
		float textRenderOffsetY = TextRenderOffsetYDefault,
		//
		double imageFileCacheValidSecondsDefault = ImageFileCacheValidSecondsDefault)
	{
		_debugLogFile = debugLogFile;

		_resourceImagesDirectoryPath = resourceImagesDirectoryPath;

		_canvasWidth = canvasWidth;
		// 2779 = iPhone 13 Pro Max 的屏幕高度。
		_canvasHeightMax = canvasHeightMax;
		_canvasWidthInPixel = _canvasWidth * canvasDPIZoomRatio;
		_canvasHeightInPixel = _canvasHeightMax * canvasDPIZoomRatio;

		_canvasDPIZoomRatio = canvasDPIZoomRatio;

		_canvasResolutionX = canvasDPIDefault * canvasDPIZoomRatio;
		_canvasResolutionY = _canvasResolutionX;

		_canvasPadding = new(
			canvasPaddingLeft,
			canvasPaddingTop,
			canvasPaddingRight,
			canvasPaddingBottom);

		_textRenderOffsetY = textRenderOffsetY;

		////////////////////////////////////////////////

		_httpClient = new();

		_canvasSurfaces
			= new(() =>
			{
				var canvasInfo = new SKImageInfo(
			    width: _canvasWidthInPixel,
			    height: _canvasHeightInPixel,
			    colorType: SKColorType.Rgba8888,
			    alphaType: SKAlphaType.Premul);
				var canvasSurface
		    = SKSurface.Create(canvasInfo);
				var canvas = canvasSurface.Canvas;
				{
					canvas.Scale(_canvasDPIZoomRatio);
				}
				return canvasSurface;
			});

		_imageWithUrlKey = new(
			async (imageUrl, _) =>
			{
				var image = await DownloadImageFromAsync(imageUrl);
				{ }
				return image;
			},
			null,
			null,
			() => imageFileCacheValidSecondsDefault,
			null);

		_fontFamiliesWithFontFileName = new(
			(fontFileName, _) =>
			{
				var fontFamily = FontNamed(fontFileName);
				{ }
				return fontFamily;
			},
			null,
			null,
			null);
	}

	protected SKBitmap ImageFileNamed(string imageFileName)
	{
		var imageFilePath
			= BaoXia.Utils.Environment.ApplicationDirectoryPath
			+ _resourceImagesDirectoryPath ?? string.Empty
			+ imageFileName;
		var skImage = SKImage.FromEncodedData(imageFilePath);
		var skBitmap = SKBitmap.FromImage(skImage);
		{ }
		return skBitmap;
	}

	protected SKTypeface? FontNamed(string fontFileName)
	{
		return _fontFamiliesWithFontFileName.Get(fontFileName, null);
	}

	protected async Task<SKBitmap?> DownloadImageFromAsync(string? imageUrl)
	{
		if (imageUrl == null
			|| imageUrl.Length < 1)
		{
			return null;
		}

		var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
		if (imageBytes == null
			|| imageBytes.Length < 1)
		{
			return null;
		}

		var skImage = SKImage.FromEncodedData(imageBytes);
		var skBitmap = SKBitmap.FromImage(skImage);
		{ }
		return skBitmap;
	}

	public async Task<ImageCardInfo?> ToImageCardInfoAsync<DataType, GetDataParamType>(
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
		var imageCardInfo = new ImageCardInfo()
		{
			ImageType = imageType,
			ImageQuality = imageQuality,
			FileDownloadName = fileDownloadName
		};

		DataType? data = default;
		var beginTimeOfGetData = DateTime.Now;
		{
			data = await toGetDataAsync(
				_httpClient,
				DownloadImageFromAsync,
				getDataParam);
		}
		var endTimeOfGetData = DateTime.Now;
		imageCardInfo.SecondsToGetData = (endTimeOfGetData - beginTimeOfGetData).TotalSeconds;


		var beginTimeOfRenderCard = DateTime.Now;
		{
			using var _ = _canvasSurfaces.GetObjectToUsing(out var canvasSurface);
			if (canvasSurface == null)
			{
				throw new ApplicationException("无法获取到有效的画布。");
			}
			var canvas = canvasSurface.Canvas;
			var cardFinalImageHeight
				= toRenderCard != null
				? toRenderCard(
					data,
					//
					canvasSurface.Canvas,
					//
					_canvasWidth,
					_canvasHeightMax,
					//
					_canvasPadding,
					//
					_textRenderOffsetY,
					 //
					 _httpClient,
					 ImageFileNamed,
					 ColorFromArgbHex,
					 FontNamed)
				: 0;
			if (cardFinalImageHeight <= 0)
			{
				_debugLogFile?.Logs(this, "生成图片卡片的高度为“0”。", null);
				return null;
			}


			var canvasBounds = canvas.BoundsI();
			var cardImageFrame = new SKRectI(
				0,
				0,
				canvasBounds.Width,
				(int)(cardFinalImageHeight * _canvasDPIZoomRatio));

			var imageCard = canvasSurface.Snapshot(cardImageFrame);
			{ }
			imageCardInfo.ImageCard = imageCard;
		}
		var endTimeOfRenderCard = DateTime.Now;
		imageCardInfo.SecondsToRenderCard = (endTimeOfRenderCard - beginTimeOfRenderCard).TotalSeconds;

		return imageCardInfo;
	}

	#endregion
}
