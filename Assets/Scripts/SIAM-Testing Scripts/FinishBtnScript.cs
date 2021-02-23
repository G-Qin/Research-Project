using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBtnScript : MonoBehaviour
{
    public GameObject thisButton;

    // Start is called before the first frame update
    void Start()
    {
        thisButton.SetActive(false);
    }

    public void AwakeAtFinish()
    {
        thisButton.SetActive(true);
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
