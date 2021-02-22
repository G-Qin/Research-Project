using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySdBtnScript : MonoBehaviour
{
    public GameObject manager;
    public Button thisButton;
    public AudioSource signal, noise;
    public float probability, noiseLength;
    float waitTime = 0f;
    bool signalExist;
    void Start()
    {

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
        noise.Play();
        // Yes/No button will show up after the noise
        StartCoroutine(RevealYesNoButtons());
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
        waitTime = Random.Range(0f, noiseLength);
        // For debug use
        Debug.Log("WT:" + waitTime);
        yield return new WaitForSeconds(waitTime);
        signal.Play();
    }

    IEnumerator RevealYesNoButtons(){
        yield return new WaitForSeconds(noiseLength);
        // Reveal the yes/no buttons and update playSoundText
        manager.GetComponent<SIAMManager>().RevealYesNoButtons();        
    }

    public void Reactivate(){
        thisButton.interactable = true;
    }
}
