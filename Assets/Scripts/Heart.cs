using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Vector3 initialScale;
    [SerializeField] private float factorScale;
    [SerializeField] private float speedPalpitation;
    [SerializeField] private float timePalpitation;
    private float timer_3 = 0;
    [SerializeField] private float timer_2;
    private float timer;
    [SerializeField] public float heartRateFactor;
    [SerializeField] private Questions questions;
    [SerializeField] public float timeElapsedQuestion;

    void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timePalpitation / heartRateFactor) StartCoroutine(Palpitacion());
        else transform.localScale = initialScale;

        ModifyHeartRate();
    }

    private IEnumerator Palpitacion()
    {
        timer_3 += Time.deltaTime;

        float factorEscala = Mathf.Sin(timer_3 * speedPalpitation * heartRateFactor / 2) * 0.5f + 0.5f;
        transform.localScale = initialScale * (1 + factorEscala * factorScale);
        yield return new WaitForSeconds(timer_2);
        timer = 0;
        timer_3 = 0;
    }

    private void ModifyHeartRate()
    {
        if (questions.questionComplete && timeElapsedQuestion < questions.currentQuestionTime && !ScriptEachButton.questionAnswered)
        {
            timeElapsedQuestion += Time.deltaTime;
            heartRateFactor = (1 + ScriptEachButton.numIncorrecctAnswers / 2f) + timeElapsedQuestion / 10;
        }
        else if (timeElapsedQuestion >= questions.currentQuestionTime)
        {
            heartRateFactor = 1 + ScriptEachButton.numIncorrecctAnswers / 2f;
        }
        if (!questions.questionComplete)
        {
            timeElapsedQuestion = 0;
        }

    }

}
