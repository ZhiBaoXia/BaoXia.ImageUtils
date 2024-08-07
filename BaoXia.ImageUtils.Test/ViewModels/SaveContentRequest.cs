using BaoXia.Service.BxService.ViewModels;
using BaoXia.Utils.Extensions;

namespace BaoXia.ImageUtils.Test.ViewModels
{
	public class SaveContentRequest<ContentType> : Request
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public ContentType? Content { get; set; }

		public override bool IsValid
		{
			get
			{
				if (base.IsValid
					&& Content != null)
				{
					var isContentValidObject
						= ObjectExtension.GetPropertyValueWithName(
							Content,
							nameof(IsValid));
					if (isContentValidObject == null)
					{
						return true;
					}
					if (isContentValidObject is bool isContentValid
						&& isContentValid == true)
					{
						return true;
					}
				}
				return false;
			}
		}

		#endregion
	}
}