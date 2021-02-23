using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataLoggerScript : MonoBehaviour
{
    StreamWriter writer;
    private string SIAMLogPath = @"Logs\SIAMLogs\";
    string path;

    void Start(){
        // Create folder for the log files
        if (!Directory.Exists(SIAMLogPath)){
            Directory.CreateDirectory(SIAMLogPath);
        }
    }

    public void NewSIAMDataFile()
    {   
        // Create a new file name using current time
        string date = DateTime.Now.ToString("MM-dd-yy HH-mm-ss");
        path = SIAMLogPath + date + ".txt"; 
    }

    public void LogTrialNumber(int trialNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Trial #" + trialNum.ToString() + "  ");
        }
    }

    public void LogVolume(float volume){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Volume: " + volume.ToString() + "  ");
        }
    }

    public void LogResponse(string response){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write(response + "\n");
        }
    }

    public void LogReversal(int reversalNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Reversal #" + reversalNum + "\n");
        }
    }

    public void LogAbortedProcedure(){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Aborted Procedure.");
        }
    }

    public void LogFinishedProcedure(float volume){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Finished Procedure, volume: " + volume);
        }
    }
}
