using UnityEngine;
[CreateAssetMenu(menuName = "Quiz Question", fileName = "A New Question")] // Create a new ScriptableObject class for quiz questions)]

public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)] 
    [SerializeField] string question = "Enter new question text here"; // The question text
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex; // The index of the correct answer

    public string GetQuestion(){
        return question; // Returns the question text
    }

    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex; // Returns the index of the correct answer
    }
    public string GetAnswer(int index){
        return answers[index];
    }


}
