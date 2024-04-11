using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Questions question;
    [SerializeField] private float initialValueSlider;
    private void Start()
    {
        timeSlider = GetComponent<Slider>();
        initialValueSlider = timeSlider.maxValue;
    }
    void Update()
    {
        TimeSlideLength();
    }

    private void TimeSlideLength()
    {
        if (question.questionComplete && !ScriptEachButton.questionAnswered)
        {
            timeSlider.value = initialValueSlider - question.timeElapsed;
        }
        else if (!question.questionComplete || ScriptEachButton.questionAnswered)
        {
            timeSlider.value = initialValueSlider;
        }
    }
}
