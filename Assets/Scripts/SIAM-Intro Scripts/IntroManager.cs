using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Button continueBtn;
    private bool signalClicked, noiseClicked;
    void Start()
    {
        signalClicked = false;
        noiseClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (signalClicked && noiseClicked) {
            continueBtn.GetComponent<SIAMIntroToTestButtonScript>().AwakeOnClick();
        }
    }

    public void SignalTested(){
        signalClicked = true;
    }

    public void NoiseTested(){
        noiseClicked = true;
    }
}
