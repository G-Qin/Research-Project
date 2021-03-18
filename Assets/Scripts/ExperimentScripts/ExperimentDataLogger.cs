using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ExperimentDataLogger : MonoBehaviour
{
    private string ExperimentLogPath = @"Logs\ExperimentLogs\";
    string path;
    StreamWriter writer;
    void Start()
    {
        // Create folder for the log files
        if (!Directory.Exists(ExperimentLogPath)){
            Directory.CreateDirectory(ExperimentLogPath);
        }
    }

    public void NewExperimentLogFile(){
        // Create a new file name using current time
        string date = DateTime.Now.ToString("MM-dd-yy HH-mm-ss");
        path = ExperimentLogPath + date + ".csv";
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Trial#,Signal,Response");
        } 
    }

    public void LogTrialNumber(int trialNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write(trialNum + ",");
        } 
    }

    public void LogSignal(bool exist){
        // Use write instead of writeline for next column
        using (writer = new StreamWriter(path, append:true)){
            if (exist) writer.Write("True,");
            else writer.Write("False,");
        } 
    }

    public void LogResponse(int res){
        // 1 is Hit, 2 is Miss, 3 is False Alarm, 4 is Correct Rejection
        // Use writeline here as it's last thing in the row
        using (writer = new StreamWriter(path, append:true)){
            if (res==1) writer.WriteLine("Hit");
            else if (res==2) writer.WriteLine("Miss");
            else if (res==3) writer.WriteLine("False Alarm");
            else if (res==4) writer.WriteLine("Correct Rejection");
            else writer.WriteLine("Error");
        } 
    }
}
