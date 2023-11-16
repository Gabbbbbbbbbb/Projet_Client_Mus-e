using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class S_Player_Interaction : MonoBehaviour
{
   public static S_Player_Interaction instance;
    public TMPro.TextMeshProUGUI interactionText;
    public bool hitObject = false;
    S_Interactable interactable;

    private void Awake()
    {
        if(!instance) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (interactable)
            {
                HandleInteraction();
            }
        }

    }

    
   public void OnTriggerEnter(Collider collider)
    {
        interactable = (S_Interactable)collider.GetComponent(typeof(S_Interactable));

        if (interactable != null)
        {
            interactionText.text = interactable.GetDescription();
            interactionText.gameObject.SetActive(true);

           

        }
        if (interactable == null)
        {
            
            interactionText.gameObject.SetActive(false);

        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        OnInteraction();
    }

    public void OnInteraction()
    {
        interactable = null;
        
        interactionText.gameObject.SetActive(false);
    }


    void HandleInteraction()
    {
        switch (interactable.interactiontype)
        {
            case S_Interactable.InteractionType.Click:
                interactable.Interact();
                break;
        }
    }
}


