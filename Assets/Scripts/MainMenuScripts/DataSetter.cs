using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SignalVolume")){
            PlayerPrefs.SetFloat("SignalVolume", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
