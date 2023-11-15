using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("Basic Add Score")]
    [Space]
    public int score;
    public int point = 10;
    [Space]
    public float timeDisplayPoint;
    [Space]
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textPoint;
    [Space]

    [Header("MiniGameScore")]
    [Space]
    public float timeStartMiniGame;
    public float timeEndMiniGame;
    public float timePastInGame;
    [Space]
    public int miniGamePoint;
    [Space]

    [Header("Score Slider")]
    public Slider scoreSlider;
    private void Start()
    {
        textPoint.enabled = false;
        textPoint.text = "+ " + point;
    }
    // Update is called once per frame
    void Update()
    {
        textScore.text = score.ToString();

        //Input de test 
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            AddPointToScore(point);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GetTime();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AddTimePoint(100, 60);
        }
        //Debug.Log(timeStartMiniGame);
    }

    public void AddPointToScore(int score)
    {
        this.score = this.score + score;
        textPoint.enabled = true;
        Invoke("RemovePointText", timeDisplayPoint);
    }

    public void RemovePointText()
    {
        textPoint.enabled = false;
    }


    //A ajouter dans la win reaction, rentrer dans le code les point gagnables et la durée du jeu
    public void AddTimePoint(int maxPoint, int timeMaxMiniGame)
    {
        //Recupere le temps à la fin du jeu
        timeEndMiniGame = Time.time;
        //Calcule le temps passé dans le minijeu
        timePastInGame =(int)timeEndMiniGame - (int)timeStartMiniGame;
        //Produit en crois en fonction du temps de jeu et du max de point pouvant être gagné
        miniGamePoint = (maxPoint * (int)timePastInGame) / timeMaxMiniGame;
        score = score + miniGamePoint;
    }

    public void GetTime()
    {
        //Recupere le temps à l'instant ou le mini jeu se lance
        timeStartMiniGame = Time.time;
        //Debug.Log(timeStartMiniGame);
    }


}
