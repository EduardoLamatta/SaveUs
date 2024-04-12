using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResults : SceneEffects
{
    [SerializeField] private ReadExcel readExcel;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private Questions questions;
    private void Start()
    {
        victoryScreen.SetActive(false);
        looseScreen.SetActive(false);
    }
    private void Update()
    {
        if (AnswerCounter.numCorrecctAnswers == 15)
        {
            victoryScreen.SetActive(true);
        }
        else if (questions.currentQuestionTime <= 0 && questions.inGame)
        {
            looseScreen.SetActive(true);
        }
    }

}
