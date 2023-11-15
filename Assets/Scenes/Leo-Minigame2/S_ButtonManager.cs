using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class S_ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private Button[] buttons;

    [Range(0f,10f)] [SerializeField] private float timeStartGame = 2f;
    private int currentButtonTarget = 0;
    private bool gameStarted = false;

    private void Start()
    {
        buttons.Shuffle();

        gameStarted = false;
        Invoke("StartGame", timeStartGame);
        currentButtonTarget = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
        }
    }
    public void ClickButton()
    {
        if (gameStarted)
        {
            Button buttonSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if (buttonSelected == buttons[currentButtonTarget])
            {
                buttonSelected.gameObject.GetComponent<Image>().enabled = false;
                currentButtonTarget++;
                if (currentButtonTarget == buttons.Length)
                {
                    victory.SetActive(true);
                }
            }
        }
    }

    public void StartGame()
    {
        //Hide Numbers
        foreach (Button button in buttons)
        {
            button.gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        //Activate possibility to click buttons
        gameStarted = true;
    }
}
public static class MyExtensions
{
    private static readonly System.Random rng = new System.Random();

    //Fisher - Yates shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}