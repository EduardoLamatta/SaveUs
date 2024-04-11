using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class ReadExcel : MonoBehaviour
{
    private string[] dialogues;
    [HideInInspector] public int tableSize;
    [HideInInspector] public string[] question;
    [HideInInspector] public string[] answer_a;
    [HideInInspector] public string[] answer_b;
    [HideInInspector] public string[] answer_c;

    public void ReadExcelDialogues(TextAsset excelText, int numRowExcel)
    {
        dialogues = excelText.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        tableSize = dialogues.Length / numRowExcel - 1;

        question = new string[tableSize];
        answer_a = new string[tableSize];
        answer_b = new string[tableSize];
        answer_c = new string[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            question[i] = dialogues[numRowExcel * (i + 1)];
            answer_a[i] = dialogues[numRowExcel * (i + 1) + 1];
            answer_b[i] = dialogues[numRowExcel * (i + 1) + 2];
            answer_c[i] = dialogues[numRowExcel * (i + 1) + 3];
        }
    }

}
