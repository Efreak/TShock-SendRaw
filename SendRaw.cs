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

			Color messagecolor = new Color(players[0].Group.R, players[0].Group.G, players[0].Group.B); //thanks, snirk
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
			
			Color color = new Color(0,0,0);
			color.R=Convert.ToByte(args.Parameters[0],10);
			color.G=Convert.ToByte(args.Parameters[1],10);
			color.B=Convert.ToByte(args.Parameters[2],10);
			TShock.Utils.Broadcast(message, color);
            return;
		}
		public static Color ColorFromName(string name) //sigh...you guys removed this
		{
            if (name.Equals("aliceblue", StringComparison.OrdinalIgnoreCase)) return Color.AliceBlue;
            else if (name.Equals("antiquewhite", StringComparison.OrdinalIgnoreCase)) return Color.AntiqueWhite;
            else if (name.Equals("aqua", StringComparison.OrdinalIgnoreCase)) return Color.Aqua;
            else if (name.Equals("aquamarine", StringComparison.OrdinalIgnoreCase)) return Color.Aquamarine;
            else if (name.Equals("azure", StringComparison.OrdinalIgnoreCase)) return Color.Azure;
            else if (name.Equals("beige", StringComparison.OrdinalIgnoreCase)) return Color.Beige;
            else if (name.Equals("bisque", StringComparison.OrdinalIgnoreCase)) return Color.Bisque;
            else if (name.Equals("black", StringComparison.OrdinalIgnoreCase)) return Color.Black;
            else if (name.Equals("blanchedalmond", StringComparison.OrdinalIgnoreCase)) return Color.BlanchedAlmond;
            else if (name.Equals("blue", StringComparison.OrdinalIgnoreCase)) return Color.Blue;
            else if (name.Equals("blueviolet", StringComparison.OrdinalIgnoreCase)) return Color.BlueViolet;
            else if (name.Equals("brown", StringComparison.OrdinalIgnoreCase)) return Color.Brown;
            else if (name.Equals("burlywood", StringComparison.OrdinalIgnoreCase)) return Color.BurlyWood;
            else if (name.Equals("cadetblue", StringComparison.OrdinalIgnoreCase)) return Color.CadetBlue;
            else if (name.Equals("chartreuse", StringComparison.OrdinalIgnoreCase)) return Color.Chartreuse;
            else if (name.Equals("chocolate", StringComparison.OrdinalIgnoreCase)) return Color.Chocolate;
            else if (name.Equals("coral", StringComparison.OrdinalIgnoreCase)) return Color.Coral;
            else if (name.Equals("cornflowerblue", StringComparison.OrdinalIgnoreCase)) return Color.CornflowerBlue;
            else if (name.Equals("cornsilk", StringComparison.OrdinalIgnoreCase)) return Color.Cornsilk;
            else if (name.Equals("crimson", StringComparison.OrdinalIgnoreCase)) return Color.Crimson;
            else if (name.Equals("cyan", StringComparison.OrdinalIgnoreCase)) return Color.Cyan;
            else if (name.Equals("darkblue", StringComparison.OrdinalIgnoreCase)) return Color.DarkBlue;
            else if (name.Equals("darkcyan", StringComparison.OrdinalIgnoreCase)) return Color.DarkCyan;
            else if (name.Equals("darkgoldenrod", StringComparison.OrdinalIgnoreCase)) return Color.DarkGoldenrod;
            else if (name.Equals("darkgray", StringComparison.OrdinalIgnoreCase)) return Color.DarkGray;
            else if (name.Equals("darkgreen", StringComparison.OrdinalIgnoreCase)) return Color.DarkGreen;
            else if (name.Equals("darkkhaki", StringComparison.OrdinalIgnoreCase)) return Color.DarkKhaki;
            else if (name.Equals("darkmagenta", StringComparison.OrdinalIgnoreCase)) return Color.DarkMagenta;
            else if (name.Equals("darkolivegreen", StringComparison.OrdinalIgnoreCase)) return Color.DarkOliveGreen;
            else if (name.Equals("darkorange", StringComparison.OrdinalIgnoreCase)) return Color.DarkOrange;
            else if (name.Equals("darkorchid", StringComparison.OrdinalIgnoreCase)) return Color.DarkOrchid;
            else if (name.Equals("darkred", StringComparison.OrdinalIgnoreCase)) return Color.DarkRed;
            else if (name.Equals("darksalmon", StringComparison.OrdinalIgnoreCase)) return Color.DarkSalmon;
            else if (name.Equals("darkseagreen", StringComparison.OrdinalIgnoreCase)) return Color.DarkSeaGreen;
            else if (name.Equals("darkslateblue", StringComparison.OrdinalIgnoreCase)) return Color.DarkSlateBlue;
            else if (name.Equals("darkslategray", StringComparison.OrdinalIgnoreCase)) return Color.DarkSlateGray;
            else if (name.Equals("darkturquoise", StringComparison.OrdinalIgnoreCase)) return Color.DarkTurquoise;
            else if (name.Equals("darkviolet", StringComparison.OrdinalIgnoreCase)) return Color.DarkViolet;
            else if (name.Equals("deeppink", StringComparison.OrdinalIgnoreCase)) return Color.DeepPink;
            else if (name.Equals("deepskyblue", StringComparison.OrdinalIgnoreCase)) return Color.DeepSkyBlue;
            else if (name.Equals("dimgray", StringComparison.OrdinalIgnoreCase)) return Color.DimGray;
            else if (name.Equals("dodgerblue", StringComparison.OrdinalIgnoreCase)) return Color.DodgerBlue;
            else if (name.Equals("firebrick", StringComparison.OrdinalIgnoreCase)) return Color.Firebrick;
            else if (name.Equals("floralwhite", StringComparison.OrdinalIgnoreCase)) return Color.FloralWhite;
            else if (name.Equals("forestgreen", StringComparison.OrdinalIgnoreCase)) return Color.ForestGreen;
            else if (name.Equals("fuchsia", StringComparison.OrdinalIgnoreCase)) return Color.Fuchsia;
            else if (name.Equals("gainsboro", StringComparison.OrdinalIgnoreCase)) return Color.Gainsboro;
            else if (name.Equals("ghostwhite", StringComparison.OrdinalIgnoreCase)) return Color.GhostWhite;
            else if (name.Equals("gold", StringComparison.OrdinalIgnoreCase)) return Color.Gold;
            else if (name.Equals("goldenrod", StringComparison.OrdinalIgnoreCase)) return Color.Goldenrod;
            else if (name.Equals("gray", StringComparison.OrdinalIgnoreCase)) return Color.Gray;
            else if (name.Equals("gray", StringComparison.OrdinalIgnoreCase)) return Color.Gray;
            else if (name.Equals("green", StringComparison.OrdinalIgnoreCase)) return Color.Green;
            else if (name.Equals("greenyellow", StringComparison.OrdinalIgnoreCase)) return Color.GreenYellow;
            else if (name.Equals("honeydew", StringComparison.OrdinalIgnoreCase)) return Color.Honeydew;
            else if (name.Equals("hotpink", StringComparison.OrdinalIgnoreCase)) return Color.HotPink;
            else if (name.Equals("indianred", StringComparison.OrdinalIgnoreCase)) return Color.IndianRed;
            else if (name.Equals("indigo", StringComparison.OrdinalIgnoreCase)) return Color.Indigo;
            else if (name.Equals("ivory", StringComparison.OrdinalIgnoreCase)) return Color.Ivory;
            else if (name.Equals("khaki", StringComparison.OrdinalIgnoreCase)) return Color.Khaki;
            else if (name.Equals("lavender", StringComparison.OrdinalIgnoreCase)) return Color.Lavender;
            else if (name.Equals("lavenderblush", StringComparison.OrdinalIgnoreCase)) return Color.LavenderBlush;
            else if (name.Equals("lawngreen", StringComparison.OrdinalIgnoreCase)) return Color.LawnGreen;
            else if (name.Equals("lemonchelse iffon", StringComparison.OrdinalIgnoreCase)) return Color.LemonChiffon;
            else if (name.Equals("lightblue", StringComparison.OrdinalIgnoreCase)) return Color.LightBlue;
            else if (name.Equals("lightcoral", StringComparison.OrdinalIgnoreCase)) return Color.LightCoral;
            else if (name.Equals("lightcyan", StringComparison.OrdinalIgnoreCase)) return Color.LightCyan;
            else if (name.Equals("lightgoldenrodyellow", StringComparison.OrdinalIgnoreCase)) return Color.LightGoldenrodYellow;
            else if (name.Equals("lightgray", StringComparison.OrdinalIgnoreCase)) return Color.LightGray;
            else if (name.Equals("lightgreen", StringComparison.OrdinalIgnoreCase)) return Color.LightGreen;
            else if (name.Equals("lightpink", StringComparison.OrdinalIgnoreCase)) return Color.LightPink;
            else if (name.Equals("lightsalmon", StringComparison.OrdinalIgnoreCase)) return Color.LightSalmon;
            else if (name.Equals("lightseagreen", StringComparison.OrdinalIgnoreCase)) return Color.LightSeaGreen;
            else if (name.Equals("lightskyblue", StringComparison.OrdinalIgnoreCase)) return Color.LightSkyBlue;
            else if (name.Equals("lightslategray", StringComparison.OrdinalIgnoreCase)) return Color.LightSlateGray;
            else if (name.Equals("lightsteelblue", StringComparison.OrdinalIgnoreCase)) return Color.LightSteelBlue;
            else if (name.Equals("lightyellow", StringComparison.OrdinalIgnoreCase)) return Color.LightYellow;
            else if (name.Equals("lime", StringComparison.OrdinalIgnoreCase)) return Color.Lime;
            else if (name.Equals("limegreen", StringComparison.OrdinalIgnoreCase)) return Color.LimeGreen;
            else if (name.Equals("linen", StringComparison.OrdinalIgnoreCase)) return Color.Linen;
            else if (name.Equals("magenta", StringComparison.OrdinalIgnoreCase)) return Color.Magenta;
            else if (name.Equals("maroon", StringComparison.OrdinalIgnoreCase)) return Color.Maroon;
            else if (name.Equals("mediumaquamarine", StringComparison.OrdinalIgnoreCase)) return Color.MediumAquamarine;
            else if (name.Equals("mediumblue", StringComparison.OrdinalIgnoreCase)) return Color.MediumBlue;
            else if (name.Equals("mediumorchid", StringComparison.OrdinalIgnoreCase)) return Color.MediumOrchid;
            else if (name.Equals("mediumpurple", StringComparison.OrdinalIgnoreCase)) return Color.MediumPurple;
            else if (name.Equals("mediumseagreen", StringComparison.OrdinalIgnoreCase)) return Color.MediumSeaGreen;
            else if (name.Equals("mediumslateblue", StringComparison.OrdinalIgnoreCase)) return Color.MediumSlateBlue;
            else if (name.Equals("mediumspringgreen", StringComparison.OrdinalIgnoreCase)) return Color.MediumSpringGreen;
            else if (name.Equals("mediumturquoise", StringComparison.OrdinalIgnoreCase)) return Color.MediumTurquoise;
            else if (name.Equals("mediumvioletred", StringComparison.OrdinalIgnoreCase)) return Color.MediumVioletRed;
            else if (name.Equals("midnightblue", StringComparison.OrdinalIgnoreCase)) return Color.MidnightBlue;
            else if (name.Equals("mintcream", StringComparison.OrdinalIgnoreCase)) return Color.MintCream;
            else if (name.Equals("mistyrose", StringComparison.OrdinalIgnoreCase)) return Color.MistyRose;
            else if (name.Equals("moccasin", StringComparison.OrdinalIgnoreCase)) return Color.Moccasin;
            else if (name.Equals("navajowhite", StringComparison.OrdinalIgnoreCase)) return Color.NavajoWhite;
            else if (name.Equals("navy", StringComparison.OrdinalIgnoreCase)) return Color.Navy;
            else if (name.Equals("oldlace", StringComparison.OrdinalIgnoreCase)) return Color.OldLace;
            else if (name.Equals("olive", StringComparison.OrdinalIgnoreCase)) return Color.Olive;
            else if (name.Equals("olivedrab", StringComparison.OrdinalIgnoreCase)) return Color.OliveDrab;
            else if (name.Equals("orange", StringComparison.OrdinalIgnoreCase)) return Color.Orange;
            else if (name.Equals("orangered", StringComparison.OrdinalIgnoreCase)) return Color.OrangeRed;
            else if (name.Equals("orchid", StringComparison.OrdinalIgnoreCase)) return Color.Orchid;
            else if (name.Equals("palegoldenrod", StringComparison.OrdinalIgnoreCase)) return Color.PaleGoldenrod;
            else if (name.Equals("palegreen", StringComparison.OrdinalIgnoreCase)) return Color.PaleGreen;
            else if (name.Equals("paleturquoise", StringComparison.OrdinalIgnoreCase)) return Color.PaleTurquoise;
            else if (name.Equals("palevioletred", StringComparison.OrdinalIgnoreCase)) return Color.PaleVioletRed;
            else if (name.Equals("papayawhip", StringComparison.OrdinalIgnoreCase)) return Color.PapayaWhip;
            else if (name.Equals("peachpuff", StringComparison.OrdinalIgnoreCase)) return Color.PeachPuff;
            else if (name.Equals("peru", StringComparison.OrdinalIgnoreCase)) return Color.Peru;
            else if (name.Equals("pink", StringComparison.OrdinalIgnoreCase)) return Color.Pink;
            else if (name.Equals("plum", StringComparison.OrdinalIgnoreCase)) return Color.Plum;
            else if (name.Equals("powderblue", StringComparison.OrdinalIgnoreCase)) return Color.PowderBlue;
            else if (name.Equals("purple", StringComparison.OrdinalIgnoreCase)) return Color.Purple;
            else if (name.Equals("red", StringComparison.OrdinalIgnoreCase)) return Color.Red;
            else if (name.Equals("rosybrown", StringComparison.OrdinalIgnoreCase)) return Color.RosyBrown;
            else if (name.Equals("royalblue", StringComparison.OrdinalIgnoreCase)) return Color.RoyalBlue;
            else if (name.Equals("saddlebrown", StringComparison.OrdinalIgnoreCase)) return Color.SaddleBrown;
            else if (name.Equals("salmon", StringComparison.OrdinalIgnoreCase)) return Color.Salmon;
            else if (name.Equals("sandybrown", StringComparison.OrdinalIgnoreCase)) return Color.SandyBrown;
            else if (name.Equals("seagreen", StringComparison.OrdinalIgnoreCase)) return Color.SeaGreen;
            else if (name.Equals("seashell", StringComparison.OrdinalIgnoreCase)) return Color.SeaShell;
            else if (name.Equals("sienna", StringComparison.OrdinalIgnoreCase)) return Color.Sienna;
            else if (name.Equals("silver", StringComparison.OrdinalIgnoreCase)) return Color.Silver;
            else if (name.Equals("skyblue", StringComparison.OrdinalIgnoreCase)) return Color.SkyBlue;
            else if (name.Equals("slateblue", StringComparison.OrdinalIgnoreCase)) return Color.SlateBlue;
            else if (name.Equals("slategray", StringComparison.OrdinalIgnoreCase)) return Color.SlateGray;
            else if (name.Equals("snow", StringComparison.OrdinalIgnoreCase)) return Color.Snow;
            else if (name.Equals("springgreen", StringComparison.OrdinalIgnoreCase)) return Color.SpringGreen;
            else if (name.Equals("steelblue", StringComparison.OrdinalIgnoreCase)) return Color.SteelBlue;
            else if (name.Equals("tan", StringComparison.OrdinalIgnoreCase)) return Color.Tan;
            else if (name.Equals("teal", StringComparison.OrdinalIgnoreCase)) return Color.Teal;
            else if (name.Equals("thistle", StringComparison.OrdinalIgnoreCase)) return Color.Thistle;
            else if (name.Equals("tomato", StringComparison.OrdinalIgnoreCase)) return Color.Tomato;
            else if (name.Equals("transparent", StringComparison.OrdinalIgnoreCase)) return Color.Transparent;
            else if (name.Equals("turquoise", StringComparison.OrdinalIgnoreCase)) return Color.Turquoise;
            else if (name.Equals("violet", StringComparison.OrdinalIgnoreCase)) return Color.Violet;
            else if (name.Equals("wheat", StringComparison.OrdinalIgnoreCase)) return Color.Wheat;
            else if (name.Equals("white", StringComparison.OrdinalIgnoreCase)) return Color.White;
            else if (name.Equals("whitesmoke", StringComparison.OrdinalIgnoreCase)) return Color.WhiteSmoke;
            else if (name.Equals("yellow", StringComparison.OrdinalIgnoreCase)) return Color.Yellow;
            else if (name.Equals("yellowgreen", StringComparison.OrdinalIgnoreCase)) return Color.YellowGreen; 
            else return new Color(1, 1, 1);
		}
	}
}