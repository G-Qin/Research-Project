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
        path = SIAMLogPath + date + ".csv"; 
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Volume,Trial #,Response");
        }
    }

    public void LogTrialNumber(int trialNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write(trialNum.ToString() + ",");
        }
    }

    public void LogVolume(float volume){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write(volume.ToString() + ",");
        }
    }

    public void LogResponse(string response){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine(response);
        }
    }

    public void LogReversal(int reversalNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Reversal #" + reversalNum);
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
