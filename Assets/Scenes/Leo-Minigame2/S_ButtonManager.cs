using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class S_ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private Button[] buttons;

    private int currentButtonTarget = 0;

    private void Start()
    {
        currentButtonTarget = 0;
    }
    public void ClickButton()
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
