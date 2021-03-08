using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public int maxTrialNum;
    public float signalRatio;
    public Button startBtn;
    public GameObject cardCover;
    [SerializeField]
    private int currTrialNum;

    void Start(){
        startBtn.interactable = true;
        cardCover.SetActive(true);
    } 

    public void StartExperiment(){
        startBtn.interactable = false;
        cardCover.SetActive(false);
    }

    void Update()
    {
        
    }
}
