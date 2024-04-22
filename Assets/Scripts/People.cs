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
    [SerializeField] private int rangeMin, rangeMax, numToTrans;
    [SerializeField] private int numPeople;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip audioVoice;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int counterToTransform;
    [Range(0, 1)]
    [SerializeField] private float alphaValue = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        StartDialogue(entryPoint, textGameObject, timeNextPhrases, textPeople, timeNextChar, dictRowInExcel[numberRow], randomDialogue, audioVoice, audioSource);

        if (AnswerCounter.totalAnswer < 9)
        {
            ExitScena(sceneEffects, finalPoint, entryVelocity, timeTransformation, numPeople);
        }
        if (AnswerCounter.nullAnswer)
        {
            CounterTransformAnswer();
        }

        TransformPeople();
    }

    public void CounterTransformAnswer()
    {
        if (gameObject.activeSelf)
        {
            counterToTransform++;
            Debug.Log("People:    " + counterToTransform);
            AnswerCounter.nullAnswer = false;
        }
    }
    /*public void CounterTransformNullAnswer()
    {
        if (gameObject.activeSelf && AnswerCounter.nullAnswer)
        {
            counterToTransform++;
            AnswerCounter.nullAnswer = false;
        }
    }*/
    private void TransformPeople()
    {
        if ((int)AnswerCounter.totalAnswer / 3 >= rangeMin && (int)AnswerCounter.totalAnswer / 3 <= rangeMax)
        {
            if (counterToTransform == 1)
            {
                FirstTransformation(animator);
            }
            else if (counterToTransform == 2)
            {
                SecondTransformation(animator);
            }
            else if (counterToTransform == 3)
            {
                ThirdTransformation(animator);
            }
        }
    }
    
    public int GetCounterToTransform()
    {
        return counterToTransform;
    }



}
