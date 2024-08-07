using BaoXia.DataUtils;
using BaoXia.ImageUtils.Test.Models;
using BaoXia.ImageUtils.Test.Services.Base;
using BaoXia.Utils.Extensions;

namespace BaoXia.ImageUtils.Test.Services
{
	/// <summary>
	/// 宝匣软件，宝匣服务（微服务架构），服务模板（计算Id的模型）。
	/// 请替换以下关键字：
	/// 替换：ExampleWithComputedId，为：当前服务模型的类名。
	/// </summary>
	public class ModelService
		()
		: DbSetMemoryCacheServiceWithIntId
		<ModelWithComputedId>
		(db => db.ModelWithComputedIds)
	{
		////////////////////////////////////////////////
		// @自身属性
		////////////////////////////////////////////////

		#region 自身属性


		protected EntityIndexWith1Key<ModelWithComputedId, string> Index_EntityKey_1 = new(
		    (entity) => entity.EntityKey_1 ?? string.Empty);

		protected EntitiesIndexWith1Key<ModelWithComputedId, string> Index_GroupKey_1 = new(
		    (entity) => entity.GroupKey_1 ?? string.Empty,
		    (entityA, entityB) => entityA.Id == entityB.Id,
		    (entityA, entityB) => entityA.Id.CompareTo(entityB.Id));

		#endregion


		////////////////////////////////////////////////
		// @自身实现
		////////////////////////////////////////////////

		#region 自身实现

		public ModelWithComputedId? GetEntityWithEntityKey_1ReadOnly(string? entityKey_1)
		{
			if (string.IsNullOrEmpty(entityKey_1))
			{
				return null;
			}
			return Index_EntityKey_1.GetEntity(entityKey_1);
		}

		public ModelWithComputedId? GetEntityWithEntityKey_1Writeable(string? entityKey_1)
		{
			return GetEntityWithEntityKey_1ReadOnly(entityKey_1)
				?.Clone();
		}

		public ModelWithComputedId[]? GetEntitiesWithGroupKey_1ReadOnly(string? groupKey_1)
		{
			if (string.IsNullOrEmpty(groupKey_1))
			{
				return null;
			}
			return Index_GroupKey_1.GetEntities(groupKey_1);
		}

		public ModelWithComputedId[]? GetEntitiesWithGroupKey_1Writeable(string? groupKey_1)
		{
			return GetEntitiesWithGroupKey_1ReadOnly(groupKey_1)
				.CloneItemsToArray();
		}


		#endregion
	}
}