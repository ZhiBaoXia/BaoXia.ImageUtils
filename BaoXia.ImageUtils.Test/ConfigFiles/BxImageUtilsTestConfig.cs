using BaoXia.DataUtils.ViewModels;

namespace BaoXia.ImageUtils.Test.ConfigFiles;

/// <summary>
/// BxImageUtilsTest配置信息。
/// </summary>
public class BxImageUtilsTestConfig
{

	////////////////////////////////////////////////
	// @静态常量
	////////////////////////////////////////////////

	#region 静态常量

	public readonly static DbOperationConfig DbOperationConfigDefault = new()
	{
		LoadThreadsCount = 10,
		LoadPageSize = 1000,

		SaveThreadsCount = 10,
		SaveTaskProcessSecondsMaxThreshold = 1.0,
		SaveTaskQueueLengthMaxThreshold = 10
	};

	#endregion


	////////////////////////////////////////////////
	// @自身属性
	////////////////////////////////////////////////

	#region 自身属性

	/// <summary>
	/// 自身业务相关配置。
	/// </summary>
	/// public string? Xxx { get; set; }


	/// <summary>
	/// 1.a/4，配置服务客户端：数据库，连接字符串。
	/// </summary>
	public string? DbConnectionString { get; set; }

	/// <summary>
	/// 1.b/4，配置服务客户端：数据库，操作配置。
	/// </summary>
	public DbOperationConfig? DbOperationConfig { get; set; }

	///// <summary>
	///// 2/4，配置服务客户端：缓存服务。
	///// </summary>
	//public BxCacheServiceClientConfig? BxCacheServiceClientConfig { get; set; }

	///// <summary>
	///// 3/4，配置服务客户端：令牌服务。
	///// </summary>
	//public BxTokenManagerClientConfig? BxTokenManagerClientConfig { get; set; }

	///// <summary>
	///// 4/4，配置服务客户端：用户服务。
	///// </summary>
	//public BxUserServiceClientConfig? BxUserServiceClientConfig { get; set; }

	#endregion
}
