using System;
using System.IO;

public class CPHInline
{
    public bool Execute()
    {

		var nextPlayersFile = args["nextPlayersFile"].ToString();

		int nextPlayers = Int32.Parse(System.IO.File.ReadAllText(nextPlayersFile));

        var listFile = args["listFile"].ToString();
        int counter = 1;
		var queueList = "";
        foreach (string line in System.IO.File.ReadLines(listFile))
        {
            if (counter <= nextPlayers)
            {
				CPH.SendMessage("#"+counter.ToString()+" - "+line);
            }
			if (counter > nextPlayers)
			{
				queueList += "[#"+counter+ " "+line+ "] ";
			}

            counter++;
        }

		CPH.SendMessage("Warteliste: "+queueList);

        return true;
    }
}