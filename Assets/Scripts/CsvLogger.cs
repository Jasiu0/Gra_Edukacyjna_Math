using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using UnityEngine;


class CsvLogger {
    //PC filename
    //const string FILENAME = "event_log.csv";
    //Android filename
    static string FILENAME = Application.persistentDataPath + "/event_log.csv";

    public static void LogEvent(string eventMsg) {
        string time = DateTime.Now.ToString("HH:mm:ss");

        if (!File.Exists(FILENAME)) {
            StreamWriter headerWriter = File.AppendText(FILENAME);
            headerWriter.WriteLine("TIME" + "," + "EVENT");
            headerWriter.Close();
        }

        StreamWriter writer = File.AppendText(FILENAME);
        writer.WriteLine(time + "," + eventMsg);
        writer.Close();
    }
}



