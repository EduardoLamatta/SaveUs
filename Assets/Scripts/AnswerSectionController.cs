using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSectionController : SceneEffects
{
    [SerializeField] private GameObject answerSection;
    [SerializeField] private Transform finalExitPosition, finalInsidePosition;
    [SerializeField] private Questions questions;
    [SerializeField] private ReadExcel readExcel;
    [SerializeField] private float movementSpeed;
    private bool sectionAnswerInGame;


    private void Update()
    {
        MovementInsideScene();
        MovementExitScene();
        if (ButtonsAnswer.numAnswers == 16 || questions.currentQuestionTime <= 0)
        {
            sectionAnswerInGame = true;
        }
    }

    private void MovementInsideScene()
    {
        if (!sectionAnswerInGame)
        {
            MovementsInGame(finalInsidePosition.position, movementSpeed);
        }
    }
    private void MovementExitScene()
    {
        if (sectionAnswerInGame)
        {
            MovementsInGame(finalExitPosition.position, movementSpeed);
        }

    }


}
