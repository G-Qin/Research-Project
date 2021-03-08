using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SIAMManager : MonoBehaviour
{
    public float targetPerformance;
    public int maxTrialNum;
    public int stepSize;  
    public int targetReversalNum, targetOfChange, discardReversalNum;
    public int incrementOfDecibel;
    // Above are config variables    
    public GameObject dataLogger, playSoundTxt, trialText;
    public Button playSoundBtn, noBtn, yesBtn, finishBtn;  
    [SerializeField]
    private float _volume = 1f;
    private int reversalNum = 0, trialNum = 0;
    private bool signalExist = false, volIncrease = false;
    void Start()
    {
        dataLogger.GetComponent<DataLoggerScript>().NewSIAMDataFile();
        trialText.GetComponent<Text>().text = "Trial #1";
    }

    public void PlaySound(bool exist)
    {       
        dataLogger.GetComponent<DataLoggerScript>().LogVolume(volume);
        playSoundTxt.GetComponent<PlaySoundTxtScript>().AwakeOnPlaySound();
        signalExist = exist;
    }

    // Adjust the volume for next trial based on SIAM payoff matrix
    public void UpdateVolume(bool response){
        bool currTrend = true;
        float dB = LinearToDecibel(volume);
        // Hit
        if (response && signalExist){
            dB -= 1 * stepSize;
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Hit");
            // Hit will decrease the volume
            currTrend = false;
        } else if (!response && signalExist){ // Miss
            dB += targetPerformance / (1f - targetPerformance) * stepSize;
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Miss");
        } else if (response && !signalExist){ // False alarm
            dB += 1f/(1f-targetPerformance) * stepSize;
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Flase alarm");
        } else if (!response && !signalExist){ // Correct rejection
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Correct rejection");
            // Correct rejection does not have effect on reversals
            currTrend = volIncrease;            
        }
        if (currTrend != volIncrease){// A reversal occurs
            volIncrease = currTrend;
            reversalNum ++;
            dataLogger.GetComponent<DataLoggerScript>().LogReversal(reversalNum);
            // For debug use
            Debug.Log("Reversal");
        }
        if (reversalNum == targetOfChange) {
            targetPerformance = 0.5f;
        }
        if (reversalNum >= targetReversalNum) {
            TerminateProcedure();
        }
        volume = DecibelToLinear(dB);
    }

    public void RevealYesNoButtons(){
        yesBtn.GetComponent<YesButtonScript>().AwakeOnSoundEnd();
        noBtn.GetComponent<NoButtonScript>().AwakeOnSoundEnd();
        playSoundTxt.GetComponent<PlaySoundTxtScript>().SoundFinishText();
    }

    public void IncrementTrialNumber()
    {   
        // Log trial number
        dataLogger.GetComponent<DataLoggerScript>().LogTrialNumber(trialNum);
        // Update trial number
        trialNum ++;
        trialText.GetComponent<Text>().text = "Trial #" + trialNum.ToString();
        // Activate trial number when procedure is complete
        if (trialNum > maxTrialNum) {
            TerminateProcedure();
        }
    }

    private void TerminateProcedure(){
        dataLogger.GetComponent<DataLoggerScript>().LogFinishedProcedure(volume);
        trialText.GetComponent<Text>().text = "SIAM Procedure Finished.";
        playSoundBtn.interactable = false;
        finishBtn.GetComponent<FinishBtnScript>().AwakeAtFinish();
    }

    private static float LinearToDecibel(float linear)
    {   // Convert linear volume to decibels
        float dB;
         
        if (linear != 0) dB = 20.0f * Mathf.Log10(linear);
        else if (linear >= 1) dB = 0f;
        else dB = -144.0f; 

        return dB;
    }

    private static float DecibelToLinear(float dB)
    {   // Convert decibels to linear volume
        float linear = Mathf.Pow(10.0f, dB/20.0f);
        if (linear > 1) linear = 1;
        return linear;
    }

    public float volume{
        get {return _volume;}
        set {_volume = value;}
    }
}
