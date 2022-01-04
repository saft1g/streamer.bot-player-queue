using System;

public class CPHInline
{
	public bool Execute()
	{
		var user = args["user"].ToString();
		var listFile = args["listFile"].ToString();
        // go through each line of the file
        // Read the file and display it line by line.  
        foreach (string line in System.IO.File.ReadLines(listFile))
        {
			
            if (line == user)
            {
				CPH.SendMessage ("@"+user+", du bist bereits eingetragen.");
               return false;
            }
        }
		
		return true;
	}
}
