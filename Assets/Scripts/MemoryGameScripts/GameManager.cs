using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text scoreText;

    private bool init = false;
    private int score = 0, matches = 0;

    void Start()
    {     
        
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        if (!init) {
            InitializeCards();
        }

        if (Input.GetMouseButtonUp(0)){
            CheckCards();
        }
    }

    void InitializeCards(){
        bool[] cardFaceUsed = new bool[cardFaces.Length];
        int cardFaceIndex, nextCardIndex;

        for (int i = 0; i < cards.Length; i++) {
            cards[i].GetComponent<CardScript>().Initialized = false;
        }
            
        for (int i = 0; i < cards.Length; i++){
            cards[i].GetComponent<CardScript>().Start();
            if (!cards[i].GetComponent<CardScript>().Initialized){ // The card has not been initialized
                do{
                    cardFaceIndex = Random.Range(0, cardFaces.Length);
                } while (cardFaceUsed[cardFaceIndex]); // If the card face has been used, go on to find another one
                cards[i].GetComponent<CardScript>().CardValue = cardFaceIndex;
                cards[i].GetComponent<CardScript>().Initialized = true;
                cardFaceUsed[cardFaceIndex] = true; // Mark this card face as used

                do{ // Find another card to form a pair
                    nextCardIndex = Random.Range(0, cards.Length);
                } while (cards[nextCardIndex].GetComponent<CardScript>().Initialized);
                cards[nextCardIndex].GetComponent<CardScript>().CardValue = cardFaceIndex;
                cards[nextCardIndex].GetComponent<CardScript>().Initialized = true;                
            }                
        }

        foreach(GameObject card in cards){
            card.GetComponent<CardScript>().SetupGraphics();
        }
        matches = 0;
        init = true;
    }

    void ResetCards(){
        for (int i = 0; i < cards.Length; i++){
            cards[i].GetComponent<CardScript>().ResetCard();
        }
    }

    void CheckCards(){
        List<int> c = new List<int>();
        for (int i = 0; i < cards.Length; i++){
            if (cards[i].GetComponent<CardScript>().State == 1){
                c.Add(i);
            }
        }

        if (c.Count == 2) CardComparison(c);
    }

    void CardComparison(List<int> c){
        CardScript.canBeFlipped = false;
        int x = 0;

        if (cards[c[0]].GetComponent<CardScript>().CardValue == cards[c[1]].GetComponent<CardScript>().CardValue){
            x = 2;
            score ++;
            matches ++;
                                  
        }

        for (int i = 0; i < c.Count; i++){
            cards[c[i]].GetComponent<CardScript>().State = x;
            cards[c[i]].GetComponent<CardScript>().FalseCheck();
        }

        if (matches == cards.Length / 2){ 
            // Game over, reset the game
            ResetCards();
            InitializeCards();
        }  
    }

    public Sprite GetCardBack(){
        return cardBack;
    }

    public Sprite GetCardFace(int i){
        return cardFaces[i];
    }
}
