using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialTextScript : MonoBehaviour
{
    public int trialNum, maxTrialNum;
    public Text trialText;
    public GameObject FinishButton, playButton, dataLogger;
    
    void Start()
    {
        trialNum = 1;
    }

    // Update is called once per frame
    void Update(){
        if (trialNum <= maxTrialNum){
            trialText.text = "Trial #" + trialNum.ToString();
        } else { // Update the trial text after the procedure is complete
            trialText.text = "SIAM Procedure Finished.";
            playButton.GetComponent<Button>().interactable = false;
        }
        
    }

    public void IncrementTrialNumber()
    {   
        // Log trial number
        dataLogger.GetComponent<DataLoggerScript>().LogTrialNumber(trialNum);
        // Update trial number
        trialNum ++;
        // Activate trial number when procedure is complete
        if (trialNum > maxTrialNum) {
            FinishButton.GetComponent<FinishBtnScript>().AwakeAtFinish();
        }
    }


}
