using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class GetContentsWithIdsRequest : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public int[]? ContentIds { get; set; }

		public override bool IsValid
		{
			get
			{
				if (base.IsValid
					&& ContentIds?.Length > 0)
				{
					foreach (var contentIndexId in ContentIds)
					{
						if (contentIndexId != 0)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		#endregion
	}
}