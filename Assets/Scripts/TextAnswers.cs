using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnswers : ButtonsAnswer
{

    List<Transform> buttonList = new List<Transform>();
    List<int> indexButtonRandom = new List<int>();
    [SerializeField] private GameObject[] buttonsAnswers = new GameObject[3];
    [SerializeField] List<int> numButtonList = new List<int>();
    [SerializeField] private TextMeshProUGUI[] textButtons;
    [SerializeField] private Transform layoutButtons;
    [SerializeField] private Questions dialogue;
    [SerializeField] private GameObject buttonQuestion;
    [SerializeField] private TextAsset excelText;
    [SerializeField] int numRowExcel;
    private bool showAnswers;
    private void Start()
    {
        layoutButtons = gameObject.transform;
        DeactivateButtons(buttonsAnswers, dialogue);
        AddButtonChildren(buttonList, layoutButtons);
        ReadExcelDialogues(excelText, numRowExcel);
    }
    private void Update()
    {
        DeactivateButtons(buttonsAnswers, dialogue);
        ActivateButtons(buttonsAnswers, dialogue);

        if (numAnswers < tableSize && dialogue.questionComplete && showAnswers)
        {
            AddIndexButtons(buttonList, numButtonList);
            RandomIndexList(buttonsAnswers, indexButtonRandom, numButtonList);
            RandomButtons(buttonList, indexButtonRandom);
            AnswerInButtons();
            AddButtonChildren(buttonList, layoutButtons);
        }
    }
    public void ShowAswersButton()
    {
        showAnswers = true;
        ScriptEachButton.questionAnswered = false;
    }
    private void AnswerInButtons()
    {
        textButtons[0].text = answer_a[numAnswers];
        textButtons[1].text = answer_b[numAnswers];
        textButtons[2].text = answer_c[numAnswers];
        showAnswers = false;
        numAnswers++;
        ClearListButtons(buttonList, indexButtonRandom, numButtonList);
    }



}
