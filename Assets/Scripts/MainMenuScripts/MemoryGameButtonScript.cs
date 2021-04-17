using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameButtonScript : MonoBehaviour
{
    public Button thisBtn;
    void Start(){ // Un-comment this code to enable the limit on the experiment
        // if (PlayerPrefs.GetInt("SIAMDone", 0) == 0){
        //     thisBtn.interactable = false;
        // } else {
        //     thisBtn.interactable = true;
        // }
    }
    public void StartMemoryGame(){
        SceneManager.LoadScene("Experiment");
    }
}
