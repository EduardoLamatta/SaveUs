using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResults : SceneEffects
{
    [SerializeField] private Questions questions;
    [SerializeField] private Transform positionSoon, positionWife, positionFather;
    [SerializeField] private GameObject soon, positionSoonGO, wife, positionWifeGO, father, positionFatherGO, textFinal;
    [SerializeField] private float waitTime;
    [SerializeField] private People peopleFather;
    private int counterTransformMonster;
    private float timeToFinishScene;
    private void Update()
    {
        Debug.Log(counterTransformMonster);
        if (AnswerCounter.totalAnswer == 9)
        {
            PositionsPeopleToFinal();
        }
        else if (questions.currentQuestionTime <= 0 && questions.inGame)
        {
            PositionsPeopleToFinal();
        }
    }
    private void PositionsPeopleToFinal()
    {
        timeToFinishScene += Time.deltaTime;

        if (timeToFinishScene > waitTime)
        {
            setCounterTransformFather();
            positionSoonGO.SetActive(true);
            positionWifeGO.SetActive(true);
            soon.SetActive(true);
            wife.SetActive(true);
            textFinal.SetActive(true);
            soon.transform.position = positionSoon.position;
            wife.transform.position = positionWife.position;

            if (counterTransformMonster > 0)
            {
                positionFatherGO.SetActive(true);
                father.SetActive(true);
                father.transform.position = positionFather.position;
            }
        }
    }

    private void setCounterTransformFather()
    {
        counterTransformMonster = peopleFather.GetCounterToTransform();
    }

}
