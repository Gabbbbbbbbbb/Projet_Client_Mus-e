using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Exemple_Interactable_Obj : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";

    public override string GetDescription()
    {
        return description;
    }

    public override void Interact()
    {
        S_Player_Interaction.instance.OnInteraction();
        Destroy(gameObject);
        //Add cube to inventori
    }


}
