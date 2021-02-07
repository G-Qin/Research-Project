﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text scoreText;

    private bool init = false;
    private int score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (!init){
            InitializeCards();
        }

        if (Input.GetMouseButtonUp(0)){
            CheckCards();
        }
    }

    void InitializeCards(){
        bool[] cardFaceUsed = new bool[cardFaces.Length];
        int cardFaceIndex, nextCardIndex;
        for (int i = 0; i < cards.Length; i++){
            if (!cards[i].GetComponent<CardScript>().Initialized){ // The card has not been initialized
                do{
                    cardFaceIndex = Random.Range(0, cardFaces.Length);
                } while (cardFaceUsed[cardFaceIndex]); // If the card face has been used, go on to find another one
                cards[i].GetComponent<CardScript>().CardValue = cardFaceIndex;
                cards[i].GetComponent<CardScript>().Initialized = true;

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

        init = true;
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
        CardScript.canBeFlipped = true;
        int x = 0;

        if (cards[c[0]].GetComponent<CardScript>().CardValue == cards[c[1]].GetComponent<CardScript>().CardValue){
            x = 1;
            score ++;
            scoreText.text = "Number of Matches" + score;
            if (score == 0){
                // Reset the game
            }

            for (int i = 0; i < c.Count; i++){
                cards[c[i]].GetComponent<CardScript>().State = x;
                cards[c[i]].GetComponent<CardScript>().FalseCheck();
            }
        }
    }

    public Sprite GetCardBack(){
        return cardBack;
    }

    public Sprite GetCardFace(int i){
        return cardFaces[i];
    }
}
