using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public static bool canBeFlipped = false;
    [SerializeField]
    private int state;
    [SerializeField]
    private int cardValue;
    [SerializeField]
    private bool initialized = false;

    private Sprite cardBack;
    private Sprite cardFace;
    private GameObject manager;

    void Start(){
        state = 1;
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void SetupGraphics(){
        cardBack = manager.GetComponent<GameManager>().GetCardBack();
        cardFace = manager.GetComponent<GameManager>().GetCardFace(cardValue);
        FlipCard();
    }

    public void FlipCard(){
        if (state == 0){
            state = 1;
        } else if (state == 1){
            state = 0;
        }

        if (state == 0 && !canBeFlipped){
            GetComponent<Image>().sprite = cardBack;
        } else if (state == 1 && !canBeFlipped){
            GetComponent<Image>().sprite = cardFace;
        }
    }

    public int CardValue{
        get {return cardValue;}
        set {cardValue = value;}
    }

    public int State {
        get {return state;}
        set {state = value;}
    }

    public bool Initialized{
        get {return initialized;}
        set {initialized = value;}
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
            canBeFlipped = false;
        }
    }
}
