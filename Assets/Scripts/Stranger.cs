using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stranger : PeopleController
{
    [SerializeField] private float velocityMovement;
    [SerializeField] private GameObject startButton, questionSection, answerSection;
    private bool moveStranger = true; 
    [SerializeField] private Transform finalPosition;
    private SceneEffects effects;
    [SerializeField] private Transform entryPoint, finalPoint;
    [SerializeField] private float timeNextChar;
    [SerializeField] private float entryTime, timeNextPhrases;
    [SerializeField] private GameObject textGameObject, buttonNextDialogue;
    [SerializeField] private TextMeshProUGUI textPeople;
    [SerializeField] private int numberRow;
    [SerializeField] private bool randomDialogue;
    [SerializeField] private int numRowInExcel;
    [SerializeField] private TextAsset excelPhrases;
    [SerializeField] private int lineIdexStarnger = 0;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioVoice;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        effects = GetComponent<SceneEffects>();
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        DictionaryRowsInExcel();
    }

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
        
        SetIndexText();
        if (lineIdexStarnger < dictRowInExcel[numberRow].Length && textPeople.text == dictRowInExcel[numberRow][lineIdexStarnger] && transform.position == entryPoint.position)
        {
            ActivateButtonStranger(buttonNextDialogue);
        }
        /*if (lineIdexStarnger == dictRowInExcel[numberRow].Length)
        {
            buttonNextDialogue.SetActive(false);
        }*/
    }
    public void DialogueStranger()
    {
        if (lineIdexStarnger < dictRowInExcel[numberRow].Length && transform.position != finalPosition.position)
        {
            startButton.SetActive(false);
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[numberRow], randomDialogue, buttonNextDialogue, audioVoice, audioSource);
            audioManager.PlayAudioClipButton();
        }
    }
    public void MoveRight()
    {
        if (lineIdexStarnger >= dictRowInExcel[numberRow].Length - 1)
        {
            moveStranger = false;
            effects.MovementsInGame(finalPosition.position, velocityMovement);
            textGameObject.SetActive(false);
            buttonNextDialogue.SetActive(false);
        }
    }
    public void ActivateQuestionSection()
    {
        questionSection.SetActive(true);
    }
    public void ActivateAnswerSection()
    {
        answerSection.SetActive(true);
    }
    private int SetIndexText()
    {
        lineIdexStarnger = GetIndexText();
        return lineIdexStarnger;
    }

}
