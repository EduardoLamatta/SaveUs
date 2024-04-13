using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnswers : ButtonsAnswer
{
    List<Transform> buttonList = new List<Transform>();
    List<int> indexButtonRandom = new List<int>();
    [SerializeField] private GameObject[] buttonsAnswers = new GameObject[3];
    [SerializeField] private Button[] buttons = new Button[3];
    [SerializeField] List<int> numButtonList = new List<int>();
    [SerializeField] private TextMeshProUGUI[] textButtons;
    [SerializeField] private Transform layoutButtons;
    [SerializeField] private Questions questions;
    [SerializeField] private GameObject buttonQuestion;
    [SerializeField] private TextAsset excelText;
    [SerializeField] int numRowExcel;
    [SerializeField] public GameObject slider;
    [SerializeField] private float effectInterval;
    [SerializeField] private float effectInterval1;
    [SerializeField] private float timeDeactivate;
    [SerializeField] private int repeatTimes;
    public float time;
    private bool showAnswers;
    private bool allowEffectButtons;
    public bool finishEffectButtons;
    
    private void Start()
    {
        layoutButtons = gameObject.transform;
        DeactivateButtons(buttonsAnswers, buttons, questions);
        AddButtonChildren(buttonList, layoutButtons);
        ReadExcelDialogues(excelText, numRowExcel);
    }
    private void Update()
    {
        if (time >= timeDeactivate)
        {
            DeactivateButtons(buttonsAnswers, buttons, questions);
        }
        
        if (AnswerCounter.questionAnswered)
        {
            time += Time.deltaTime;
            if (time <= timeDeactivate && !allowEffectButtons)
            {
                StartCoroutine(EffectCorrectAnswer());
            }
        }
        
        ActivateButtons(buttonsAnswers, buttons, questions);
        
        if (numAnswers < tableSize && questions.questionComplete && showAnswers)
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
        AnswerCounter.questionAnswered = false;
        slider.SetActive(false);
    }
    private void AnswerInButtons()
    {
        slider.SetActive(true);
        textButtons[0].text = answer_a[numAnswers];
        textButtons[1].text = answer_b[numAnswers];
        textButtons[2].text = answer_c[numAnswers];
        showAnswers = false;
        numAnswers++;
        ClearListButtons(buttonList, indexButtonRandom, numButtonList);
        
    }

    private IEnumerator EffectCorrectAnswer()
    {
        DeactivateButtons(buttonsAnswers, buttons, questions);
        allowEffectButtons = true;
        for (int i = 0; i < repeatTimes; i++) 
        {
            buttonsAnswers[2].SetActive(false);
            yield return new WaitForSeconds(effectInterval);
            buttonsAnswers[2].SetActive(true);
            yield return new WaitForSeconds(effectInterval1);
        }
        finishEffectButtons = true;
    }

    public void DisallowEffect()
    {
        allowEffectButtons = false;
        time = 0;
        finishEffectButtons = false;
    }

}
