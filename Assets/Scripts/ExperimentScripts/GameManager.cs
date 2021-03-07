using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text scoreText, accuText;
    public GameObject dataLogger;
    private bool init = false;
    private int score = 0, matches = 0, flipNum = 0;
    private float accuracy;

    void Start(){
        dataLogger.GetComponent<GameDataLogger>().NewMemGameLogFile();
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        if (flipNum == 0) accuText.text = "Accuracy: 0.00%";
        else accuText.text = "Accuracy: " + 
            (((float)score/(float)flipNum)*100).ToString("0.00") + "%";

        if (!init) {
            InitializeCards();
        }

        if (Input.GetMouseButtonUp(0)){
            CheckCards();
        }
    }

    void InitializeCards(){
        bool[] cardFaceUsed = new bool[cardFaces.Length];
        int cardFaceIndex;
        int[] tempCards = new int[cards.Length];
        // Randomly select cards and place them uniformly in the deck
        for (int i = 0; i < cards.Length; i++){
            if (i % 2 == 0) {
                do {
                    cardFaceIndex = Random.Range(0, cardFaces.Length);
                } while (cardFaceUsed[cardFaceIndex]);
                tempCards[i] = cardFaceIndex;
                cardFaceUsed[cardFaceIndex] = true;
            } else {
                tempCards[i] = tempCards[i - 1];
            }
        }
        // Shuffle the deck using Fisher-Yates algorithm
        int j, temp;
        for (int i = 0; i < cards.Length; i++){
            j = Random.Range(0, i+1);
            temp = tempCards[i];
            tempCards[i] = tempCards[j];
            tempCards[j] = temp;
        }

        for (int i = 0; i < cards.Length; i++){
            cards[i].GetComponent<CardScript>().CardValue = tempCards[i];
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
        // Find cards that are flipped
        List<int> c = new List<int>();
        for (int i = 0; i < cards.Length; i++){
            if (cards[i].GetComponent<CardScript>().State == 1) c.Add(i);            
        }

        if (c.Count == 2) {
            flipNum ++;
            CardComparison(c);            
        }
    }

    void CardComparison(List<int> c){
        CardScript.canBeFlipped = false;
        int state = 0;
        // If the flipped cards form a pair
        if (cards[c[0]].GetComponent<CardScript>().CardValue == cards[c[1]].GetComponent<CardScript>().CardValue){
            state = 2;
            score ++;
            matches ++;
            dataLogger.GetComponent<GameDataLogger>().LogHit(score, flipNum);                                  
        } else {
            dataLogger.GetComponent<GameDataLogger>().LogMiss(score, flipNum);
        }
        // Update card state
        for (int i = 0; i < c.Count; i++){
            cards[c[i]].GetComponent<CardScript>().State = state;
            cards[c[i]].GetComponent<CardScript>().FalseCheck();
        }

        if (matches == cards.Length / 2){ 
            // Game over, reset the game with a delay
            StartCoroutine(ResetPause());
        }  
    }

    public Sprite GetCardBack(){
        return cardBack;
    }

    public Sprite GetCardFace(int i){
        return cardFaces[i];
    }

    IEnumerator ResetPause(){
        yield return new WaitForSeconds(1);
        ResetCards();
        InitializeCards();
        dataLogger.GetComponent<GameDataLogger>().LogReset();
    }
}
