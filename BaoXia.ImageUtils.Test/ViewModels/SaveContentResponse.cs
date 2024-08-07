using BaoXia.DataUtils.ViewModels;
using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class SaveContentResponse<ContentType> : Response
		where ContentType : class, new()
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public ContentType? Content { get; set; }

		#endregion


		////////////////////////////////////////////////
		// @自身实现
		////////////////////////////////////////////////

		#region 自身实现

		public void SetResult(EntityOperateResult<ContentType>? result)
		{
			if (result == null)
			{
				return;
			}

			Error = result.Error;
			ErrorDescription = result.ErrorDescription;

			Content = result.Entity;
		}

		#endregion
	}
}