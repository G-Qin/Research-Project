using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIAMText : MonoBehaviour
{
    public GameObject thisText;
    public void Deactivate(){
        thisText.SetActive(false);
    }
}
