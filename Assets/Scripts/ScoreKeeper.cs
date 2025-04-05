using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCorrectAnswers(){
        correctAnswers++;
    }
    public int GetCorrectAnswers(){
        return correctAnswers;
    }
    public void setQuestionSeen(){
        questionsSeen++;
    }
    public int GetQuestionsSeen(){
        return questionsSeen;
    }
    public int GetScore(){
        if (questionsSeen == 0){
            return 0;

        }
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
