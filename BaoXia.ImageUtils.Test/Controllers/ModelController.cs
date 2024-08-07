using BaoXia.Constants;
using BaoXia.ImageUtils.Test.Models;
using BaoXia.ImageUtils.Test.Services;
using BaoXia.ImageUtils.Test.ViewModels;
using BaoXia.Service.Attributes;
using BaoXia.Service.BxService.ViewModels;
using BaoXia.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BaoXia.ImageUtils.Test.Controllers
{
	/// <summary>
	/// 宝匣软件，宝匣服务（微服务架构），控制器模板（计算Id的模型）。
	/// 请替换以下关键字：
	/// 替换：ExampleWithComputedId，为：当前业务模型的类名称。
	/// 替换：ExampleModel_ComputedIdService，为：当前业务模型的服务的类名称。
	/// </summary>
	public class ModelController(
		ModelService entityService)
		: Controller
	{
		////////////////////////////////////////////////
		// @自身实现
		////////////////////////////////////////////////

		#region 自身实现

		[ApiName("保存模型对象")]
		[HttpPost]
		public async Task<IActionResult> SaveEntity(
			[FromBody] SaveContentRequest<ModelWithComputedId> request)
		{
			var response
				= new SaveContentResponse<ModelWithComputedId>();
			var modelSaveResult
				= await entityService
				.SaveEntityAsync(
					request.Content,
					//
					0,
					this.GetClientIpInfo(),
					DateTime.Now,
					null);
			{
				response.SetResult(modelSaveResult);
			}
			return Json(response);
		}

		[ApiName("删除模型对象，根据模型Id")]
		[HttpPost]
		public async Task<IActionResult> RemoveEntitiesWithIds(
			[FromBody] RemoveContentRequest request)
		{
			var response = new Response();

			Result removeResult;
			if (request.ContentId != 0)
			{
				removeResult = await entityService.RemoveEntityWithIdAsync(
					request.ContentId,
					//
					0,
					this.GetClientIpInfo(),
					DateTime.Now,
					null);
			}
			else
			{
				removeResult = await entityService.RemoveEntitiesWithIdsAsync(
					request.ContentIds,
					//
					0,
					this.GetClientIpInfo(),
					DateTime.Now,
					null);
			}

			//
			response.Error = removeResult.Error;
			response.ErrorDescription = removeResult.ErrorDescription;
			//

			return Json(response);
		}

		[ApiName("获取模型对象，根据模型Id")]
		[HttpPost]
		public IActionResult GetEntityWithId(
			[FromBody] GetContentWithIdRequest request)
		{
			var response = new GetContentResponse<ModelWithComputedId>();

			var model = entityService.GetEntityWithIdReadOnly(request.ContentId);
			if (model == null)
			{
				response.Error = Error.ObjectNotExisted;
			}
			else
			{
				response.Content = model;
			}

			return Json(response);
		}

		[ApiName("获取模型对象，根据模型名称")]
		[HttpPost]
		public IActionResult GetEntityWithName(
			[FromBody] GetContentWithNameRequest request)
		{
			var response = new GetContentResponse<ModelWithComputedId>();

			var model
				= entityService.GetEntityWithNameReadOnly(request.ContentName);
			if (model == null)
			{
				response.Error = Error.ObjectNotExisted;
			}
			else
			{
				response.Content = model;
			}

			return Json(response);
		}

		[ApiName("搜索模型（获取模型列表）")]
		[HttpPost]
		public async Task<IActionResult> SearchEntities(
			[FromBody] SearchContentRequest request)
		{
			var response = new SearchContentResponse<ModelWithComputedId>(request);

			var searchResult = await entityService.SearchEntitiesAsync(
				request.SearchKey,
				request.PageIndex,
				request.PageSize);
			if (searchResult != null)
			{
				response.ContentCountSearched = searchResult.ItemsCountSearchMatched;
				response.ContentInPage = searchResult.ItemsInPage;
			}
			else
			{
				response.Error = Error.ObjectNotExisted;
				response.ErrorDescription = "搜索模型失败，模型不存在。";
			}

			return Json(response);
		}

		#endregion
	}
}
