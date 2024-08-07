using BaoXia.ImageUtils.Test.ConfigFiles;
using BaoXia.ImageUtils.Test.Constants;
using BaoXia.ImageUtils.Test.LogFiles;
using BaoXia.Service.BxService.ViewModels;
using BaoXia.Service.Constants;
using BaoXia.Service.Extensions;
using System.Reflection;

var appBuilder = WebApplication.CreateBuilder(args);
var httpServer = HttpServer.Kestrel;
#if WEBSERVER_IIS
httpServer = HttpServer.IIS;
#endif

appBuilder.RunWithServerName(
#if DEBUG
	"Development Server",
#else
	"Production Server",
#endif
	BaoXia.Utils.Environment.GetEnvironmentNameWith_ASPNETCORE_ENVIRONMENT(),
	Assembly.GetExecutingAssembly(),
	httpServer,
	//
	AESKeys.Default,
	//
	"ConfigFiles",
	"LogFiles",
	//
	Log.Logs,
	Log.Exception,
	Log.Warning,
	Log.BxService,
	//
	Config.Service,
	nameof(ServiceConfig.WebServiceConfig),
	//
	RouteTemplateConstants.RouteTemplatesDefault,
	//
	new ServiceInitializationParam(
		//
		Config.Service,
		nameof(ServiceConfig.WebServiceConfig),
		Config.Service,
		nameof(ServiceConfig.BxServiceConfig),
		//
		true,
		false,
		false,
		new BaoXia.Service.BxService.ServiceDiscoverer()),
	new ServiceAdministratorInitializationParam(
		Config.Service,
		nameof(ServiceConfig.ServiceAdministratorConfig)),
	(appBuilder) =>
	{
		////////////////////////////////////////////////
		// 1/4，配置服务客户端：数据库服务。
		////////////////////////////////////////////////
		//appBuilder
		//.Services
		//.AddDbContext<BxImageUtilsTestDbContext>(options =>
		//{
		//	var dbConnectionString
		//	= Config.Service.BxImageUtilsTestConfig?.DbConnectionString
		//	?? throw new InvalidOperationException(
		//		"配置数据库服务失败，没有在配置文件中配置“数据库连接字符串”（DbConnectionString）。");
		//	// !!!
		//	options.UseSqlServer(
		//		dbConnectionString,
		//		sqlServerOptionsAction: sqlOptions =>
		//		{
		//			// !!!⚠ 设置数据库操作的超时时间为30秒， ⚠!!!
		//			// !!!⚠ 放宽对数据库的操作超时时间限制，  ⚠!!!
		//			// !!!⚠ 提升数据保存队列的可靠性。             ⚠!!!
		//			sqlOptions.CommandTimeout(30);
		//		});
		//	// !!!
		//});

		////////////////////////////////////////////////
		// 2/4，配置服务客户端：缓存服务。
		////////////////////////////////////////////////
		//var iLogFile_Logs = new ILogFile(Log.Logs);
		//var iLogFile_Exception = new ILogFile(Log.Exception);
		//var iLogFile_Warning = new ILogFile(Log.Warning);
		//var iLogFile_BxService = new ILogFile(Log.BxService);
		//appBuilder.ConfigureBxCacheService(
		//	Config.Service.BxServiceConfig?.ServiceInfo?.GroupName,
		//	() =>
		//	{
		//		return Config.Service.BxImageUtilsTestConfig?.BxCacheServiceClientConfig;
		//	},
		//	iLogFile_Logs,
		//	iLogFile_Exception,
		//	iLogFile_Warning,
		//	iLogFile_BxService);

		////////////////////////////////////////////////
		// 3/4，配置服务客户端：令牌服务。
		////////////////////////////////////////////////
		//appBuilder.ConfigureBxTokenManager(
		//	Config.Service.BxServiceConfig?.ServiceInfo?.GroupName,
		//	//
		//	TokenCheckType.NeedCheckTokenDefault_ElseNotNeedCheckAttribute,
		//	true,
		//	null,
		//	//
		//	() =>
		//	{
		//		return Config.Service.BxImageUtilsTestConfig?.BxTokenManagerClientConfig;
		//	},
		//	iLogFile_Logs,
		//	iLogFile_Exception,
		//	iLogFile_Warning,
		//	iLogFile_BxService);

		////////////////////////////////////////////////
		// 4/4，配置服务客户端：用户服务。
		////////////////////////////////////////////////
		//appBuilder.ConfigureBxUserService(
		//	Config.Service.BxServiceConfig?.ServiceInfo?.GroupName,
		//	//
		//	() =>
		//	{
		//		return Config.Service.BxImageUtilsTestConfig?.BxUserServiceClientConfig;
		//	},
		//	iLogFile_Logs,
		//	iLogFile_Exception,
		//	iLogFile_Warning,
		//	iLogFile_BxService);

		return appBuilder;
	});