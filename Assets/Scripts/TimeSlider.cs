using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Questions question;
    [SerializeField] private float initialValueSlider;
    private void Awake()
    {
        timeSlider = GetComponent<Slider>();
        initialValueSlider = timeSlider.maxValue;
    }
    void Update()
    {
        TimeSlideLength();
    }
    public void TimeSlideLength()
    {
        if (question.questionComplete && !AnswerCounter.questionAnswered)
        {
            timeSlider.value = initialValueSlider - question.timeElapsed;
        }
        if (!question.questionComplete || AnswerCounter.questionAnswered && AnswerCounter.numIncorrectAnswers == 0)
        {
            timeSlider.value = initialValueSlider;
        }
    }

    public void TimeSlideMaxValue()
    {
        if (!question.questionComplete || AnswerCounter.questionAnswered && AnswerCounter.numIncorrectAnswers > 0)
        {
            timeSlider.maxValue = question.currentQuestionTime;
            initialValueSlider = question.currentQuestionTime;
        }
    }

}
