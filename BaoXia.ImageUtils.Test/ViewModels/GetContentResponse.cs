using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class GetContentResponse<ContentType> : Response
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public ContentType? Content { get; set; }

		#endregion
	}
}