using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Click,
        Hold
    }

    public InteractionType interactiontype;
    public abstract string GetDescription();
    public abstract void Interact();

    
}
