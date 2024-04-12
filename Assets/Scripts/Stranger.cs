using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : SceneEffects
{
    [SerializeField] private Vector3 rightPosition;
    [SerializeField] private float velocityMovement;
    [SerializeField] private GameObject startButton, questionSection, answerSection;
    private bool moveStranger = true;
    void Start()
    {

    }
    public void Update()
    {
        if (!moveStranger)
        {
            MoveRight();
        }
        if (transform.position == rightPosition)
        {
            moveStranger = true;
            ActivateQuestionSection();
            ActivateAnswerSection();
        }
    }
    public void MoveRight()
    {
        MovementsInGame(rightPosition, velocityMovement);
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
