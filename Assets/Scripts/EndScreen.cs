using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        

    }
    public void ShowFinalScore(){
        finalScoreText.text = "Congrats! You scored " + scoreKeeper.GetScore() + "%!";
        
    }
}
