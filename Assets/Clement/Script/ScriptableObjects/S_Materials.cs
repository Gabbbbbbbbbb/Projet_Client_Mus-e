using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/New Material", order = 1)]

public class S_Materials : ScriptableObject
{
    public string materialName;
    [TextAreaAttribute] public string description;
    public Sprite icone;
    [Range(0,20)] public float cookTime;
}
