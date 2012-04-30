using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Terraria;
using Hooks;
using TShockAPI;
using TShockAPI.DB;
using System.ComponentModel;


namespace PluginTemplate
{
	[APIVersion(1, 11)]
	public class PluginTemplate : TerrariaPlugin
	{
		public override string Name
		{
			get { return "SendRaw"; }
		}
		public override string Author
		{
			get { return "Efreak"; }
		}
		public override string Description
		{
			get { return "Impersonation of other users and raw broadcasts"; }
		}
		public override Version Version
		{
			get { return Assembly.GetExecutingAssembly().GetName().Version; }
		}

		public override void Initialize()
		{
			GameHooks.Initialize += OnInitialize;
		}
		protected override void Dispose(bool disposing)
		{
			GameHooks.Initialize -= OnInitialize;
			base.Dispose(disposing);
		}
		public PluginTemplate(Main game)
			:base(game)
		{
		}

		public void OnInitialize()
		{
			Commands.ChatCommands.Add(new Command("sendraw", SendRa, "sendraw"));
			Commands.ChatCommands.Add(new Command("sendred", SendRed, "sendred"));
			Commands.ChatCommands.Add(new Command("sendwhite", SendWhite, "sendwhite"));
			Commands.ChatCommands.Add(new Command("sendas", SendAs, "sendas"));
		}

		public static void SendAs(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendas <player> [message]", Color.Red);
				return;
			}
			if (args.Parameters[0].Length == 0)
			{
				args.Player.SendMessage("Missing player name", Color.Red);
				return;
			}

			string plStr = args.Parameters[0];
			var players = TShock.Utils.FindPlayer(plStr);
			if (players.Count == 0)
			{
				args.Player.SendMessage("Invalid player!", Color.Red);
			}
			else if (players.Count > 1)
			{
				var plrMatches = "";
				foreach (TSPlayer plr in players)
				{
					if (plrMatches.Length != 0)
					{
						plrMatches += ", " + plr.Name;
					}
					else
					{
						plrMatches += plr.Name;
					}
				}
				args.Player.SendMessage("More than one player matched! Matches: " + plrMatches, Color.Red);
			}
			else
			{
				string message = players[0].Group.Prefix + args.Parameters[0] + players[0].Group.Suffix + ":";
				for (int i = 1; i < args.Parameters.Count; i++)
				{
					message += " " + args.Parameters[i];
				}

				Color messagecolor = new Color(players[0].Group.R, players[0].Group.G, players[0].Group.B);
				TShock.Utils.Broadcast(message, messagecolor);
			}
			
		}
		public static void SendRa(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendraw [something to send]", Color.Red);
				return;
			}
			else
			{
				string message = "";
				for (int i = 0; i < args.Parameters.Count; i++)
				{
					message += " " + args.Parameters[i];
				}

				TShock.Utils.Broadcast(message);
			}
		}
		public static void SendWhite(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendraw [something to send]", Color.Red);
				return;
			}
			else
			{
				string message = "";
				for (int i = 0; i < args.Parameters.Count; i++)
				{
					message += " " + args.Parameters[i];
				}

				TShock.Utils.Broadcast(message, Color.White);
			}
		}
		public static void SendRed(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendraw [something to send]", Color.Red);
				return;
			}
			else
			{
				string message = "";
				for (int i = 0; i < args.Parameters.Count; i++)
				{
					message += " " + args.Parameters[i];
				}

				TShock.Utils.Broadcast(message, Color.Red);
			}
		}
		
		public static void SendColor(CommandArgs args) //start new command for built-in colors
		{
			if(args.Parameters.count < 2)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendcolor [colorname] message");
				return;
			}
			string message = "";
			for (int i = 1; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}

			TShock.Utils.Broadcast(message, FromName(ags.Parameters[0]));
		}
		public static void SendColor(CommandArgs args) //start new command for custom colors by RGB
		{
			if(args.Parameters.count < 4)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendcolor [Red] [Green] [Blue] [message]. Use 0-255 for RGB values");
				return;
			}
			string message = "";
			for (int i = 3; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}
			
			Color color = new Color;
			color.R=Convert.ToByte(args.Parameters[0],10);
			color.G=Convert.ToByte(args.Parameters[1],10);
			color.B=Convert.ToByte(args.Parameters[2],10);
			TShock.Utils.Broadcast(message, color);
		}
	}
}