using System;
using System.IO;
using System.Text;

public class CPHInline
{
	public bool Execute()
	{
	
		var nextPlayersFile = args["nextPlayersFile"].ToString();
		var listFile = args["listFile"].ToString();

		// create empty files if not exists
		if (!File.Exists(nextPlayersFile))
		{
			File.Create(nextPlayersFile).Close();
		}

		if (!File.Exists(listFile))
		{
			File.Create(listFile).Close();
		}

		var entry = args["input0"].ToString();

		if (entry == "clear")
		{
			File.WriteAllText(listFile, string.Empty);
		}

		// set default value of amount of highlighted participants
		var nextPlayers = System.IO.File.ReadAllText(nextPlayersFile);
		
		if (nextPlayers == "")
		{
			nextPlayers = "3";
		}

		if (entry != "clear" && entry != "")
		{
			nextPlayers = entry;
		}
		
		CPH.SetArgument("nextPlayers",nextPlayers);

		return true;
	}
}
