using System;
using System.Net;
using Craft.Net.Server;
using Craft.Net.TerrainGeneration;
using Craft.Net.Anvil;
using Craft.Net.Common;

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
		static GameMode gamemode = GameMode.Survival;
		static String levelName = "world";
		static String levelType = "NORMAL";
		static String ingameMOTD = "Chat is no longer broken!";

		public static void Main (string[] args)
		{
			var server = new Server (MainClass.port, MainClass.onlineMode, MainClass.maxPlayers, MainClass.motd, MainClass.difficulty, MainClass.levelName, MainClass.levelType, gamemode, MainClass.ingameMOTD);
			server.Start ();
		}
	}
}
