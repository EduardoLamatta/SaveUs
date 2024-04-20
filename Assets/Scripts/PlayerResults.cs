using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResults : SceneEffects
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private Questions questions;
    [SerializeField] private Transform positionSoon, positionWife, positionFather;
    [SerializeField] private GameObject soon, wife, father;
    private void Start()
    {
        victoryScreen.SetActive(false);
        looseScreen.SetActive(false);
    }
    private void Update()
    {
        if (AnswerCounter.numCorrecctAnswers == 9)
        {
            victoryScreen.SetActive(true);
            PositionsPeopleToFinal();
        }
        else if (questions.currentQuestionTime <= 0 && questions.inGame)
        {
            looseScreen.SetActive(true);
            PositionsPeopleToFinal();
        }
    }
    private void PositionsPeopleToFinal()
    {
        soon.transform.position = positionSoon.position;
        wife.transform.position = positionWife.position;
        father.transform.position = positionFather.position;
    }

}
