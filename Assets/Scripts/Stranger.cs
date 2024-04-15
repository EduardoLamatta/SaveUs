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
    private void Start()
    {
        effects = GetComponent<SceneEffects>();
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        DictionaryRowsInExcel();
    }

    public void Update()
    {
        Debug.Log(lineIdexStarnger);

        if (!moveStranger)
        {
            MoveRight();
            Debug.Log("si2");

        }
        if (transform.position == finalPosition.position)
        {
            moveStranger = true;
            ActivateQuestionSection();
            ActivateAnswerSection();
        }
        SetIndexText();

        if (lineIdexStarnger < dictRowInExcel[numberRow].Length && textPeople.text == dictRowInExcel[numberRow][lineIdexStarnger])
        {
            Debug.Log("si6");
            ActivateButtonStranger(buttonNextDialogue);
        }
    }
    public void DialogueStranger()
    {
        if (lineIdexStarnger < dictRowInExcel[numberRow].Length)
        {
            startButton.SetActive(false);
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[numberRow], randomDialogue, buttonNextDialogue);
        }
    }
    public void MoveRight()
    {
        if (lineIdexStarnger >= dictRowInExcel[numberRow].Length - 1)
        {
            moveStranger = false;
            effects.MovementsInGame(finalPosition.position, velocityMovement);
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
