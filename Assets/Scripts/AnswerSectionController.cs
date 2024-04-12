using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSectionController : SceneEffects
{
    [SerializeField] private GameObject answerSection;
    [SerializeField] private Transform finalPosition;
    [SerializeField] private float speedMovement;
    public bool sectionAnswerinGame;


    private void Update()
    {
        MovementsInGame(finalPosition.position, speedMovement);
    }


}
