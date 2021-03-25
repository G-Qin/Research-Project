using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySdBtnScript : MonoBehaviour
{
    public GameObject manager;
    public Button thisButton;
    public AudioSource signal, noise;
    public AudioClip TEN;
    public float probability, noiseLength, noisePlayLength;
    float waitTime = 0f;
    bool signalExist;
    void Start()
    {
        noiseLength = TEN.length;
    }

    public void PlaySoundAndDisplayText(){
        // Play button should be unclickable during the play
        thisButton.interactable = false;
        // Update volume from last trial, default by 1f
        signal.volume = manager.GetComponent<SIAMManager>().volume;       
        
        // For debug use
        Debug.Log("Volume: " + signal.volume);               
        // Decide whether to play signal in this trial
        float willPlay = Random.Range(0f,1f);
        // Play noise        
        // Yes/No button will show up after the noise
        StartCoroutine(PlayNoiseAndShowButtons());
        if (willPlay < probability) {
            signalExist = true;
            StartCoroutine(WaitAndPlaySignal());
        } else {
            signalExist = false;
        }
        manager.GetComponent<SIAMManager>().PlaySound(signalExist);
    }

    IEnumerator WaitAndPlaySignal(){
        // Randomly generate a wait time before the signal is played
        waitTime = Random.Range(0f, noisePlayLength);
        // For debug use
        Debug.Log("WT:" + waitTime);
        yield return new WaitForSeconds(waitTime);
        signal.Play();
    }

    IEnumerator PlayNoiseAndShowButtons(){
        noise.time = Random.Range(0, noiseLength - noisePlayLength);        
        // For debug use
        Debug.Log("Noise Playback Time:" + noise.time);
        
        noise.Play();
        yield return new WaitForSeconds(noisePlayLength);
        // Reveal the yes/no buttons and update playSoundText
        noise.Stop();
        manager.GetComponent<SIAMManager>().RevealYesNoButtons();                
    }

    public void StopPlaying(){
        noise.mute = true;
        signal.mute = true;
    }
}
