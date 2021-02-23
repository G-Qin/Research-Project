using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public static bool canBeFlipped = true;
    [SerializeField]
    private int state;
    [SerializeField]
    private int cardValue;
    [SerializeField]
    private Sprite cardBack;
    private Sprite cardFace;
    private GameObject manager;

    public void Start(){
        state = 1;
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void SetupGraphics(){
        cardBack = manager.GetComponent<GameManager>().GetCardBack();
        cardFace = manager.GetComponent<GameManager>().GetCardFace(cardValue);
        state = 0;
    }

    public void FlipCard(){
        // Cards can only be flipped by user from back to face
        // Flipping from face to back will be done by program
        if (state == 0){
            state = 1;            
        } 

        if (state == 1 && canBeFlipped){
            GetComponent<Image>().sprite = cardFace;            
        } else if (state == 1 && !canBeFlipped){
            state = 0;
        }
    }

    public void ResetCard(){
        state = 0;
        GetComponent<Image>().sprite = cardBack;
    }

    public int CardValue{
        get {return cardValue;}
        set {cardValue = value;}
    }

    public int State {
        get {return state;}
        set {state = value;}
    }

    public void FalseCheck(){
        StartCoroutine(Pause());
    }

    IEnumerator Pause(){
        yield return new WaitForSeconds(1);
        if (state == 0){
            GetComponent<Image>().sprite = cardBack;
        } else if (state == 1){
            GetComponent<Image>().sprite = cardFace;            
        }
        canBeFlipped = true;
    }
}
