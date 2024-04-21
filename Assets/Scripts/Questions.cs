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
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform finalPositionInside, finalPositionExit;
    [SerializeField] TMP_Text textQuestions;
    private SceneEffects sceneEffects;
    private float timeToChars = 0.04f;
    public float initialQuestionTime;
    public float currentQuestionTime;
    public float timeElapsed = 0, timeDeactiveButton = 0;
    private bool changeTimeElapsed;
    public bool inGame;
    private bool  answerSectionInGame;
    [SerializeField] private PeopleController peopleController;
    [SerializeField] private float waitTime;
    [SerializeField] private float waitTimeAfterReply;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource audioSource;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneEffects = GetComponent<SceneEffects>();
        inGame = false;
        inDialogue = false;
        ReadExcelDialogues(excelText, numRowExcel);
        currentQuestionTime = initialQuestionTime;
        DeactivateButtonQuestion();
    }

    private void Update()
    {
        ModifyTimeQuestion();
        CallNullAnswers();
        ChangeTimeElpased();
        MovementInScene();
        MovementExitScene();

        
        if (PeopleController.allowQuestion)
        {       
            ShowText(textQuestions);
        }



        if (finalPositionInside.position == transform.position && !inGame)
        {
            inGame = true;
            ActivateButtonQuestion();
        }

        if (ButtonsAnswer.numAnswers == 10 || currentQuestionTime <= 0)
        {
            answerSectionInGame = true;
        }

        if (AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3 && AnswerCounter.questionAnswered)
        {
            timeDeactiveButton += Time.deltaTime;
            if (timeDeactiveButton > waitTime)
            {
                ActivateButtonQuestion();
                timeDeactiveButton = 0;
            }
        }
    }

    private void MovementInScene()
    {
        if (!inGame)
        {
            sceneEffects.MovementsInGame(finalPositionInside.position, movementSpeed);
        }
    }
    private void MovementExitScene()
    {
        if (answerSectionInGame)
        {
            sceneEffects.MovementsInGame(finalPositionExit.position, movementSpeed);
            DeactivateButtonQuestion();
        }
        
    }

    private void CallNullAnswers()
    {
        if (questionComplete)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > currentQuestionTime && !AnswerCounter.questionAnswered)
            {
                timeElapsed = 0;
                AnswerCounter.NullAnswer();
            }
            if (timeElapsed >= initialQuestionTime && AnswerCounter.totalAnswer / 3 != (int)AnswerCounter.totalAnswer / 3 && AnswerCounter.questionAnswered)
            {
                ActivateButtonQuestion();
                timeElapsed = 0;
            }
        }
    }
    private void ChangeTimeElpased()
    {
        if (AnswerCounter.questionAnswered && !changeTimeElapsed && PeopleController.allowQuestion)
        {
            timeElapsed = initialQuestionTime  - waitTimeAfterReply;
            changeTimeElapsed = true;
        }
    }
    public void ShowQuestionButton()
    {
        if (currentQuestionTime > 0)
        {
            StartDialogue(rowExcel1, quiestionText);
            questionComplete = false;
            changeTimeElapsed = false;
            timeElapsed = 0;
            peopleController.Active();
            peopleController.Deactive();
            audioManager.PlayAudioClipButton();
        }

        if (!PeopleController.allowQuestion)
        {
            HideText(textQuestions);
        }
    }

    public void StartDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {

        if (lineIndex < dialoguesChar.Length - 1 && PeopleController.allowQuestion)
        {
            if (!inDialogue)
            {
                StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
            }
            else if (dialogueText.text == dialoguesChar[lineIndex])
            {
                NextDialogue(dialoguesChar, dialogueText);
            }

        }
        

    }

    private void NextDialogue(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
            lineIndex++;

        if (lineIndex < dialoguesChar.Length - 1)
        {
            StartCoroutine(DialogueSystem(dialoguesChar, dialogueText));
        }
    }

    private IEnumerator DialogueSystem(string[] dialoguesChar, TextMeshProUGUI dialogueText)
    {
        DeactivateButtonQuestion();
        dialogueText.text = string.Empty;
        inDialogue = true;
        foreach (char ch in dialoguesChar[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(timeToChars);
        }
        questionComplete = true;
        
    }

    private void DeactivateButtonQuestion()
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

    private void HideText(TMP_Text text)
    {
        Color textColor = text.color;
        textColor.a = 0;
        text.color = textColor;
    }
    private void ShowText(TMP_Text text)
    {
        Color textColor = text.color;
        textColor.a = 1;
        text.color = textColor;
    }

}
