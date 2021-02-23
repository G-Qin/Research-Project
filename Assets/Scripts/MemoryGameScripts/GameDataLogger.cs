using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameDataLogger : MonoBehaviour
{
    private string MemGameLogPath = @"Logs\MemGameLogs\";
    string path;
    StreamWriter writer;
    float accuracy;

    // Start is called before the first frame update
    void Start()
    {
        // Create folder for the log files
        if (!Directory.Exists(MemGameLogPath)){
            Directory.CreateDirectory(MemGameLogPath);
        }
    }

    public void NewMemGameLogFile(){
        // Create a new file name using current time
        string date = DateTime.Now.ToString("MM-dd-yy HH-mm-ss");
        path = MemGameLogPath + date + ".txt"; 
    }

    public void LogHit(int score, int flipNum){
        accuracy = (float)score / (float)flipNum;
        LogTime();
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Hit , Score: " + score + 
                ", Accuracy: " + (accuracy * 100).ToString("0.00"));
        }
    }

    public void LogMiss(int score, int flipNum){
        accuracy = (float)score / (float)flipNum;
        
        LogTime();
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Miss, Score: " + score + 
                ", Accuracy: " + (accuracy * 100).ToString("0.00"));
        }
    }

    private void LogTime(){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss  "));
        }
    }

    public void LogAbortedGame(){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Aborted Game.");
        }
    }

    public void LogReset(){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Reset.");
        }
    }
}
