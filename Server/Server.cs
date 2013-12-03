using System;
using Craft.Net.TerrainGeneration;
using Craft.Net.Anvil;
using Craft.Net.Server;
using Craft.Net.Server.Events;
using Craft.Net.Common;
using System.Net;
using Newtonsoft.Json;

namespace Server
{
	public class Server
	{
		private MinecraftServer server;
		private int port;
		private String ingameMOTD;
		public Server (int port, bool onlineMode, int maxPlayers, String motd, int difficulty, String levelName, String levelType, GameMode gamemode, String ingameMOTD)
		{
			var level = new Level ();
			if (levelType == "FLAT") {
				var generator = new FlatlandGenerator ();
				level = new Level (generator);
			} else if (levelType == "NORMAL") {
				var generator = new StandardGenerator ();
				level = new Level (generator);
			}
			level.AddWorld ("overworld");
			this.server = new MinecraftServer (level);
			this.server.Settings.MotD = motd;
			this.server.Settings.OnlineMode = onlineMode;
			this.server.Settings.MaxPlayers = (byte)maxPlayers;
			this.server.Settings.Difficulty = (Difficulty)difficulty;
			this.server.PlayerLoggedIn += OnPlayerLoggedIn;
			this.server.ChatMessage += OnPlayerChatMessage;
			this.server.PlayerLoggedOut += OnPlayerLoggedOut;
			this.server.Level.GameMode = gamemode;
			this.port = port;
			this.ingameMOTD = ingameMOTD;
		}

		public void Start () {
			Console.WriteLine ("Starting Minecraft server on port {0}...", this.port.ToString());
			this.server.Start (new IPEndPoint (IPAddress.Any, this.port));
			Console.WriteLine ("Server started.");
		}

		public void Stop () {
			Console.WriteLine ("Stopping server...");
			//this.server.Stop ();
			System.Environment.Exit (0);
			// Taken out due to buggy behavior and freezing
			// Console.WriteLine ("Saving level...");
			// this.server.Level.Save ();
		}

		private void OnPlayerLoggedIn (object s, PlayerLogInEventArgs e) {
			e.Handled = true;
			this.SendChat ("A player has joined the game: " + e.Username);
			this.SendConsoleMessage (string.Format(e.Username + " has joined the server into world " + e.Client.World + " with gamemode " + e.Client.GameMode + "."));
			e.Client.SendChat (string.Format("[MOTD] {0}", this.ingameMOTD));
		}

		private void OnPlayerLoggedOut (object s, PlayerLogInEventArgs e) {
			e.Handled = true;
			this.SendChat ("A player has left the game: " + e.Username);
		}

		private void OnPlayerChatMessage (object s, ChatMessageEventArgs e) {
			e.Handled = true;
			/*var message = JsonConvert.DeserializeObject<DeserializedChatMessage> (e.RawMessage).text;
			if (e.RawMessage.Equals ("/stop")) {
				this.SendChat (e.Origin.Username + " has stopped the server.");
				this.Stop ();
			} else
				this.SendChat (string.Format ("Message: {1}",  message));
				*/
			this.SendChat (string.Format ("<{0}> {1}", e.Origin.Username, e.RawMessage));
			if (e.RawMessage == "/stop" && e.Origin.Username == "mypalsminecraft") {
				this.SendChat (string.Format ("{0} has stopped the server.", e.Origin.Username));
				this.Stop ();
			}
		}

		public void SendChat (String message) {
			Console.WriteLine ("[CHAT] " + message);
			this.server.SendChat (message.Replace ("{", "\\{").Replace ("}", "\\}").Replace ("\"", "\\\"").Replace (":", "\\:"));
		}

		public void SendConsoleMessage (String message) {
			Console.WriteLine ("[INFO] " + message);
		}

		public void OnPlayerDeath(object s, Craft.Net.
	}
}

