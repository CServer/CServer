using System;
using Server;

namespace CServer.API
{
	public class Plugin
	{
		PluginTunnel tunnel;
		protected Plugin (Server server)
		{
			this.tunnel = new PluginTunnel(server);
		}
	}
}