using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCounter : ButtonsAnswer
{
    public static float numCorrecctAnswers;
    public static float numIncorrectAnswers;
    public static bool questionAnswered;
    public void CorrectAnswer()
    {
        if (gameObject.tag == "CorrectAnswer")
        {
            numCorrecctAnswers++;
        }
        questionAnswered = true;
    }
    public void IncorrectAnswer()
    {
        if (gameObject.tag == "InorrectAnswer")
        {
            numIncorrectAnswers++;
        }
        questionAnswered = true;
    }
    public static void NullAnswer()
    {
        if (!questionAnswered)
        {
            numIncorrectAnswers++;
            questionAnswered = true;
        }
    }


}
