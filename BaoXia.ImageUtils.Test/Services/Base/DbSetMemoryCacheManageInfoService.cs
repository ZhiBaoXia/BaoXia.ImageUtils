using BaoXia.DataUtils.Interfaces;
using BaoXia.DataUtils.Models;
using BaoXia.DataUtils.Services;
using BaoXia.DataUtils.ViewModels;
using BaoXia.ImageUtils.Test.Data;
using BaoXia.Service.Attributes;
using Microsoft.EntityFrameworkCore;

namespace BaoXia.ImageUtils.Test.Services.Base
{
	[Service]
	public class DbSetMemoryCacheManageInfoService
		(IDbSetMemoryCacheServiceConfig dbSetMemoryCacheServiceConfig)
		: DbSetMemoryCacheManageInfoServiceWithIntId
		<____服务实例名称____DbContext>
		(dbSetMemoryCacheServiceConfig)
	{
		////////////////////////////////////////////////
		// @实现“DbSetMemoryCacheManageInfoServiceWithIntId”
		////////////////////////////////////////////////

		#region 实现“DbSetMemoryCacheManageInfoServiceWithIntId”

		protected override DbSet<DbSetMemoryCacheManageInfoWithIntId> DidGetEntitySet(____服务实例名称____DbContext db)
		{
			return db.__DbSetMemoryCacheManageInfes;
		}

		#endregion
	}
}