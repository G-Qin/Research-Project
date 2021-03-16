using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeardSignalWithSpacebar : MonoBehaviour
{
    public Button thisButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && thisButton.interactable){
            thisButton.onClick.Invoke();
        }
    }
}
