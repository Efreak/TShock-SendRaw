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
			Commands.ChatCommands.Add(new Command("sendcolor", SendColor, "sendcolor"));
            Commands.ChatCommands.Add(new Command("sendrgb", SendRGB, "sendrgb"));
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
                return;
			}
			if (players.Count > 1)
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
                return;
			}
			string message = players[0].Group.Prefix + args.Parameters[0] + players[0].Group.Suffix + ":";
			for (int i = 1; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}

			Color messagecolor = new Color(players[0].Group.R, players[0].Group.G, players[0].Group.B);
			TShock.Utils.Broadcast(message, messagecolor);
		}
		public static void SendRa(CommandArgs args)
		{
			if (args.Parameters.Count < 1)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendraw [something to send]", Color.Red);
				return;
			}
			string message = "";
			for (int i = 0; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}
			TShock.Utils.Broadcast(message);
			
            return;
		}
		public static void SendColor(CommandArgs args) //start new command for built-in colors
		{
			if(args.Parameters.Count < 2)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendcolor [colorname] message");
				return;
			}
			string message = "";
			for (int i = 1; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}
            string colorname = "" + args.Parameters[0];
            Color color = ColorFromName(colorname);
            if (color != new Color(1, 1, 1))
                TShock.Utils.Broadcast(message, color);
            else
                args.Player.SendMessage("Invalid color!");
            return;
		}
		public static void SendRGB(CommandArgs args) //start new command for custom colors by RGB
		{
			if(args.Parameters.Count < 4)
			{
				args.Player.SendMessage("Invalid syntax! Proper syntax: /sendcolor [Red] [Green] [Blue] [message]. Use 0-255 for RGB values");
				return;
			}
			string message = "";
			for (int i = 3; i < args.Parameters.Count; i++)
			{
				message += " " + args.Parameters[i];
			}

			byte R=Convert.ToByte(args.Parameters[0],10);
			byte G=Convert.ToByte(args.Parameters[1],10);
			byte B=Convert.ToByte(args.Parameters[2],10);
			TShock.Utils.Broadcast(message, new Color(R,G,B));
            return;
		}
		public static Color ColorFromName(string name) //sigh...you guys removed this
		{
            if (name == "aliceblue") return Color.AliceBlue;
            else if (name == "antiquewhite") return Color.AntiqueWhite;
            else if (name == "aqua") return Color.Aqua;
            else if (name == "aquamarine") return Color.Aquamarine;
            else if (name == "azure") return Color.Azure;
            else if (name == "beige") return Color.Beige;
            else if (name == "bisque") return Color.Bisque;
            else if (name == "black") return Color.Black;
            else if (name == "blanchedalmond") return Color.BlanchedAlmond;
            else if (name == "blue") return Color.Blue;
            else if (name == "blueviolet") return Color.BlueViolet;
            else if (name == "brown") return Color.Brown;
            else if (name == "burlywood") return Color.BurlyWood;
            else if (name == "cadetblue") return Color.CadetBlue;
            else if (name == "chartreuse") return Color.Chartreuse;
            else if (name == "chocolate") return Color.Chocolate;
            else if (name == "coral") return Color.Coral;
            else if (name == "cornflowerblue") return Color.CornflowerBlue;
            else if (name == "cornsilk") return Color.Cornsilk;
            else if (name == "crimson") return Color.Crimson;
            else if (name == "cyan") return Color.Cyan;
            else if (name == "darkblue") return Color.DarkBlue;
            else if (name == "darkcyan") return Color.DarkCyan;
            else if (name == "darkgoldenrod") return Color.DarkGoldenrod;
            else if (name == "darkgray") return Color.DarkGray;
            else if (name == "darkgreen") return Color.DarkGreen;
            else if (name == "darkkhaki") return Color.DarkKhaki;
            else if (name == "darkmagenta") return Color.DarkMagenta;
            else if (name == "darkolivegreen") return Color.DarkOliveGreen;
            else if (name == "darkorange") return Color.DarkOrange;
            else if (name == "darkorchid") return Color.DarkOrchid;
            else if (name == "darkred") return Color.DarkRed;
            else if (name == "darksalmon") return Color.DarkSalmon;
            else if (name == "darkseagreen") return Color.DarkSeaGreen;
            else if (name == "darkslateblue") return Color.DarkSlateBlue;
            else if (name == "darkslategray") return Color.DarkSlateGray;
            else if (name == "darkturquoise") return Color.DarkTurquoise;
            else if (name == "darkviolet") return Color.DarkViolet;
            else if (name == "deeppink") return Color.DeepPink;
            else if (name == "deepskyblue") return Color.DeepSkyBlue;
            else if (name == "dimgray") return Color.DimGray;
            else if (name == "dodgerblue") return Color.DodgerBlue;
            else if (name == "firebrick") return Color.Firebrick;
            else if (name == "floralwhite") return Color.FloralWhite;
            else if (name == "forestgreen") return Color.ForestGreen;
            else if (name == "fuchsia") return Color.Fuchsia;
            else if (name == "gainsboro") return Color.Gainsboro;
            else if (name == "ghostwhite") return Color.GhostWhite;
            else if (name == "gold") return Color.Gold;
            else if (name == "goldenrod") return Color.Goldenrod;
            else if (name == "grey") return Color.Gray;
            else if (name == "gray") return Color.Gray;
            else if (name == "green") return Color.Green;
            else if (name == "greenyellow") return Color.GreenYellow;
            else if (name == "honeydew") return Color.Honeydew;
            else if (name == "hotpink") return Color.HotPink;
            else if (name == "indianred") return Color.IndianRed;
            else if (name == "indigo") return Color.Indigo;
            else if (name == "ivory") return Color.Ivory;
            else if (name == "khaki") return Color.Khaki;
            else if (name == "lavender") return Color.Lavender;
            else if (name == "lavenderblush") return Color.LavenderBlush;
            else if (name == "lawngreen") return Color.LawnGreen;
            else if (name == "lemonchelse iffon") return Color.LemonChiffon;
            else if (name == "lightblue") return Color.LightBlue;
            else if (name == "lightcoral") return Color.LightCoral;
            else if (name == "lightcyan") return Color.LightCyan;
            else if (name == "lightgoldenrodyellow") return Color.LightGoldenrodYellow;
            else if (name == "lightgray") return Color.LightGray;
            else if (name == "lightgreen") return Color.LightGreen;
            else if (name == "lightpink") return Color.LightPink;
            else if (name == "lightsalmon") return Color.LightSalmon;
            else if (name == "lightseagreen") return Color.LightSeaGreen;
            else if (name == "lightskyblue") return Color.LightSkyBlue;
            else if (name == "lightslategray") return Color.LightSlateGray;
            else if (name == "lightsteelblue") return Color.LightSteelBlue;
            else if (name == "lightyellow") return Color.LightYellow;
            else if (name == "lime") return Color.Lime;
            else if (name == "limegreen") return Color.LimeGreen;
            else if (name == "linen") return Color.Linen;
            else if (name == "magenta") return Color.Magenta;
            else if (name == "maroon") return Color.Maroon;
            else if (name == "mediumaquamarine") return Color.MediumAquamarine;
            else if (name == "mediumblue") return Color.MediumBlue;
            else if (name == "mediumorchid") return Color.MediumOrchid;
            else if (name == "mediumpurple") return Color.MediumPurple;
            else if (name == "mediumseagreen") return Color.MediumSeaGreen;
            else if (name == "mediumslateblue") return Color.MediumSlateBlue;
            else if (name == "mediumspringgreen") return Color.MediumSpringGreen;
            else if (name == "mediumturquoise") return Color.MediumTurquoise;
            else if (name == "mediumvioletred") return Color.MediumVioletRed;
            else if (name == "midnightblue") return Color.MidnightBlue;
            else if (name == "mintcream") return Color.MintCream;
            else if (name == "mistyrose") return Color.MistyRose;
            else if (name == "moccasin") return Color.Moccasin;
            else if (name == "navajowhite") return Color.NavajoWhite;
            else if (name == "navy") return Color.Navy;
            else if (name == "oldlace") return Color.OldLace;
            else if (name == "olive") return Color.Olive;
            else if (name == "olivedrab") return Color.OliveDrab;
            else if (name == "orange") return Color.Orange;
            else if (name == "orangered") return Color.OrangeRed;
            else if (name == "orchid") return Color.Orchid;
            else if (name == "palegoldenrod") return Color.PaleGoldenrod;
            else if (name == "palegreen") return Color.PaleGreen;
            else if (name == "paleturquoise") return Color.PaleTurquoise;
            else if (name == "palevioletred") return Color.PaleVioletRed;
            else if (name == "papayawhip") return Color.PapayaWhip;
            else if (name == "peachpuff") return Color.PeachPuff;
            else if (name == "peru") return Color.Peru;
            else if (name == "pink") return Color.Pink;
            else if (name == "plum") return Color.Plum;
            else if (name == "powderblue") return Color.PowderBlue;
            else if (name == "purple") return Color.Purple;
            else if (name == "red") return Color.Red;
            else if (name == "rosybrown") return Color.RosyBrown;
            else if (name == "royalblue") return Color.RoyalBlue;
            else if (name == "saddlebrown") return Color.SaddleBrown;
            else if (name == "salmon") return Color.Salmon;
            else if (name == "sandybrown") return Color.SandyBrown;
            else if (name == "seagreen") return Color.SeaGreen;
            else if (name == "seashell") return Color.SeaShell;
            else if (name == "sienna") return Color.Sienna;
            else if (name == "silver") return Color.Silver;
            else if (name == "skyblue") return Color.SkyBlue;
            else if (name == "slateblue") return Color.SlateBlue;
            else if (name == "slategray") return Color.SlateGray;
            else if (name == "snow") return Color.Snow;
            else if (name == "springgreen") return Color.SpringGreen;
            else if (name == "steelblue") return Color.SteelBlue;
            else if (name == "tan") return Color.Tan;
            else if (name == "teal") return Color.Teal;
            else if (name == "thistle") return Color.Thistle;
            else if (name == "tomato") return Color.Tomato;
            else if (name == "transparent") return Color.Transparent;
            else if (name == "turquoise") return Color.Turquoise;
            else if (name == "violet") return Color.Violet;
            else if (name == "wheat") return Color.Wheat;
            else if (name == "white") return Color.White;
            else if (name == "whitesmoke") return Color.WhiteSmoke;
            else if (name == "yellow") return Color.Yellow;
            else if (name == "yellowgreen") return Color.YellowGreen; 
            else return new Color(1, 1, 1);
		}
	}
}