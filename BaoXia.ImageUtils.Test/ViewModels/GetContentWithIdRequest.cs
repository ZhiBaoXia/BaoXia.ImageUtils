using BaoXia.Service.BxService.ViewModels;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class GetContentWithIdRequest : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public int ContentId { get; set; }


		public override bool IsValid
		{
			get
			{
				if (base.IsValid
					&& ContentId != 0)
				{
					return true;
				}
				return false;
			}
		}

		#endregion
	}
}