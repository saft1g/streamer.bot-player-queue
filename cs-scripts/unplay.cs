using System;
using System.IO;
using System.Linq;

public class CPHInline
{
    public bool Execute()
    {
        var listFile = args["listFile"].ToString();
        var user = args["user"].ToString();
        int matchLine = 0;
        int currentLine = 0;
        string line;
        string tempFile = Path.GetTempFileName();

        using (var sw = new StreamWriter(tempFile))
        {
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if ((currentLine == 1 && line != user))
                    {
                        sw.Write(line);
                    }
                    if (currentLine != 1 && line != user)
                    {
                        sw.Write("\r\n" + line);
                    }
                    if ((currentLine == 1 && line == user))
                    {
                        currentLine--;
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
        if (currentLine == 1)
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