using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoiseBtn : MonoBehaviour
{
    public AudioSource noise;

    // Update is called once per frame
    public void PlayNoiseForSeconds()
    {
        StartCoroutine(PlayForSeconds());

    }

    IEnumerator PlayForSeconds(){
        noise.Play();
        yield return new WaitForSeconds(3);
        noise.Stop();
    }
}
