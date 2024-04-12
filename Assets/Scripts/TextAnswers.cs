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
    [SerializeField] private Questions questions;
    [SerializeField] private GameObject buttonQuestion;
    [SerializeField] private TextAsset excelText;
    [SerializeField] int numRowExcel;
    [SerializeField] public GameObject slider;
    [SerializeField] private float effectInterval;
    [SerializeField] private float effectInterval1;
    [SerializeField] private float timeDeactivate;
    public float time;
    private bool showAnswers;
    private bool algo;
    private void Start()
    {
        layoutButtons = gameObject.transform;
        DeactivateButtons(buttonsAnswers, questions);
        AddButtonChildren(buttonList, layoutButtons);
        ReadExcelDialogues(excelText, numRowExcel);
    }
    private void Update()
    {
        if (time >= timeDeactivate)
        {
            DeactivateButtons(buttonsAnswers, questions);
        }
        
        if (AnswerCounter.questionAnswered)
        {
            time += Time.deltaTime;
            if (time <= timeDeactivate && !algo)
            {
                StartCoroutine(EffectCorrectAnswer());
            }
        }
        
        ActivateButtons(buttonsAnswers, questions);
        
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
        DeactivateButtons(buttonsAnswers, questions);
        algo = true;
        for (int i = 0; i < 7; i++) 
        {
            buttonsAnswers[2].SetActive(false);
            yield return new WaitForSeconds(effectInterval);
            buttonsAnswers[2].SetActive(true);
            yield return new WaitForSeconds(effectInterval1);

        }
    }

    public void DisallowEffect()
    {
        algo = false;
        time = 0;
    }

}
