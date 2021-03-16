using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class ExperimentManager : MonoBehaviour
{
    public int maxTrialNum;
    public float signalRatio, trialInterval, noisePlayLength;
    public float feedbackTime, responseWindow;
    // Above are config variables
    public Button startBtn, signalBtn;
    public GameObject cardCover;
    public AudioSource noise, signal;
    public Image feedbackSquare;
    public Text trialNumText;
    [SerializeField]
    private int currTrialNum;
    [SerializeField]
    private float volume, noiseLength;
    private bool signalExist = false, answered = false;
    private float windowStartTime, windowEndTime, signalWaitTime;
    private Stopwatch stopwatch;

    void Start(){
        noiseLength = noise.clip.length;
        startBtn.interactable = true;
        cardCover.SetActive(true);
        volume = PlayerPrefs.GetFloat("SignalVolume");
        signal.volume = volume;
        signalBtn.interactable = false;
        // For debug use
        UnityEngine.Debug.Log(volume);
    } 

    public void StartExperiment(){
        signalBtn.interactable = true;
        startBtn.interactable = false;
        cardCover.SetActive(false);
        StartCoroutine(Experiment());
    }

    IEnumerator Experiment()
    {        
        for (currTrialNum = 1; currTrialNum <= maxTrialNum; currTrialNum++){
            answered = false;
            trialNumText.text = "Trial #: " + currTrialNum;
            signalBtn.interactable = true;
            // Determine and play signal
            float willPlay = Random.Range(0f,1f);
            if (willPlay < signalRatio) {
                signalExist = true;
                signalWaitTime = Random.Range(0f, noisePlayLength);
                UnityEngine.Debug.Log(signalWaitTime);
                // Compute the response window in miliseconds
                windowStartTime = signalWaitTime * 1000f;
                windowEndTime = windowStartTime + responseWindow * 1000f;
                // Reset the stopwatch and start timing
                stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                StartCoroutine(PlaySignal());
            } else {
                signalExist = false;
            }
            // Play the noise
            noise.time = Random.Range(0, noiseLength - noisePlayLength);
            noise.Play();            
            yield return new WaitForSeconds(noisePlayLength); 
            noise.Stop();
            // Log a miss or correct rejection
            if (!answered && signalExist) UnityEngine.Debug.Log("Miss");
            if (!answered && !signalExist) UnityEngine.Debug.Log("Correct Rejection");
            // Signal button cannot be pressed during the pause
            signalBtn.interactable = false;
            yield return new WaitForSeconds(trialInterval);   
        }
    }

    IEnumerator PlaySignal(){
        yield return new WaitForSeconds(signalWaitTime);
        signal.Play();
    }

    public void SignalResponse(){
        if (!signalExist && !answered) UnityEngine.Debug.Log("False Alarm");
        if (signalExist && !answered){
            stopwatch.Stop();
            float time = (float)stopwatch.ElapsedMilliseconds;
            if (time >= windowStartTime && time <= windowEndTime){
                // Response within the window
                UnityEngine.Debug.Log("Hit");
                StartCoroutine(ChangeFeedbackColor(true));
            } else {
                // Response outside the window
                UnityEngine.Debug.Log("False Alarm");
                StartCoroutine(ChangeFeedbackColor(false));
            }
        }
        answered = true;
    }

    IEnumerator ChangeFeedbackColor(bool isCorrect){
        // Green
        if (isCorrect) feedbackSquare.color = new Color32(0, 255, 0, 255);
        // Red
        else feedbackSquare.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(feedbackTime);
        // Black
        feedbackSquare.color = new Color32(0, 0, 0, 255);
    }
}
