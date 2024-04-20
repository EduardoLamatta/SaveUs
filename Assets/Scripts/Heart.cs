using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : AudioManager
{
    private Vector3 initialScale;
    [SerializeField] private float factorAdditional;
    [SerializeField] private float speedPalpitation;
    [SerializeField] private float timePalpitation;
    private float timer_3 = 0;
    [SerializeField] private float timer_2, timeElapsedQuestion2;
    private float timer;
    [SerializeField] public float heartRateFactor;
    [SerializeField] private Questions questions;
    [SerializeField] public float timeElapsedQuestion;
    [SerializeField] private AudioClip audioClip;
    private float factorEscale;
    [SerializeField] private float maxFactorScale;
    private Transform entryPoint;

    [SerializeField] private AnswerSectionController answerSectionController;
    void Start()
    {
        initialScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (AnswerCounter.totalAnswer != 9)
        {
            if (timer > timePalpitation / heartRateFactor) StartCoroutine(Palpitacion());
            else transform.localScale = initialScale;
        }

        ModifyHeartRate();
    }

    private IEnumerator Palpitacion()
    {
        timer_3 += Time.deltaTime;
        factorEscale = Mathf.Sin(timer_3 * speedPalpitation * heartRateFactor / 2) * 0.5f + 0.5f;
        factorEscale = Mathf.Clamp(factorEscale, 0f, maxFactorScale);
        transform.localScale = initialScale * (1 + factorEscale * factorAdditional);
        if (factorEscale == maxFactorScale && !audioSource.isPlaying)
        {
            PlayAudioClip(audioClip);
        }
        yield return new WaitForSeconds(timer_2);
        timer = 0;
        timer_3 = 0;
    }

    private void ModifyHeartRate()
    {
        

        if (questions.questionComplete && timeElapsedQuestion < questions.currentQuestionTime && !AnswerCounter.questionAnswered)
        {
            timeElapsedQuestion2 -= Time.deltaTime;
            timeElapsedQuestion += Time.deltaTime;
            heartRateFactor = (1 + AnswerCounter.numIncorrectAnswers / 2f) + timeElapsedQuestion / 10;
            if (audioSource.pitch < 4)
            {
                audioSource.pitch = questions.currentQuestionTime / timeElapsedQuestion2;
            }
        }
        else if (timeElapsedQuestion >= questions.currentQuestionTime)
        {
            heartRateFactor = 1 + AnswerCounter.numIncorrectAnswers / 2f;
        }
        if (!questions.questionComplete)
        {
            timeElapsedQuestion = 0;
        }
        
    }

    public Transform SetEntryPointAnswerSection()
    {
        entryPoint = answerSectionController.GetEntryPoint();
        return entryPoint;
    }

}
