using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public Score scoreFinal;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = scoreFinal.score;
        //scoreText.text = "{score:000000}";
        scoreText.text = string.Format("{0:000000}",score);
        //scoreText.text = $"Mon score est : {score:00} points";
    }
}
