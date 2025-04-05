using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeLimit = 16f; // Time limit in seconds
    [SerializeField] float timeToReviewAnswer = 5f; // Time showing the correct answer
    public float fillFraction;
    public bool loadNextQuestion = true;
    public bool isAnsweringQuestion = false; // Flag to check if the player is answering a question
    float timerValue;
    void Update()
    {
        UpdateTimer(); // Call the UpdateTimer method every frame
    }
    public void CancelTimer(){
        timerValue = 0f;
    }
    void UpdateTimer(){
        timerValue -= Time.deltaTime; // Decrease the timer value by the time passed since the last frame
        if (isAnsweringQuestion)
        {
            if (timerValue > 0f){
                fillFraction = timerValue / timeLimit; 
            }
            else {
                isAnsweringQuestion = false;
                timerValue = timeToReviewAnswer; // Reset the timer value to the review time
            }
 
        }
        else {
            if (timerValue > 0f)
            {
                fillFraction = timerValue / timeToReviewAnswer;
            }
            else {

                loadNextQuestion = true; // Load the next question when the timer runs out
                isAnsweringQuestion = true;
                timerValue = timeLimit;
            }
        }
     
    }
}
