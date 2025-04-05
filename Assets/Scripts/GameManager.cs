using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;

    void Awake()
    {
        quiz = FindFirstObjectByType<Quiz>(); 
        endScreen = FindFirstObjectByType<EndScreen>(); 
    }
    void Start()
    {
 
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false); // Deactivate the EndScreen script
    }

    void Update()
    {
        if (quiz.isComplete){
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }
    public void ReloadGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
