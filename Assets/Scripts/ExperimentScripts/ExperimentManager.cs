using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public int maxTrialNum;
    public float signalRatio, responseWindow, trialInterval, noisePlayLength;
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
    private bool signalExist = false;

    void Start(){
        noiseLength = noise.clip.length;
        startBtn.interactable = true;
        cardCover.SetActive(true);
        volume = PlayerPrefs.GetFloat("SignalVolume");
        signal.volume = volume;
        signalBtn.interactable = false;
        // For debug use
        Debug.Log(volume);
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
            trialNumText.text = "Trial #: " + currTrialNum;
            signalBtn.interactable = true;
            // Determine and play signal
            float willPlay = Random.Range(0f,1f);
            if (willPlay < signalRatio) {
                signalExist = true;
                StartCoroutine(PlaySignal());
            } else {
                signalExist = false;
            }
            // Play the noise
            noise.time = Random.Range(0, noiseLength - noisePlayLength);
            noise.Play();            
            yield return new WaitForSeconds(noisePlayLength); 
            noise.Stop();
            // Signal button cannot be pressed during the pause
            signalBtn.interactable = false;
            yield return new WaitForSeconds(trialInterval);   
        }
    }

    IEnumerator PlaySignal(){
        float waitTime = Random.Range(0f, noisePlayLength);
        yield return new WaitForSeconds(waitTime);
        signal.Play();
    }

    public void SignalResponse(){
        Debug.Log("response");
    }
}
