using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : SceneEffects
{
    [SerializeField] private float velocityMovement;
    [SerializeField] private GameObject startButton, questionSection, answerSection;
    private bool moveStranger = true; 
    [SerializeField] private Transform finalPosition;


    public void Update()
    {
        if (!moveStranger)
        {
            MoveRight();
        }
        if (transform.position == finalPosition.position)
        {
            moveStranger = true;
            ActivateQuestionSection();
            ActivateAnswerSection();
        }
        
    }
    public void MoveRight()
    {
        MovementsInGame(finalPosition.position, velocityMovement);
    }
    public void ButtonStart()
    {
        moveStranger = false;
        startButton.SetActive(false);
    }
    public void ActivateQuestionSection()
    {
        questionSection.SetActive(true);
    }
    public void ActivateAnswerSection()
    {
        answerSection.SetActive(true);
    }
}
