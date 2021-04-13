using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataCleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResetAndProceed());
    }

    IEnumerator ResetAndProceed(){
        PlayerPrefs.DeleteAll();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
