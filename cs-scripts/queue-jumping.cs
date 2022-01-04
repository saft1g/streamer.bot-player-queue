using System;
using System.IO;
using System.Linq;

public class CPHInline
{
    public bool Execute()
    {
        var listFile = args["listFile"].ToString();
        var user = args["user"].ToString();
        int currentLine = 0;
        string line;
        string tempFile = Path.GetTempFileName();
        var injectedline = false;
        using (var sw = new StreamWriter(tempFile))
        {
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    currentLine++;
                    if (line == user)
                    {
                        injectedline = true;
                        sw.Write(line);
                        CPH.SendMessage("@" + user + " fährt die Ellebogen aus und reiht sich ganz vorne in der Schlange ein!");
                    }
                }
			if (!injectedline){
			CPH.SendMessage("Ups, finde dich in der Liste nicht @" +user);
			return false;
			}
            using (var sr = new StreamReader(listFile))
                while ((line = sr.ReadLine()) != null)
                {
                    if ((line != user))
                    {
                        sw.Write("\r\n" + line);
                    }
                }
        }

        if (currentLine == 0)
        {
            CPH.SendMessage("Liste ist leer");
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