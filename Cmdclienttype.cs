using System;
using System.Collections.Generic;
using System.Text;

namespace MCGalaxy
{
	public class CmdClienttype : Command
	{
		public override string name { get { return "Clienttype"; } }

		public override string shortcut { get { return ""; } }

		public override string type { get { return "other"; } }

		public override bool museumUsable { get { return true; } }

		public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

		public override void Use(Player p, string message)
		{
            Dictionary<string, List<Player>> clients = new Dictionary<string, List<Player>>();
            Dictionary<string, List<Player>> bcclients = new Dictionary<string, List<Player>>();
            Player[] online = PlayerInfo.Online.Items;

            foreach (Player pl in online)
            {
                string appName = PlayerInfo.ClientName(pl);

                List<Player> usingClient;
                List<Player> usingBCClient;
                if (appName != null && pl.name.Contains("+"))
                {
                    if (!clients.TryGetValue(appName, out usingClient))
                    {
                        usingClient = new List<Player>();
                        clients[appName] = usingClient;
                    }
                    usingClient.Add(pl);
                }
                else
                {
                    if (!bcclients.TryGetValue(appName, out usingBCClient))
                    {
                        usingBCClient = new List<Player>();
                        bcclients[appName] = usingBCClient;
                    }
                    usingBCClient.Add(pl);
                }
                    
                
                     
            }

            p.Message("Players using Classicube Client:");
            foreach (var kvp in clients)
            {
                StringBuilder builder = new StringBuilder();
                List<Player> players = kvp.Value;

                for (int i = 0; i < players.Count; i++)
                {
                    string nick = Colors.StripUsed(p.FormatNick(players[i]));
                    builder.Append(nick);
                    if (i < players.Count - 1) builder.Append(", ");
                }
       
                p.Message("&f{0}", builder.ToString());   
            }
            p.Message("Players using Betacraft Client:");
            foreach (var kvp in bcclients)
            {
                StringBuilder builder = new StringBuilder();
                List<Player> players = kvp.Value;

                for (int i = 0; i < players.Count; i++)
                {
                    string nick = Colors.StripUsed(p.FormatNick(players[i]));
                    builder.Append(nick);
                    if (i < players.Count - 1) builder.Append(", ");
                }

                p.Message("&f{0}", builder.ToString());
            }
        }

		public override void Help(Player p)
		{
			p.Message("/Clienttype - Shows which Client Type a player is on.");
		}
	}
}
