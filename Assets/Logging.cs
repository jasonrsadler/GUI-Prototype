using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Logging
{

    public static void Log(string stringToLog)
    {
        if (!File.Exists(Application.persistentDataPath + "/log.txt"))
        {
            FileStream stream = File.Create(Application.persistentDataPath + "/log.txt");
            stream.Close();
        }
        StringBuilder stringBuilder = new StringBuilder(File.ReadAllText(Application.persistentDataPath + "/log.txt"));
        stringBuilder.AppendLine(stringToLog);
        File.WriteAllText(Application.persistentDataPath + "/log.txt", stringBuilder.ToString());
        Debug.Log(stringToLog);
    }
}
