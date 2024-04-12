using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : SceneEffects
{
    [SerializeField] private ReadExcel readExcel;
    [SerializeField] private Transform questionSection, finalPositionQuestion;
    [SerializeField] private Transform answerSection, finalPositionAnswer;
    [SerializeField] private float speedQuestionSection;
    void Update()
    {
        if (AnswerCounter.numCorrecctAnswers == readExcel.answer_a.Length)
        {
            Movements(questionSection.position, finalPositionQuestion.position, speedQuestionSection);
            Movements(answerSection.position, finalPositionAnswer.position, speedQuestionSection);
        }
        else if (AnswerCounter.numIncorrectAnswers > readExcel.answer_a.Length / 3)
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Movements(questionSection.transform.position, finalPositionQuestion.position, speedQuestionSection);
            Movements(answerSection.transform.position, finalPositionAnswer.position, speedQuestionSection);
            /* Movements(questionSection.position, ,speedQuestionSection);
             Movements(answerSection.position, speedQuestionSection);*/
            Debug.Log("ejecutandose");
        }
    }

    public void Movements(Vector3 initialPosition, Vector3 finalPosition, float speedMovement)
    {
        Vector3 direction = (finalPosition - initialPosition).normalized;
        Debug.Log("ejecutandose1");

        float distance = Vector3.Distance(initialPosition, finalPosition);
        Debug.Log("ejecutandose2");

        initialPosition += direction * speedMovement * Time.deltaTime;
        Debug.Log("ejecutandose3");

        if (distance <= speedMovement * Time.deltaTime)
        {
            initialPosition = finalPosition;
            Debug.Log("ejecutandose4");
        }
    }
}
