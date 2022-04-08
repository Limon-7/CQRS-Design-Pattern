using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.API.Contracts.V1
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v1";
		public const string Base = Root + "/" + Version;

		public static class WeatherForecast
		{
			public const string GetAll = Base + "/weather";
		}
		public static class Values
		{
			public const string GetAll = Base + "/values";
			public const string Get = Base + "/values/{valueId}";
			public const string Delete = Base + "/values/{valueId}";
			public const string Create = Base + "/values";
			public const string Update = Base + "/values/{valueId}";
		}
	}
}
