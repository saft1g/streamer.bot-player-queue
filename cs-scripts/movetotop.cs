using System;
using System.IO;
using System.Linq;

public class CPHInline
{
    public bool Execute()
    {
        var listFile = args["listFile"].ToString();
        var entry = args["input0"].ToString();
        int matchLine = 0;
        int currentLine = 0;
        int totallines = 1;
        string line;
        string matchedLine = "";
        string tempFile = Path.GetTempFileName();
        var injectedline = false;
        using (var sw = new StreamWriter(tempFile))
        {
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine.ToString() == entry)
                    {
                        injectedline = true;
                        sw.Write(line);
                        CPH.SendMessage("Moving entry #" + currentLine + " to the top of the queue.");
                    }
                }
			currentLine = 0;
			if (!injectedline){
			CPH.SendMessage("That's not a valid entry to move to top.");
			return false;
			}
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine.ToString() != entry)
                    {
                        if ((currentLine == 1) && !injectedline)
                        {
                            sw.Write(line);
                        }
                        else
                        {
                            sw.Write("\r\n" + line);
                        }
                    }
                }
        }

        if (currentLine == 0)
        {
            CPH.SendMessage("There are no more entries to go through");
            File.Delete(tempFile);
            return false;
        }

        File.Delete(listFile);
        if (currentLine == 2 && !injectedline)
        {
            File.Delete(tempFile);
        }
        else
        {
            File.Move(tempFile, listFile);
        }

        return true;
    }
}