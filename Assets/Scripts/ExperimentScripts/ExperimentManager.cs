using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public int maxTrialNum;
    public float signalRatio;
    // Above are config variables
    public Button startBtn;
    public GameObject cardCover;
    public AudioSource noise, signal;
    [SerializeField]
    private int currTrialNum;
    [SerializeField]
    private float volume;

    void Start(){
        startBtn.interactable = true;
        cardCover.SetActive(true);
        volume = PlayerPrefs.GetFloat("SignalVolume");
        // For debug use
        Debug.Log(volume);
    } 

    public void StartExperiment(){
        startBtn.interactable = false;
        cardCover.SetActive(false);
    }

    void Update()
    {
        
    }
}
