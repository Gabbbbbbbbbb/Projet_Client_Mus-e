using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Objectives : MonoBehaviour
{
    public static S_Objectives instance;

    [TextArea]
    [SerializeField] private string[] objectives = new string[3];

    [SerializeField] private TextMeshProUGUI objetiveText;

    [SerializeField] private S_Materials[] matObjective2 = new S_Materials[3];
    [SerializeField] private S_Materials[] matObjective3 = new S_Materials[3];
    private S_Materials currentMatObjective2, currentMatObjective3;

    [SerializeField] private int scoreBonusObj2, scoreBonusObj3;


    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        objetiveText.text = objectives[0];
        SetupObjectives();
    }

    public void SetupObjectives()
    {
        for (int i = 0; i < objectives[1].Length; ++i)
        {
            currentMatObjective2 = matObjective2[Random.Range(0, matObjective2.Length)];
            objectives[1] = string.Format(objectives[1], currentMatObjective2.displayName.ToLower());
        }
        for (int i = 0; i < objectives[2].Length; ++i)
        {
            currentMatObjective3 = matObjective3[Random.Range(0, matObjective3.Length)];
            objectives[2] = string.Format(objectives[2], currentMatObjective3.displayName.ToLower());
        }
    }

    public void NextObjective()
    {
        objetiveText.text = objectives[GameMode.instance.currentPhase];
    }

    public void CheckObjective(S_Recipes recipe)
    {
        if(GameMode.instance.currentPhase == 2)
        {
            foreach (var mat in recipe.requiredMaterials)
            {
                if (currentMatObjective2 == mat)
                {
                    S_ScoreSystem.instance.AddScore(scoreBonusObj2);
                }
            }
        }
        else if (GameMode.instance.currentPhase == 3)
        {
            foreach (var mat in recipe.requiredMaterials)
            {
                if (currentMatObjective3 == mat)
                {
                    S_ScoreSystem.instance.AddScore(scoreBonusObj3);
                }
            }
        }
    }
}
