using BaoXia.Constants;
using BaoXia.Constants.Models;
using BaoXia.Utils.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace BaoXia.ImageUtils.Models;

public class ImageCardInfo
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	public class ToActionResultResult : Response
	{
		public IActionResult? ActionResult { get; set; }
	}

	#endregion


	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	public string? ImageType { get; set; }

	public int ImageQuality { get; set; }

	public string? FileDownloadName { get; set; }

	public SKImage? ImageCard { get; set; }

	public double SecondsToGetData { get; set; }

	public double SecondsToRenderCard { get; set; }

	#endregion


	////////////////////////////////////////////////
	// @自身实现
	////////////////////////////////////////////////

	#region 自身实现

	public ToActionResultResult TryToActionResult(
		HttpResponse httpResponse)
	{
		httpResponse.Headers["BaoXia-ImageUtil-Seconds-To-GetData"] = SecondsToGetData.ToString("F3");
		httpResponse.Headers["BaoXia-ImageUtil-Seconds-To-RenderCard"] = SecondsToRenderCard.ToString("F3");

		var imageCard = ImageCard;
		if (imageCard == null)
		{
			return new()
			{
				Error = Error.InvalidRequest,
				ErrorDescription = "生成图片卡片失败，“imageCard”为“null”。"
			};
		}

		byte[] imageCardBytes;
		string imageCardMIME;
		var imageType = ImageType;
		var imageQuality = ImageQuality;
		{
			if (imageQuality < 0)
			{
				imageQuality = 0;
			}
			else if (imageQuality > 100)
			{
				imageQuality = 100;
			}
		}
		if ("webp".EqualsIgnoreCase(imageType))
		{
			imageCardBytes = imageCard.Encode(SKEncodedImageFormat.Webp, imageQuality).ToArray();
			imageCardMIME = "image/webp";
		}
		else if ("jpg".EqualsIgnoreCase(imageType)
			|| "jpeg".EqualsIgnoreCase(imageType))
		{
			imageCardBytes = imageCard.Encode(SKEncodedImageFormat.Jpeg, imageQuality).ToArray();
			imageCardMIME = "image/jpg";
		}
		else if ("gif".EqualsIgnoreCase(imageType))
		{
			imageCardBytes = imageCard.Encode(SKEncodedImageFormat.Gif, imageQuality).ToArray();
			imageCardMIME = "image/gif";
		}
		else
		{
			imageCardBytes = imageCard.Encode(SKEncodedImageFormat.Png, imageQuality).ToArray();
			imageCardMIME = "image/png";
		}


		// !!!
		var fileContentResult = new FileContentResult(
			imageCardBytes,
			imageCardMIME);
		// !!!
		{
			fileContentResult.FileDownloadName = FileDownloadName;
		}
		return new()
		{
			ActionResult = fileContentResult
		};
	}

	#endregion
}