using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PeopleController : ReadExcel
{
    [SerializeField] private SceneEffects sceneEffects;
    [SerializeField] private float entryVelocity;
    [SerializeField] private Transform entryPoint, finalPoint;
    [SerializeField] private Questions questions;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private TextMeshProUGUI textPeople;
    [SerializeField] private int numRowInExcel;
    [SerializeField] private TextAsset excelPhrases;
    private float time, timePhrases;
    [SerializeField] private float entryTime, timeNextPhrases;
    [SerializeField] private float timeNextChar;
    private int lineIndex = 0;
    private bool allowEntry;
    public static bool inDialoguePeople;
    private Material materialPeople;
    private Color colorPeople;

    [Range(0, 1)]
    [SerializeField] private float alphaValue;
    void Start()
    {
        allowEntry = false;
        sceneEffects = GetComponent<SceneEffects>();
        textGameObject.SetActive(false);
        ReadExcelDialogues(excelPhrases, numRowInExcel);
        inDialoguePeople = false;

        //Test color material

        materialPeople = GetComponent<Renderer>().material;
        colorPeople = materialPeople.color;

    }

    void Update()
    {
        if (questions.inGame)
        {
            time += Time.deltaTime;
        }
        if (questions.inGame && time >= entryTime)
        {
            allowEntry = true;
        }

        if (allowEntry)
        {
            sceneEffects.MovementsInGame(entryPoint.position, entryVelocity);
        }

        if (transform.position == entryPoint.position)
        {
            timePhrases += Time.deltaTime;
            textGameObject.SetActive(true);
            inDialoguePeople = true;
            if (timePhrases >= timeNextPhrases)
            {
                StartCoroutine(DialogueSystem(question, textPeople));
                timePhrases = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transparency(materialPeople, alphaValue);
        }    

    }
    private IEnumerator DialogueSystem(string[] phrases, TextMeshProUGUI textPeopleGame)
    {
        lineIndex = Random.Range(0, tableSize);
        inDialoguePeople = false;
        textPeopleGame.text = string.Empty;
        foreach (char ch in phrases[lineIndex])
        {
            textPeopleGame.text += ch;
            yield return new WaitForSeconds(timeNextChar);
        }
    }

    private void Transparency(Material mat, float alpha)
    {
        colorPeople.a = alphaValue * Time.deltaTime;
        materialPeople.color = colorPeople;
    }




}
