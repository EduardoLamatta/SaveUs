using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Questions : ReadExcel
{
    [HideInInspector] public int lineIndex = 0;
    [HideInInspector] public bool inDialogue;
    [HideInInspector] public bool questionComplete;
    [SerializeField] TextMeshProUGUI quiestionText;
    [SerializeField] private GameObject buttonQuestion;
    [SerializeField] private TextAsset excelText;
    [SerializeField] int numRowExcel;
    private float timeToChars = 0.04f;
    public float initialQuestionTime;
    public float currentQuestionTime;
    public float timeElapsed = 0;
    private bool changeTimeElapsed;

    private void Start()
    {
        inDialogue = false;
        ReadExcelDialogues(excelText, numRowExcel);
        currentQuestionTime = initialQuestionTime;
    }

    private void Update()
    {
        ModifyTimeQuestion();
        CallNullAnswers();
        ChangeTimeElpased();
    }
    private void CallNullAnswers()
    {
        if (questionComplete)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > currentQuestionTime)
            {
                ActivateButtonQuestion();
                timeElapsed = 0;
                AnswerCounter.NullAnswer();
            }
        }
    }
    private void ChangeTimeElpased()
    {
        if (AnswerCounter.questionAnswered && !changeTimeElapsed)
        {
            timeElapsed = currentQuestionTime - 5;
            changeTimeElapsed = true;
        }
    }
    public void ShowQuestionButton()
    {
        if (currentQuestionTime > 0)
        {
            StartDialogue(question, quiestionText);
            questionComplete = false;
            changeTimeElapsed = false;
            timeElapsed = 0;
        }
    }

    public void StartDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {

        if (lineIndex < dialoguesChar.Length)
        {
            if (!inDialogue)
            {
                StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
            }
            else if (dialogueText.text == dialoguesChar[lineIndex])
            {
                NextDialogue(dialoguesChar, dialogueText);
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialoguesChar[lineIndex];
                questionComplete = true;
            }

        }
    }

    private void NextDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
        lineIndex++;

        if (lineIndex < dialoguesChar.Length)
        {
            StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
        }
    }

    private IEnumerator DialogueSystem(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
        DesactivateButtonQuestion();
        dialogueText.text = string.Empty;
        inDialogue = true;
        foreach (char ch in dialoguesChar[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(timeToChars);
        }
        questionComplete = true;
    }

    private void DesactivateButtonQuestion()
    {
        buttonQuestion.SetActive(false);

    }
    private void ActivateButtonQuestion()
    {
        buttonQuestion.SetActive(true);
    }

    private void ModifyTimeQuestion()
    {
        currentQuestionTime = initialQuestionTime * (1.0f - AnswerCounter.numIncorrectAnswers / 5.0f);
    }
}
