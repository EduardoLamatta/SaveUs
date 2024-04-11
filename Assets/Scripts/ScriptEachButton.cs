using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEachButton : ButtonsAnswer
{
    public static int numCorrecctAnswers;
    public static int numIncorrecctAnswers;
    public static bool questionAnswered;
    public void CorrectAnswer()
    {
        if (gameObject.tag == "CorrectAnswer")
        {
            numCorrecctAnswers++;
        }
        Debug.Log("respuestas incorrectas: " + numIncorrecctAnswers);
        Debug.Log("respuestas correctas: " + numCorrecctAnswers);
        questionAnswered = true;
    }
    public void IncorrectAnswer()
    {
        if (gameObject.tag == "InorrectAnswer")
        {
            numIncorrecctAnswers++;
        }
        Debug.Log("respuestas correctas: " + numCorrecctAnswers);
        Debug.Log("respuestas incorrectas: " + numIncorrecctAnswers);
        questionAnswered = true;
    }
    public static void NullAnswer()
    {
        if (!questionAnswered)
        {
            numIncorrecctAnswers++;
            questionAnswered = true;
            Debug.Log("respuestas incorrectas: " + numIncorrecctAnswers);
            Debug.Log("respuestas correctas: " + numCorrecctAnswers);
        }
    }

}
