using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    private float timeStartMiniGame;
    private float timeEndMiniGame;
    public float timePastInGame;
    [Space]
    public int miniGamePoint;
    [Space]

    [Header("Score Slider")]
    public Image scoreSlider;
    public int maxScore;
    public float speed = 1f;
    float scoreValue = 0;
    private void Start()
    {
        //scoreSlider.maxValue = maxPoint;
        //scoreSlider.value = 0;
        textPoint.enabled = false;
        textPoint.text = "+ " + point;
        scoreValue = (float)score / maxScore;

    }
    // Update is called once per frame
    void Update()
    {
        
        //Input de test 
        if(Input.GetMouseButtonDown(0)) 
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
        //actualise la valeur du slider par rapport au score du joueur
        if (scoreSlider.fillAmount <= scoreValue)
        {
            scoreSlider.fillAmount += speed * Time.deltaTime;
            textScore.text = "" + (int)(scoreSlider.fillAmount * maxScore);
            //    int scoreValue = (int)scoreSlider.value;
            //    textScore.text = scoreValue.ToString();
        }
        
        //Debug.Log(scoreSlider.value);
    }

    public void AddPointToScore(int score)
    {
        this.score += score;
    }    

    public void AddPointText()
    {
        //affiche un texte temporaire pour montrer le gain de point
        textPoint.enabled = true;
        //supprime le texte temporaire au bout d'un certain temps décidé en amont
        Invoke("RemovePointText", timeDisplayPoint);
    }

    public void RemovePointText()
    {
        textPoint.enabled = false;
    }


    //A ajouter à la fin du minijeu, rentrer dans le code les point totaux gagnables et la durée du jeu estimé
    public void AddTimePoint(int maxPoint, int timeMaxMiniGame)
    {
        //Recupere le temps à la fin du jeu
        timeEndMiniGame = Time.time;
        //Calcule le temps passé dans le minijeu
        timePastInGame =(int)timeEndMiniGame - (int)timeStartMiniGame;
        //Produit en crois en fonction du temps de jeu et du max de point pouvant être gagné
        miniGamePoint = (maxPoint * (int)timePastInGame) / timeMaxMiniGame;
        score = score + miniGamePoint;
        //affiche un texte montrant le score actuel
        textScore.text = this.score.ToString();
    }

    //a ajouter au debut du minijeu
    public void GetTime()
    {
        //Recupere le temps à l'instant ou le mini jeu se lance
        timeStartMiniGame = Time.time;
        //Debug.Log(timeStartMiniGame);
    }


}
