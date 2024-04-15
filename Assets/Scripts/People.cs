using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class People : PeopleController
{
    private SceneEffects sceneEffects;
    [SerializeField] private float entryVelocity;
    [SerializeField] private Transform entryPoint, finalPoint;
    [SerializeField] private Questions questions;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private TextMeshProUGUI textPeople;
    [SerializeField] private int numRowInExcel;
    [SerializeField] private TextAsset excelPhrases;
    [SerializeField] private float entryTime, timeNextPhrases;
    [SerializeField] private float timeNextChar;
    [SerializeField] private CanvasRenderer peopleRenderer;
    [SerializeField] private float speedChangeAlpha;
    [SerializeField] private TextAnswers textAnswers;
    [SerializeField] private float timeTransformation;
    [SerializeField] private int numberRow;
    [SerializeField] private bool randomDialogue;
    [Range(0, 1)]
    [SerializeField] private float alphaValue;

    void Start()
    {
        sceneEffects = GetComponent<SceneEffects>();
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        TransparencyNull(peopleRenderer, alphaValue);
        DeactivateTextGameObject(textGameObject);
        DictionaryRowsInExcel();
    }
    void Update()
    {
        if (transform.position == entryPoint.position)
        {
            allowQuestion = true;
        }
        if (transform.position == finalPoint.position)
        {
            allowQuestion = false;
        }

        BeginGame(questions, entryTime);
        EntryInScena(sceneEffects, entryPoint, entryVelocity);
        StartDialogue(entryPoint, textGameObject, timeNextPhrases, textPeople, timeNextChar, dictRowInExcel[numberRow], randomDialogue);
        alphaValue = Transparency(peopleRenderer, speedChangeAlpha, textAnswers, timeTransformation, alphaValue);
        ExitScena(sceneEffects, finalPoint, entryVelocity);
    }
    




}
