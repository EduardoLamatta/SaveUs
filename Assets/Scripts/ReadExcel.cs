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
    public string[] rowExcel1;
    public string[] rowExcel2;
    public string[] rowExcel3;
    public string[] rowExcel4;
    public Dictionary<int, string[]> dictRowInExcel = new Dictionary<int, string[]>();
    public void ReadExcelDialogues(TextAsset excelText, int numRowExcel)
    {
        dialogues = excelText.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
        tableSize = dialogues.Length / numRowExcel - 1;

        rowExcel1 = new string[tableSize];
        rowExcel2 = new string[tableSize];
        rowExcel3 = new string[tableSize];
        rowExcel4 = new string[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            rowExcel1[i] = dialogues[numRowExcel * (i + 1)];
            rowExcel2[i] = dialogues[numRowExcel * (i + 1) + 1];
            rowExcel3[i] = dialogues[numRowExcel * (i + 1) + 2];
            rowExcel4[i] = dialogues[numRowExcel * (i + 1) + 3];
        }
    }
    public void DictionaryRowsInExcel()
    {
        dictRowInExcel.Add(1, rowExcel1);
        dictRowInExcel.Add(2, rowExcel2);
        dictRowInExcel.Add(3, rowExcel3);
        dictRowInExcel.Add(4, rowExcel4);
    }

}
