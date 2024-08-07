using BaoXia.ImageUtils.Services;
using BaoXia.ImageUtils.Test.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaoXia.ImageUtils.Test.Controllers;

public class QrCodeController
	(Services.QrCodeImageCardService qrCodeService)
	: Controller
{
	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	private readonly ImageUtils.Services.QrCodeImageCardService _qrCodeService = qrCodeService;

	#endregion


	////////////////////////////////////////////////
	// @自身实现
	////////////////////////////////////////////////

	#region 自身实现

	public async Task<IActionResult> Index()
	{
		return await _qrCodeService.GetQRCodeImageIActionResultWithContentAsync(
			"https://www.baidu.com/s?wd=仓颉",
			Response,
			false,
			false);
	}

	#endregion
}