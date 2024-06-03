using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    private bool changeLanguage;
    [SerializeField] private GameObject gameScene;
    [SerializeField] private TextMeshProUGUI initialButtonText;
    [SerializeField] private TextMeshProUGUI finalGameText;

    public void SpanishLanguage()
    {
        changeLanguage = true;
        initialButtonText.text = "Diálogo";
        finalGameText.text = "!Ahora esta es tu familia! JAJA";
        FadeOut();
    }

    public void EnglishLanguage()
    {
        changeLanguage = false;
        FadeOut();
        initialButtonText.text = "Dialogue";
        finalGameText.text = "Now, this is your family! HAHA";
    }

    private void FadeOut()
    {
        gameScene.SetActive(true);
        gameObject.SetActive(false);
    }

    public bool GetChangeLanguage()
    {
        return changeLanguage;
    }
}
