using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class SearchContentRequest : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public string? SearchKey { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public override bool IsValid
		{
			get
			{
				if (base.IsValid
					&& PageIndex >= 0
					&& PageSize > 0)
				{
					return true;
				}
				return false;
			}
		}

		#endregion
	}
}
