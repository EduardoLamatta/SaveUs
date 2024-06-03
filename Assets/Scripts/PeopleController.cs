using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeopleController : ReadExcel
{
    private protected List<GameObject> peopleList = new List<GameObject>();
    public bool inDialoguePeople;
    private bool allowTransformation, completeTransformation;
    private int lineIndex = 0;
    private bool allowEntry;
    private float timePhrases;
    private float time;
    private float timeElpasedTrandfotmation;
    public static bool allowQuestion;
    public bool strangerDialogue = false;
    private bool allowExit;

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

    public void StartDialogue(Transform entryPoint, GameObject textGameObject, float timeNextPhrases, TextMeshProUGUI textPeople, float timeNextChar, string[] rowExcel, bool randomAllow, AudioClip audioVoice, AudioSource audioSource)
    {
        if (transform.position == entryPoint.position && randomAllow)
        {
            timePhrases += Time.deltaTime;
            textGameObject.SetActive(true);
            inDialoguePeople = true;
            if (timePhrases >= timeNextPhrases)
            {
                StartCoroutine(DialogueSystem(rowExcel, textPeople, timeNextChar, randomAllow, audioVoice, audioSource));
                timePhrases = 0;
            }
            
        }
    }
    public void StartDialogueStranger(GameObject textGameObject, TextMeshProUGUI textPeople, float timeNextChar, string[] rowExcel, bool randomAllow, GameObject buttonTextStranger, AudioClip audioVoice, AudioSource audioSource)
    {
        if (!randomAllow && lineIndex < rowExcel.Length - 1)
        {
            if (!strangerDialogue)
            {
                textGameObject.SetActive(true);

                strangerDialogue = true;
                StartCoroutine(DialogueSystem(rowExcel, textPeople, timeNextChar, randomAllow, audioVoice, audioSource));
            }
            else if (textPeople.text == rowExcel[lineIndex])
            {
                NextDialogue(textPeople, timeNextChar, rowExcel, randomAllow, audioVoice, audioSource);
            }
            if (textPeople.text != rowExcel[lineIndex])
            {
                DeactivateButtonStranger(buttonTextStranger);
            }
        }

    }

    private void NextDialogue(TextMeshProUGUI textPeople, float timeNextChar, string[] rowExcel, bool randomAllow, AudioClip audioVoice, AudioSource audioSource)
    {
        lineIndex++;

        if (lineIndex < rowExcel.Length)
        {
            StartCoroutine(DialogueSystem(rowExcel, textPeople, timeNextChar, randomAllow, audioVoice, audioSource));
        }
    }

    public IEnumerator DialogueSystem(string[] phrases, TextMeshProUGUI textPeopleGame, float timeNextChar, bool randomAllow, AudioClip audioVoice, AudioSource audioSource)
    {
        if(randomAllow)
        {
            lineIndex = Random.Range(0, tableSize);
        }

        inDialoguePeople = false;
        textPeopleGame.text = string.Empty;


        foreach (char ch in phrases[lineIndex])
        {
            audioSource.PlayOneShot(audioVoice);
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
        alphaValue = 1;
        renderer.SetAlpha(alphaValue);
    }
    public void EntryInScena(SceneEffects sceneEffects, Transform entryPoint, float entryVelocity)
    {
        if (allowEntry && !completeTransformation && AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3 && !allowExit)
        {
            sceneEffects.MovementsInGame(entryPoint.position, entryVelocity);
        }
    }
    public void ExitScena(SceneEffects sceneEffects, Transform finalPoint, float entryVelocity, float timeTransformation, int numPeople)
    {
        if (AnswerCounter.totalAnswer / 3 == numPeople &&  AnswerCounter.totalAnswer / 3 == (int)AnswerCounter.totalAnswer / 3)
        {
            timeElpasedTrandfotmation += Time.deltaTime;
            if (timeElpasedTrandfotmation >= timeTransformation)
            {
                allowExit = true;
            }

            if (allowExit)
            {
                sceneEffects.MovementsInGame(finalPoint.position, entryVelocity);
            }
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

    public int GetIndexText()
    {
        return lineIndex;
    }
    public void DeactivateButtonStranger(GameObject buttonNextDialogue)
    {
        buttonNextDialogue.SetActive(false);
    }
    public void ActivateButtonStranger(GameObject buttonNextDialogue)
    {
        buttonNextDialogue.SetActive(true);
    }

    public void FirstTransformation(Animator animator)
    {
        animator.SetBool("Tra_1", true);
    }
    public void SecondTransformation(Animator animator)
    {
        animator.SetBool("Tra_2", true);
    }
    public void ThirdTransformation(Animator animator)
    {
        animator.SetBool("Tra_3", true);
    }



}
