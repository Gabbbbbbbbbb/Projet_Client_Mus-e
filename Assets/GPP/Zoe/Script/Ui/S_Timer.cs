using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Timer : MonoBehaviour
{
    public static S_Timer instance;

    public TextMeshProUGUI timerText;
    public float remainingTime;
    public float timeSinceBeggining;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        remainingTime = GameMode.instance.settings[0].timePhase;
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        timeSinceBeggining += Time.deltaTime;
        if (remainingTime <= 0.1f)
        {
            GameMode.instance.EndGame();
        }
        else
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    public void TimerNextPhase(PhaseSettings settings)
    {
        remainingTime = settings.timePhase;
    }
}
