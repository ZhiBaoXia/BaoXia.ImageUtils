using BaoXia.DataUtils.Interfaces;
using BaoXia.DataUtils.Models;
using BaoXia.DataUtils.Services;
using BaoXia.ImageUtils.Test.Data;
using Microsoft.EntityFrameworkCore;

namespace BaoXia.ImageUtils.Test.Services.Base;

public class DbSetMemoryCacheManageInfoService
	(IDbSetMemoryCacheServiceConfig dbSetMemoryCacheServiceConfig)
	: DbSetMemoryCacheManageInfoServiceWithIntId
	<BxImageUtilsTestDbContext>
	(dbSetMemoryCacheServiceConfig)
{
	////////////////////////////////////////////////
	// @实现“DbSetMemoryCacheManageInfoServiceWithIntId”
	////////////////////////////////////////////////

	#region 实现“DbSetMemoryCacheManageInfoServiceWithIntId”

	protected override DbSet<DbSetMemoryCacheManageInfoWithIntId> DidGetEntitySet(BxImageUtilsTestDbContext db)
	{
		return db.__DbSetMemoryCacheManageInfes;
	}

	#endregion
}