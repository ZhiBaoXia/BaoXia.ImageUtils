using BaoXia.Service.Models;
using BaoXia.Service.WebService.Models;
using BaoXia.Utils;

namespace BaoXia.ImageUtils.Test.ConfigFiles
{
	public class ServiceConfig : ConfigFile
	{
		////////////////////////////////////////////////
		// @自身属性，服务基础参数
		////////////////////////////////////////////////

		#region 自身属性，服务基础参数

		/// <summary>
		/// ____服务实例名称____配置。
		/// </summary>
		public ____服务实例名称____Config? ____服务实例名称____Config { get; set; }

		/// <summary>
		/// 服务管理员配置。
		/// </summary>
		public ServiceAdministratorConfig? ServiceAdministratorConfig { get; set; }

		/// <summary>
		/// Web服务基础配置。
		/// </summary>
		public WebServiceConfig? WebServiceConfig { get; set; }

		/// <summary>
		/// 长连接服务配置。
		/// </summary>
		public BaoXia.Service.BxService.Models.ServiceConfig? BxServiceConfig { get; set; }

		#endregion
	}
}