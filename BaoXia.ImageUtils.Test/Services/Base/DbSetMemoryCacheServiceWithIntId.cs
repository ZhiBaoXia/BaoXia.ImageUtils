using BaoXia.DataUtils.Interfaces;
using BaoXia.DataUtils.Models;
using BaoXia.DataUtils.Services;
using BaoXia.ImageUtils.Test.Data;
using Microsoft.EntityFrameworkCore;

namespace BaoXia.ImageUtils.Test.Services.Base;

public abstract class DbSetMemoryCacheServiceWithIntId
	<EntityType>
	(Func<BxImageUtilsTestDbContext, DbSet<EntityType>> toGetEntitySetInDb)
	: DbSetMemoryCacheServiceWithIntId<
		// 数据库上下文类型：
		BxImageUtilsTestDbContext,
		// 数据实体类型：
		EntityType>
	(BaoXia.Utils.Environment.GetRequiredService<IDbSetMemoryCacheServiceConfig>(),
		BaoXia.Utils.Environment.GetRequiredService<DbSetMemoryCacheManageInfoService>())
	where EntityType : ModelWithBaseFields<int>, new()
{
	////////////////////////////////////////////////
	// @实现“IDbSetMemoryCacheServiceWithIntId”
	////////////////////////////////////////////////

	#region 实现“IDbSetMemoryCacheServiceWithIntId”

	protected override DbSet<EntityType> DidGetEntitySet(BxImageUtilsTestDbContext db)
	{
		return toGetEntitySetInDb(db);
	}


	#endregion
}