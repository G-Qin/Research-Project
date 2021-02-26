using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundTxtScript : MonoBehaviour
{
    GameObject thisText;
    void Start()
    {
        // Disable the text at the beginning
        thisText = GameObject.Find("PlaySoundText");
        thisText.SetActive(false);
        thisText.GetComponent<Text>().text = "Playing Sound...";
    }

    public void AwakeOnPlaySound(){
        thisText.SetActive(true);
    }

    public void SoundFinishText(){
        thisText.GetComponent<Text>().text = "Sound ended.";
    }

    public void PlaySoundText(){
        thisText.GetComponent<Text>().text = "Playing Sound...";
    }
}
