using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalDialogue : PeopleController
{
    [SerializeField] private TextAsset excelPhrases;
    [SerializeField] private int numRowInExcel;
    [SerializeField] private int numberRow;
    [SerializeField] private float timeNextChar;
    [SerializeField] private GameObject textGameObject, buttonNextDialogue;
    [SerializeField] private TextMeshProUGUI textPeople;
    [SerializeField] private bool randomDialogue;
    [SerializeField] private int algo;
    void Start()
    {
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        DictionaryRowsInExcel();
    }

    private void Update()
    {
        //algo = (int)AnswerCounter.numCorrecctAnswers;
    }
    public void StartFinalDialogue()
    {
        if (algo == 9)
        {
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[1], randomDialogue, buttonNextDialogue);
        }
        if (AnswerCounter.numIncorrectAnswers > 0)
        {
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[2], randomDialogue, buttonNextDialogue);
        }
    }
    public void AcceptDeal()
    {
        StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[3], randomDialogue, buttonNextDialogue);
    }
    public void RejectDeal()
    {
        StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[4], randomDialogue, buttonNextDialogue);
    }


}
