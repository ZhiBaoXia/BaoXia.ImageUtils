using BaoXia.DataUtils.Attributes;
using BaoXia.DataUtils.Interfaces;
using BaoXia.DataUtils.ModelFields;
using BaoXia.DataUtils.Models;
using Microsoft.EntityFrameworkCore;

namespace BaoXia.ImageUtils.Test.Models
{
	/// <summary>
	/// 宝匣软件，宝匣服务（微服务架构），自增Id模型的模板。
	/// 请替换以下关键字：
	/// 替换：BaoXia.Service.ProjectTemplate，为：当前项目的模型命名空间。
	/// </summary>
	[Index(nameof(ModelWithAutoIdentityId.Name), IsUnique = true)]
	[Index(nameof(ModelWithAutoIdentityId.Identity), IsUnique = true)]
	[EntityIndentityProperty(nameof(ModelWithAutoIdentityId.Identity))]
	public class ModelWithAutoIdentityId
		: ModelWithAutoIdentityId<int>,
		IModel_SafeInfo
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性

		public string Name { get; set; } = default!;

		public string Identity { get; set; } = default!;

		public string? EntityKey_1 { get; set; }

		public string? GroupKey_1 { get; set; }

		private ModelFields_SafeInfo _createSafeInfo = default!;
		public ModelFields_SafeInfo CreateSafeInfo { get => _createSafeInfo; set => _createSafeInfo = value; }

		private ModelFields_SafeInfo _updateSafeInfo = default!;
		public ModelFields_SafeInfo UpdateSafeInfo { get => _updateSafeInfo; set => _updateSafeInfo = value; }

		#endregion
	}
}
