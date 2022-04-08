using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.API.Options
{
	public class JwtSettingsOptions
	{
		public string Secret { get; set; }
		public TimeSpan TokenLifeTime { get; internal set; }
	}
}
