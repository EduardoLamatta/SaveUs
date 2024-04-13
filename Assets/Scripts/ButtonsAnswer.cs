using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAnswer : ReadExcel
{
    public static int numAnswers = 0;

    public void AddButtonChildren(List<Transform> buttonList, Transform layoutButtons)
    {
        foreach (Transform transform in layoutButtons)
        {
            buttonList.Add(transform);
        }
    }

    public void AddIndexButtons(List<Transform> buttonList, List<int> numButtonList)
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            numButtonList.Add(i);
        }
    }

    public void RandomIndexList(GameObject[] buttonsAnswers, List<int> indexButtonRandom, List<int> numButtonList)
    {
        for (int i = 0; i < buttonsAnswers.Length; i++)
        {
            int numRandom = UnityEngine.Random.Range(0, numButtonList.Count);
            indexButtonRandom.Add(numButtonList[numRandom]);
            numButtonList.RemoveAt(numRandom);
        }
    }

    public void RandomButtons(List<Transform> buttonList, List<int> indexButtonRandom)
    {
        for (int i = 0; i <  buttonList.Count; i++)
        {
            buttonList[i].SetSiblingIndex(indexButtonRandom[i]);
        }
    }

    public void DeactivateButtons(GameObject[] buttonsAnswers, Button[] buttons, Questions questions)
    {
        if (!questions.questionComplete || AnswerCounter.questionAnswered || questions.currentQuestionTime <= 0)
        {
            for (int i = 0; i < buttonsAnswers.Length; i++)
            {
                buttonsAnswers[i].SetActive(false);
            }
        }
    }
  
    public void ActivateButtons(GameObject[] buttonsAnswers, Button[] buttons, Questions dialogue)
    {
        if (numAnswers >= 0 && dialogue.questionComplete && !AnswerCounter.questionAnswered && dialogue.currentQuestionTime > 0)
        {
            for (int i = 0; i < buttonsAnswers.Length; i++)
            {
                buttonsAnswers[i].SetActive(true);
            }
        }

    }
    public void ClearListButtons(List<Transform> buttonList, List<int> indexButtonRandom, List<int> numButtonList)
    {
        numButtonList.Clear();
        indexButtonRandom.Clear();
        buttonList.Clear();
    }

}
