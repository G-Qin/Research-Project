﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButtonScript : MonoBehaviour
{
    public GameObject trialText;
    public GameObject thisButton;

    void Start(){
        // No button is hidden at the start
        thisButton.SetActive(false);
    }

    public void UpdateTrialNumber(){
        trialText.
            GetComponent<TrialTextScript>().
                IncrementTrialNumber();
    }

    public void AwakeOnSoundEnd(){
        thisButton.SetActive(true);
    }

    public void HideOnClick(){
        thisButton.SetActive(false);
    }
}
