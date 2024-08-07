using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class RemoveContentRequest : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public int ContentId { get; set; }

		public IEnumerable<int>? ContentIds { get; set; }

		public override bool IsValid
		{
			get
			{
				if (base.IsValid)
				{
					if (ContentId != 0)
					{
						return true;
					}
					if (ContentIds is IEnumerable<int> contentIds)
					{
						foreach (var contentId in contentIds)
						{
							if (contentId != 0)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
		}

		#endregion
	}
}