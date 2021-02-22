using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialTextScript : MonoBehaviour
{
    public int trialNum, maxTrialNum;
    public Text thisText;
    public GameObject FinishButton, playButton, manager;
    
    void Start()
    {
        trialNum = 1;
    }

    // Update is called once per frame
    void Update(){
        // if (trialNum <= maxTrialNum){
        //     thisText.text = "Trial #" + trialNum.ToString();
        // } else { // Update the trial text after the procedure is complete
        //     thisText.text = "SIAM Procedure Finished.";
        //     playButton.GetComponent<Button>().interactable = false;
        // }        
    }
}
