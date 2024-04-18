using TMPro;
using UnityEngine;

public class FinalText : PeopleController
{
    [SerializeField] private TextAsset excelPhrases;
    [SerializeField] private int numRowInExcel;
    private int numberRow;
    [SerializeField] private float timeNextChar;
    [SerializeField] private GameObject textGameObject, buttonNextDialogue, nextDialogueAccept, nextDialogueReject;
    [SerializeField] private TextMeshProUGUI textPeople;
    [SerializeField] private bool randomDialogue;
    [SerializeField] private int numCorrects, numTotal, numIncorrects;
    [SerializeField] private GameObject buttonStart, buttonAccepDeal, buttonRejectDeal;
    private int lineIdexStarnger;
    [SerializeField] Animator animator;
    private bool deal, dealaccepted, dealRejected;
    
    void Start()
    {
        deal = false;
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        DictionaryRowsInExcel();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(strangerDialogue);
        if (numCorrects == 9)
        {
            numberRow = 1;
        }
        if (numIncorrects > 0)
        {
            numberRow = 2;
        }

        SetIndexText();
        if (lineIdexStarnger < dictRowInExcel[numberRow].Length && textPeople.text == dictRowInExcel[numberRow][lineIdexStarnger])
        {
            if (!deal)
            {
                ActivateButtonStranger(buttonNextDialogue);
            }
            if (dealaccepted)
            {
                nextDialogueAccept.SetActive(true);
            }
            if (dealRejected)
            {
                nextDialogueReject.SetActive(true);
            }
        }
        if (lineIdexStarnger == dictRowInExcel[numberRow].Length - 1 && !deal)
        {
            DeactivateButtonStranger(buttonNextDialogue);
            textGameObject.SetActive(false);

            if (numIncorrects > 0)
            {
                Deal();
            }
        }
        if (numTotal == 9 &&  !strangerDialogue && !deal)
        {
            buttonStart.SetActive(true);
        }

    }
    public void StartFinalDialogue()
    {
        if (!deal)
        {
            if (numCorrects == 9)
            {
                StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[1], randomDialogue, buttonNextDialogue, false);
                numberRow = 1;
                buttonStart.SetActive(false);
            }
            if (numIncorrects > 0)
            {
                StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[2], randomDialogue, buttonNextDialogue, false);
                numberRow = 2;
                buttonStart.SetActive(false);
            }
        }
    }

    public void Deal()
    {
        buttonAccepDeal.SetActive(true);
        buttonRejectDeal.SetActive(true);
    }

    public void ButtonAcceptDeal()
    {
        dealaccepted = true;

        if (dealaccepted)
        {
            lineIdexStarnger = 0;
            deal = true;
            strangerDialogue = false;
            textGameObject.SetActive(true);
            numberRow = 3;
            Debug.Log("si");
            buttonAccepDeal.SetActive(false);
            buttonRejectDeal.SetActive(false);
        }
    }
    public void ButtonRejectDeal()
    {
        dealRejected = true;
        if (dealRejected)
        {
            deal = true;
            strangerDialogue = false;
            Debug.Log("no");
            textGameObject.SetActive(true);
            numberRow = 4;
            buttonAccepDeal.SetActive(false);
            buttonRejectDeal.SetActive(false);
        }
    }
    private void ButtonNextDialogue()
    {
        if (dealaccepted)
        {
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[3], randomDialogue, nextDialogueAccept, true);
        }
        if (dealRejected)
        {
            StartDialogueStranger(textGameObject, textPeople, timeNextChar, dictRowInExcel[4], randomDialogue, nextDialogueReject, true);
        }
    }

    private int SetIndexText()
    {
        lineIdexStarnger = GetIndexText();
        return lineIdexStarnger;
    }
}
