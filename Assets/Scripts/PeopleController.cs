using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class PeopleController : ReadExcel
{
    private protected List<GameObject> peopleList = new List<GameObject>();
    public bool inDialoguePeople;
    private bool allowTransformation, completeTransformation;
    private int lineIndex = 0;
    private bool allowEntry;
    private float timeElpasedTrandfotmation, time, timePhrases;
    public static bool allowQuestion;

    void Start()
    {
        inDialoguePeople = false;
        allowEntry = false;
        completeTransformation = false;
        AddPeople();

    }
    public void AddPeople()
    {
        foreach (Transform children in transform)
        {
            peopleList.Add(children.gameObject);
        }
    }
    public void Deactive()
    {
        if (AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3 && (int)AnswerCounter.totalAnswer / 3 >= 1)
        {
            peopleList[(int)AnswerCounter.totalAnswer/3 - 1].gameObject.SetActive(false);
        }
    }
    public void Active()
    {
        if (AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3)
        {
            peopleList[(int)AnswerCounter.totalAnswer / 3].gameObject.SetActive(true);
        }
    }

    public void StartDialogue(Transform entryPoint, GameObject textGameObject, float timeNextPhrases, TextMeshProUGUI textPeople, float timeNextChar)
    {
        if (transform.position == entryPoint.position)
        {
            timePhrases += Time.deltaTime;
            textGameObject.SetActive(true);
            inDialoguePeople = true;
            if (timePhrases >= timeNextPhrases)
            {
                StartCoroutine(DialogueSystem(question, textPeople, timeNextChar));
                timePhrases = 0;
            }
        }
    }
    public IEnumerator DialogueSystem(string[] phrases, TextMeshProUGUI textPeopleGame, float timeNextChar)
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
    public float Transparency(CanvasRenderer renderer, float speed, TextAnswers textAnswers, float timeTransformation, float alphaValue)
    {
        if (AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3)
        {
            if (AnswerCounter.questionAnswered && alphaValue <= 1 && textAnswers.finishEffectButtons && !allowTransformation)
            {
                alphaValue += speed * Time.deltaTime;
                renderer.SetAlpha(alphaValue);
                if (alphaValue >= 1 && alphaValue <= 1.1)
                {
                    allowTransformation = true;
                }
            }
            if (AnswerCounter.questionAnswered && alphaValue >= 0 && textAnswers.finishEffectButtons && allowTransformation)
            {
                timeElpasedTrandfotmation += Time.deltaTime;
                if (timeElpasedTrandfotmation >= timeTransformation)
                {
                    alphaValue -= speed * Time.deltaTime;
                    renderer.SetAlpha(alphaValue);
                }
                if (alphaValue >= 0 && alphaValue <= 0.1)
                {
                    completeTransformation = true;
                }
            }
        }
        return alphaValue;
    }
    public void TransparencyNull(CanvasRenderer renderer, float alphaValue)
    {
        alphaValue = 0;
        renderer.SetAlpha(alphaValue);
    }
    public void EntryInScena(SceneEffects sceneEffects, Transform entryPoint, float entryVelocity)
    {
        if (allowEntry && !completeTransformation && AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3)
        {
            sceneEffects.MovementsInGame(entryPoint.position, entryVelocity);
        }
    }
    public void ExitScena(SceneEffects sceneEffects, Transform finalPoint, float entryVelocity)
    {
        if (completeTransformation && AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3)
        {
            sceneEffects.MovementsInGame(finalPoint.position, entryVelocity);
        }
    }
    public void BeginGame(Questions questions, float entryTime)
    {
        if (questions.inGame)
        {
            time += Time.deltaTime;
        }
        if (questions.inGame && time >= entryTime)
        {
            allowEntry = true;
        }
    }
    public void DeactivateTextGameObject(GameObject textGameObject)
    {
        textGameObject.SetActive(false);
    }
    public int GetPeopleInList()
    {
        int peopleInList = peopleList.Count;
        return peopleInList;
    }
}
