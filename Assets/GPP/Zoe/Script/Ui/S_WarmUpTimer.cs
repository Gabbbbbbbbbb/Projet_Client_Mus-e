using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_WarmUpTimer : MonoBehaviour
{
    public static S_WarmUpTimer instance;


    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI playBegin;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject warmUpTimer;
    [SerializeField] private GameObject beginTextGO;

    [SerializeField] private float timeTextBeforeBegin;
    [SerializeField] private float remainingTime;

    [SerializeField] private string textBegin;
    private float maxTime;

    

    [HideInInspector]
    public bool gameBegin;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameBegin = false;
        maxTime = remainingTime;
        beginTextGO.SetActive(false);
        warmUpTimer.SetActive(true);
    }
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(remainingTime <= 0 && !gameBegin)
        {
            warmUpTimer.SetActive(false);
            Debug.Log("okok");
            beginTextGO.SetActive(true);
            playBegin.text = textBegin;
            Invoke("ShowTimer", timeTextBeforeBegin);
        }
    }

    void ShowTimer()
    {
        beginTextGO.SetActive(false);
        timer.SetActive(true);
        Debug.Log("kldskfo");
        gameBegin = true;
    }

}
