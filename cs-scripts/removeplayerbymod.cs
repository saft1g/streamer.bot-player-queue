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
        using (var sw = new StreamWriter(tempFile))
        {
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine.ToString() != entry)
                    {
                        {
                            if ((currentLine == 1) || (entry=="1" && currentLine==2))
                            {
                                sw.Write(line);
                            }
                            else
                            {
                                sw.Write("\r\n" + line);
                            }
                        }
                    }
                    else
                    {
                        CPH.SendMessage("Removing entry #" + entry + ": " + line);
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
        if (currentLine == 2)
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