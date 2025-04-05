using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questionSOList; // list of questions

    QuestionSO currentQuestionSO;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = false; // Flag to check if the player has answered early
    int correctAnswerIndex;
    bool didntAnswerInTime = false;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage; // Reference to the Timer script
    Timer timer;

    [Header("Score")]
    [SerializeField] ScoreKeeper scoreKeeper; // Reference to the ScoreKeeper script
    [SerializeField] TextMeshProUGUI scoreText; // reference to the UI text for score

    [Header("Slider")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    void Awake()
    {
        timer = FindFirstObjectByType<Timer>(); 
    }
    void Start()
    {
        
        progressBar.maxValue = questionSOList.Count; // Set the maximum value of the slider to the number of questions
        progressBar.value = 0;

    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;  
        if (timer.loadNextQuestion){
            if (progressBar.value == progressBar.maxValue){
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            timer.loadNextQuestion = false;
            GetNextQuestion();
        } 
        else if (!timer.isAnsweringQuestion && !hasAnsweredEarly)
        {   
            if (!didntAnswerInTime){ // This bool is set to true when the function is called so it won't keep doing the function
            DidntAnswerInTime(); // Display message if the player didn't answer in time
            }
        }
    }

    void GetNextQuestion()
    {
        

        if (questionSOList.Count > 0){
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            didntAnswerInTime = false;
        }
       
    }
    void GetRandomQuestion(){
        int index = Random.Range(0, questionSOList.Count); // Get a random index from the list of questions 
        currentQuestionSO = questionSOList[index]; // Get the question at the random index
        if (questionSOList.Contains(currentQuestionSO)){
            questionSOList.RemoveAt(index); // Remove the question from the list to avoid repetition
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestionSO.GetQuestion(); // Set the question text in the UI
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionSO.GetAnswer(i); // Set the text of each answer button
        }

    }

    void SetButtonState(bool state)
    {
        for(int i=0; i<answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;

        }
    }

    void SetDefaultButtonSprites(){
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite; // Set the default sprite for each button
            // This will reset the button sprite to the default state before displaying the new question    
        }
    }

    public void OnAnswerButtonClick(int index){
        hasAnsweredEarly = true; // Set the flag to true when an answer is clicked
        DisplayAnswer(index); // Answer is shown when button is clicked
        SetButtonState(false); // Disable all buttons after an answer is selected
        timer.CancelTimer(); // Stop the timer when an answer is selected
        scoreKeeper.setQuestionSeen(); // Increment the number of questions seen
        scoreText.text = "Score: " + scoreKeeper.GetScore() + "%"; // Update the score text in the UI

    }
    void DisplayAnswer(int index){
        progressBar.value++;
        Image buttonImage;
        Image correctButtonImageWhenYouGetItWrong;
        if (index == currentQuestionSO.GetCorrectAnswerIndex()){
            scoreKeeper.SetCorrectAnswers(); // Increment the score if the answer is correct
            questionText.text = "Correct!"; // Display message
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite; // Change the button sprite to indicate correct answer
        }
        else {
            correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex(); // Get the correct answer index
            string correctAnswer = currentQuestionSO.GetAnswer(correctAnswerIndex);
            questionText.text = "The correct answer is: \n" + correctAnswer; 
            // Display the correct answer
            correctButtonImageWhenYouGetItWrong = answerButtons[correctAnswerIndex].GetComponent<Image>();
            correctButtonImageWhenYouGetItWrong.sprite = correctAnswerSprite; // Change the button sprite to indicate correct answer
            // Change the button sprite to indicate correct answer
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = wrongAnswerSprite; // Change the button sprite to indicate your wrong answer
        }
    }
    void DidntAnswerInTime()
    {   
        didntAnswerInTime = true;
        if (didntAnswerInTime){
        scoreKeeper.setQuestionSeen(); // Increment the number of questions seen
        scoreText.text = "Score: " + scoreKeeper.GetScore() + "%"; // Update the score text in the UI    
        
        progressBar.value++; // update progress value
        SetButtonState(false); // Disable all buttons when time runs out
        Image buttonImage;
        int correctAnswerIndex = currentQuestionSO.GetCorrectAnswerIndex();
        string correctAnswer = currentQuestionSO.GetAnswer(correctAnswerIndex);
        questionText.text = "The correct answer is: \n" + correctAnswer; // Display the correct answer
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite; // Change the button sprite to indiicate correct answer 
        }    
    }
    
}
