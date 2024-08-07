using BaoXia.Constants;
using BaoXia.Constants.Models;
using BaoXia.ImageUtils.Models;
using BaoXia.ImageUtils.ViewModels;
using BaoXia.Service.Attributes;
using BaoXia.Utils;
using BaoXia.Utils.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BaoXia.ImageUtils.Service;

public class QrCodeService
{
	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	public class CreateQRCodeImageCardInfoResult : Response
	{
		public ImageCardInfo? ImageCardInfo { get; set; }
	}

	#endregion

	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	private Func<QrCodeServiceConfig> ToGetServiceConfig { get; set; }

	private readonly ItemsCacheAsync<string, ImageCardInfo, object> _qrcodesCache;


	#endregion


	////////////////////////////////////////////////
	// @类方法
	////////////////////////////////////////////////

	#region 类方法

	static ContentResult ResultWithHttpStatusCode(
		HttpStatusCode httpStatusCode,
		string httpBody)
	{
		return new ContentResult()
		{
			StatusCode = (int)httpStatusCode,
			Content = httpBody,
			ContentType = "text/plain; utf-8"
		};
	}

	#endregion


	////////////////////////////////////////////////
	// @自身实现
	////////////////////////////////////////////////

	#region 自身实现

	public QrCodeService(Func<QrCodeServiceConfig> toGetServiceConfig)
	{
		ToGetServiceConfig = toGetServiceConfig;

		_qrcodesCache = new(
		async (qrCodeContent, _) =>
		{
			var serviceConfig = toGetServiceConfig();

			var qrCodeDpiRatio = EnumUtil.ValueOf(
				serviceConfig.QrCodeDpiRatioName,
				QrCodeServiceConfig.QrCodeDpiRatioDefault);
			int qrCodeSize = NumberUtil.FirstGreaterZero(
				serviceConfig.QrCodeSize,
				QrCodeServiceConfig.QrCodeSizeDefault);
			uint backgroundColorARGBHex = NumberUtil.FirstGreaterZero(
				serviceConfig.BackgroundColorARGBHex,
				QrCodeServiceConfig.BackgroundColorARGBHexDefault);
			var eccLevel = EnumUtil.ValueOf(
				serviceConfig.EccLevelName,
				QrCodeServiceConfig.EccLevelDefault);
			uint codeColorARGBHex = NumberUtil.FirstGreaterZero(
				serviceConfig.CodeColorARGBHex,
				QrCodeServiceConfig.CodeColorARGBHexDefault);
			string fileDownloadName = StringUtil.FirstNotEmpty(
				serviceConfig.FileDownloadName,
				QrCodeServiceConfig.FileDownloadNameDefault)!;
			string imageType = StringUtil.FirstNotEmpty(
				serviceConfig.ImageType,
				QrCodeServiceConfig.ImageTypeDefault)!;
			var imageQuality = NumberUtil.FirstGreaterZero(
				serviceConfig.ImageQuality,
				QrCodeServiceConfig.ImageQualityDefault);

			var qrCodeImageCardInfo
			= await QRCodeUtil.CreateQRCodeImageWithUrlAsync(
				qrCodeContent,
				qrCodeDpiRatio,
				qrCodeSize,
				backgroundColorARGBHex,
				eccLevel,
				codeColorARGBHex,
				fileDownloadName,
				imageType,
				imageQuality);
			{ }
			return qrCodeImageCardInfo;
		},
		null,
		() =>
		{
			var serviceConfig = toGetServiceConfig();
			return NumberUtil.FirstGreaterZero(
				serviceConfig.QrCodeCacheNoneReadSecondsToRemove,
				QrCodeServiceConfig.QrCodeCacheNoneReadSecondsToRemoveDefault);
		});
	}

	public async Task<CreateQRCodeImageCardInfoResult> GetQrCodeImageCardInfoWithContentAsync(
		string? qrCodeContent,
		bool isCacheDisable)
	{
		if (string.IsNullOrEmpty(qrCodeContent))
		{
			return new()
			{
				Error = Error.InvalidRequest,
				ErrorDescription = "生成二维码失败，指定二维码的内容字符串为“空”。"
			};
		}

		ImageCardInfo? qrCodeImageCardInfo;
		if (isCacheDisable)
		{
			qrCodeImageCardInfo = await _qrcodesCache.UpdateAsync(
				qrCodeContent,
				null);
		}
		else
		{
			qrCodeImageCardInfo = await _qrcodesCache.GetAsync(
				qrCodeContent,
				null);
		}

		if (qrCodeImageCardInfo == null)
		{
			return new()
			{
				Error = Error.OperationFailed,
				ErrorDescription = "生成二维码失败，无法生成二维码图片信息。"
			};
		}
		return new()
		{
			ImageCardInfo = qrCodeImageCardInfo
		};
	}

	public async Task<IActionResult> GetQRCodeImageIActionResultWithContentAsync(
		string? qrCodeContent,
		HttpResponse httpResponse,
		bool isCacheDisable)
	{
		var qrCodeImageCardInfo
			= await GetQrCodeImageCardInfoWithContentAsync(
				qrCodeContent,
				isCacheDisable);
		if (qrCodeImageCardInfo.IsFailed)
		{
			return ResultWithHttpStatusCode(
				HttpStatusCode.ServiceUnavailable,
				$"生成二维码失败，无法生成二维码图片信息：{qrCodeImageCardInfo.ErrorDescription}。");
		}

		var resultOfCreateQRCodeImageActionResult
			= qrCodeImageCardInfo.ImageCardInfo!.TryToActionResult(
				httpResponse);
		if (resultOfCreateQRCodeImageActionResult.IsFailed)
		{
			return ResultWithHttpStatusCode(
				HttpStatusCode.ServiceUnavailable,
				$"生成二维码失败，无法将二维码图片转为响应结果：{resultOfCreateQRCodeImageActionResult.ErrorDescription}。");
		}
		// !!!
		return resultOfCreateQRCodeImageActionResult.ActionResult!;
		// !!!
	}

	#endregion
}