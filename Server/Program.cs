using System;
using System.Net;
using Craft.Net.Server;
using Craft.Net.TerrainGeneration;
using Craft.Net.Anvil;

namespace Server
{
	class MainClass
	{
		// Configuration
		static int port = 25566;
		static bool onlineMode = true;
		static int maxPlayers = 20;
		static String motd = "A Minecraft Server";
		static int difficulty = 2;
		static String levelName = "world";
		static String levelType = "NORMAL";

		public static void Main (string[] args)
		{
			var server = new Server (MainClass.port, MainClass.onlineMode, MainClass.maxPlayers, MainClass.motd, MainClass.difficulty, MainClass.levelName, MainClass.levelType, Craft.Net.Common.GameMode.Survival);
			server.Start ();
		}
	}
}
