using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCounter : ButtonsAnswer
{
    public static float numCorrecctAnswers;
    public static float numIncorrectAnswers;
    public static bool questionAnswered;
    public static float totalAnswer;
    [SerializeField] private AudioManager audioManager;
    public static bool nullAnswer;

    private void Update()
    {
        totalAnswer = numCorrecctAnswers + numIncorrectAnswers;
    }
    public void CorrectAnswer()
    {
        if (!questionAnswered)
        {
            audioManager.PlayAudioClipButton();
            if (gameObject.tag == "CorrectAnswer")
            {
                numCorrecctAnswers++;
                nullAnswer = false;
            }
            questionAnswered = true;

        }
    }
    public void IncorrectAnswer()
    {
        if (!questionAnswered)
        {
            audioManager.PlayAudioClipButton();
            if (gameObject.tag == "IncorrectAnswer")
            {
                numIncorrectAnswers++;
                nullAnswer = false;
            }
            questionAnswered = true;

        }

    }
    public static void NullAnswer()
    {
        if (!questionAnswered)
        {
            numIncorrectAnswers++;
            questionAnswered = true;
            nullAnswer = true;
            Debug.Log("null answer");
        }
    }


}
