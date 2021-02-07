using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySdBtnScript : MonoBehaviour
{
    public GameObject playSoundText, yesButton, noButton, dataLogger;
    public Button thisButton;
    public AudioClip whiteNoise;
    public AudioSource source;
    public float probability, targetPerformance;
    public float noiseLength;
    float waitTime = 0f, volume = 1f;
    bool signalExist;
    void Start()
    {
        dataLogger.GetComponent<DataLoggerScript>().NewSIAMDataFile();
    }

    public void PlaySoundAndDisplayText(){
        // Play button should be unclickable during the play
        thisButton.interactable = false;
        // Update volume from last trial, default by 1f
        source.volume = volume;
        // For debug use
        Debug.Log(volume);
        dataLogger.GetComponent<DataLoggerScript>().LogVolume(source.volume);
        // Display text
        playSoundText.GetComponent<PlaySoundTxtScript>().
            AwakeOnPlaySound();
        // Decide whether to play signal in this trial
        float willPlay = Random.Range(0f,1f);
        // Play noise
        source.PlayOneShot(whiteNoise);
        // Yes/No button will show up after the noise
        StartCoroutine(RevealYesNoButtons());
        if (willPlay < probability) {
            signalExist = true;
            StartCoroutine(WaitAndPlaySignal());
        } else {
            signalExist = false;
        }
        
    }

    IEnumerator WaitAndPlaySignal(){
        // Randomly generate a wait time before the signal is played
        waitTime = Random.Range(0f, noiseLength);
        Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);
        source.Play();
    }

    IEnumerator RevealYesNoButtons(){
        yield return new WaitForSeconds(noiseLength);
        // Reveal the yes/no buttons and update playSoundText
        yesButton.GetComponent<YesButtonScript>().AwakeOnSoundEnd();
        noButton.GetComponent<NoButtonScript>().AwakeOnSoundEnd();
        playSoundText.GetComponent<PlaySoundTxtScript>().SoundFinishText();
    }

    // Adjust the volume for next trial based on SIAM payoff matrix
    public void UpdateVolume(bool response){
        float dB = LinearToDecibel(volume);
        // Hit
        if (response && signalExist){
            dB -= 1;
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Hit");
        } else if (!response && signalExist){ // Miss
            dB += targetPerformance / (1f - targetPerformance);
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Miss");
        } else if (response && !signalExist){ // False alarm
            dB += 1f/(1f-targetPerformance);
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Flase alarm");
        } else { // Correct rejection
            dataLogger.GetComponent<DataLoggerScript>().LogResponse("Correct rejection");
        }
        volume = DecibelToLinear(dB);
    }

    private float LinearToDecibel(float linear)
    {   // Convert linear volume to decibels
        float dB;
         
        if (linear != 0) dB = 20.0f * Mathf.Log10(linear);
        else dB = -144.0f; 

        return dB;
    }

    private float DecibelToLinear(float dB)
    {   // Convert decibels to linear volume
        float linear = Mathf.Pow(10.0f, dB/20.0f);
        if (linear > 1) linear = 1;
        return linear;
    }

    public void Reactivate(){
        thisButton.interactable = true;
    }
}
