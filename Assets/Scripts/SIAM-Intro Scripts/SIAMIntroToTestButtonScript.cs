using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SIAMIntroToTestButtonScript : MonoBehaviour
{
    GameObject thisButton;

    void Start(){
        // This button will be revealed after the signal test
        thisButton = GameObject.Find("ContinueButton");
        thisButton.SetActive(false);
    }
    public void IntroToTest(){
        //Go to SIAM testing
        SceneManager.LoadScene("SIAM-Testing");
    }

    public void AwakeOnClick(){
        //Reveal the button after signal test
        thisButton.SetActive(true);
    }
}
