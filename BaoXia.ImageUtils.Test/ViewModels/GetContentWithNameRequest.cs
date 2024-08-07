using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class GetContentWithNameRequest : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public string? ContentName { get; set; }

		public override bool IsValid
		{
			get
			{
				if (base.IsValid
					&& !string.IsNullOrEmpty(ContentName))
				{
					return true;
				}
				return false;
			}
		}

		#endregion
	}
}
