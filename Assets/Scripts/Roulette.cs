using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    private List<string> answers = new List<string>();
    private List<string> randomIndexAnswer = new List<string>();
    private List<int> indexListAnswer;
    [SerializeField] private protected GameObject correctAnswerPref, incorrectAnswerPref;
    [SerializeField] private protected float rotationSpeed;
    [SerializeField] private protected float rotationTime;
    [SerializeField] private protected int numCorrects, numIncorrects;

    void Start()
    {
        ReadAnswers();
        RandomAnswers();
        InstantiateAnswers();

    }
    private void ReadAnswers()
    {
        for (int i = 0; i < numCorrects; i++)
        {
            answers.Add("0");
        }
        for (int i = 0; i < numIncorrects; i++)
        {
            answers.Add("1");
        }
    }
    private void RandomAnswers()
    {
        indexListAnswer = new List<int>(answers.Count);

        for (int i = 0; i < answers.Count;i++)
        {
            indexListAnswer.Add(i);
        }
        for(int i = 0; i < answers.Count; i++)
        {
            int numRandom = Random.Range(0, indexListAnswer.Count);
            randomIndexAnswer.Add(answers[indexListAnswer[numRandom]]);
            indexListAnswer.RemoveAt(numRandom);
        }
    }
    private void InstantiateAnswers()
    {
        for (int i = 0;i < randomIndexAnswer.Count;i++)
        {
            Vector3 initialPosition = new Vector3 (0,0,0);
            Vector3 nextPosition = initialPosition + Vector3.up * i * 2;
            if (randomIndexAnswer[i] == "0")
            {
                GameObject answer = Instantiate(correctAnswerPref, nextPosition, Quaternion.identity);
                answer.transform.SetParent(transform);
            }
            else if (randomIndexAnswer[i] == "1")
            {
                GameObject answer = Instantiate(incorrectAnswerPref, nextPosition, Quaternion.identity);
                answer.transform.SetParent(transform);
            }
        }
    }


}
