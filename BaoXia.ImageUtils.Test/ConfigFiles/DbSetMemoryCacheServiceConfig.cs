using BaoXia.DataUtils.Interfaces;
using BaoXia.DataUtils.ViewModels;
using BaoXia.ImageUtils.Test.LogFiles;
using BaoXia.Service.Attributes;
using BaoXia.Utils.Interfaces;

namespace BaoXia.ImageUtils.Test.ConfigFiles
{
	[Service]
	public class DbSetMemoryCacheServiceConfig
		: IDbSetMemoryCacheServiceConfig
	{
		////////////////////////////////////////////////
		// @类方法
		////////////////////////////////////////////////

		#region 类方法

		static DbSetMemoryCacheServiceConfig? _shared = null;

		public static DbSetMemoryCacheServiceConfig Shared
		{
			get
			{
				//
				_shared ??= new();
				//
				return _shared;
			}
		}

		#endregion


		////////////////////////////////////////////////
		// @实现“DbSetMemoryCacheServiceConfig”
		////////////////////////////////////////////////

		#region 实现“DbSetMemoryCacheServiceConfig”

		public DbOperationConfig DbOperationConfig
			=> Config.Service.BxImageUtilsTestConfig?.DbOperationConfig
			?? BxImageUtilsTestConfig.DbOperationConfigDefault;

		private static readonly ILogFile _dbSetSaverLogFile = new(Log.DbSetSaver);

		public ILogFile? DbSetSaverLogFile => _dbSetSaverLogFile;

		private static readonly ILogFile _exceptionLogFile = new(Log.Exception);

		public ILogFile? ExceptionLogFile => _exceptionLogFile;

		#endregion
	}
}