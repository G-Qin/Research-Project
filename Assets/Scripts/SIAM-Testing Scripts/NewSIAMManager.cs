using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewSIAMManager : MonoBehaviour
{
    public float targetPerformance, firstQuota, secondQuota;
    public int maxTrialNum, incrementOfDecibel;
    public int stepSize, targetReversalNum, discardReversalNum;
    public float noisePlayLength;
    // Above are config variables
    public AudioSource noise, signal;
    public Button yesBtn, noBtn, startBtn, finishBtn, backBtn;
    public GameObject dataLogger;
    public Text trialNumText, playSoundText;
    [SerializeField]
    private float volume, totalVol, quota, noiseLength, signalWaitTime;
    private int reversalNum, trialNum, savedTrialNum, response;
    private bool signalExist, volIncrease, answered;
    
    void Start()
    {
        // Set up parameters
        volume = 0.5f;
        totalVol = 0f;
        signalExist = false;
        volIncrease = false;
        quota = firstQuota;
        noiseLength = noise.clip.length;
        trialNum = 1;
        reversalNum = 0;
        savedTrialNum = 0;
        // Interface adjustments
        yesBtn.interactable = false;
        noBtn.interactable = false;
        finishBtn.interactable = false;
        backBtn.interactable = false;
        playSoundText.text = "Not playing.";
        trialNumText.text = "";
    }

    public void StartProcedure()
    {
        backBtn.interactable = true;
        startBtn.interactable = false;
        dataLogger.GetComponent<DataLoggerScript>().NewSIAMDataFile();
        StartCoroutine(SIAMProcedure());
    }

    IEnumerator SIAMProcedure()
    {
        while (trialNum < maxTrialNum && reversalNum < targetReversalNum)
        {
            if (reversalNum >= 1) quota = secondQuota;
            // Adjust text and parameters
            trialNumText.text = "Trial# " + trialNum;
            playSoundText.text = "Playing sound";
            answered = false;
            signal.volume = volume;
            // Play noise and signal
            noise.time = Random.Range(0, noiseLength - noisePlayLength);
            if (Random.Range(0f,1f) < quota || trialNum == 1) signalExist = true;
            else signalExist = false;        
            // For debug use
            Debug.Log("Noise Playback Time:" + noise.time);
            noise.Play();
            if (signalExist) StartCoroutine(PlaySignal());
            yield return new WaitForSeconds(noisePlayLength);
            noise.Stop();
            // Adjust interface after sound ends
            yesBtn.interactable = true;
            noBtn.interactable = true;
            // Wait for user response
            yield return new WaitUntil(() => answered);            
            // Log data
            dataLogger.GetComponent<DataLoggerScript>()
                .LogVolume(LinearToDecibel(signal.volume));
            dataLogger.GetComponent<DataLoggerScript>().LogTrialNumber(trialNum);
            dataLogger.GetComponent<DataLoggerScript>().LogResponse(response);
            // Update the volume for next trial based on current response
            UpdateVolumeAndReversal();
            // Increment trial number
            trialNum ++;
        }
        TerminateProcedure();
    }

    IEnumerator PlaySignal()
    {
        signalWaitTime = Random.Range(0, noisePlayLength - signal.clip.length);
        yield return new WaitForSeconds(signalWaitTime);
        signal.Play();
    }

    public void RegisterResponse(bool isSignal)
    {
        if (isSignal && signalExist) response = 1;// Hit            
        else if (!isSignal && signalExist) response = 2;// Miss            
        else if (isSignal && !signalExist) response = 3;// False alarm           
        else if (!isSignal && !signalExist) response = 4;// Correct rejection                 
        // Disable buttons after response
        yesBtn.interactable = false;
        noBtn.interactable = false;
        answered = true;
    }

    void UpdateVolumeAndReversal()
    {
        if (reversalNum >= discardReversalNum){
            savedTrialNum ++;
            totalVol += volume;
        }
        bool currTrend = true; // Indicate the increase or decrease of this trial
        float dB = LinearToDecibel(volume);
        if (response == 1) { // Hit will decrease the volume
            dB -= 1 * stepSize;            
            currTrend = false;
        } else if (response == 2) { // Miss
            dB += targetPerformance / (1f - targetPerformance) * stepSize;
        } else if (response == 3) { // False alarm
            dB += 1f/(1f-targetPerformance) * stepSize;
        } else if (response == 4) { // Correct rejection
            // Correct rejection does not have effect on reversals
            currTrend = volIncrease;
        }            
        if (currTrend != volIncrease){
            volIncrease = currTrend;
            reversalNum ++;
            dataLogger.GetComponent<DataLoggerScript>().LogReversal(reversalNum);
        }
        volume = DecibelToLinear(dB);
    }
    void TerminateProcedure(){
        float avgVol = totalVol / savedTrialNum;
        // Disable yes/no buttons and back button
        yesBtn.interactable = false;
        noBtn.interactable = false;
        backBtn.interactable = false;
        // Logging and interface update
        playSoundText.text = "";
        dataLogger.GetComponent<DataLoggerScript>().LogFinishedProcedure(LinearToDecibel(avgVol));
        trialNumText.text = "SIAM Procedure Finished.";
        finishBtn.interactable = true;
        PlayerPrefs.SetFloat("SignalVolume", avgVol);
        PlayerPrefs.SetInt("SIAMDone", 1);
    }
    public void BackToMainMenu()
    {
        dataLogger.GetComponent<DataLoggerScript>().LogAbortedProcedure();
        SceneManager.LoadScene("MainMenu");
    }
    public void FinishButton(){
        SceneManager.LoadScene("MainMenu");
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
}

