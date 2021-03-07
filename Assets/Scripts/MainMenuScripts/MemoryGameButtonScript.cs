using UnityEngine.SceneManagement;
using UnityEngine;

public class MemoryGameButtonScript : MonoBehaviour
{
    public void StartMemoryGame(){
        SceneManager.LoadScene("Experiment");
    }
}
