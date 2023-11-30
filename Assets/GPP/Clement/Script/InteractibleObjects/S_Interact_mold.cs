using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Interact_mold : S_Interactable
{
    [Header("Display Text")]
    public string description = "Press <color=red>RIGHT CLICK</color>";
    public string cannotUseMatText = "<color=red>CANNOT PUT 2 SAME MATERIALS</color>";
    public string inventoryEmpty = "<color=red>CANNOT USE THE MOLD IF NO MATERIAL IN YOUR INVENTORY</color>";
    [Header("Inventory")]
    private Image reducedInventory;
    public GameObject mainInventoryGroup;

    private S_Mold_Inventory moldInventory;

    public S_Materials mold;

    private void Start()
    {
        reducedInventory = S_UI_Inventory.instance.GetComponent<Image>();
        moldInventory = GetComponent<S_Mold_Inventory>();
    }

    public override string GetDescription()
    {
        if (S_Inventory.instance.GetMaterials() != null)
        {
            if (S_Inventory.instance.GetMaterials() != moldInventory.GetMaterial1())
            {
                return description;
            }
            else
            {
                return cannotUseMatText;
            }
        }
        else if(!moldInventory.IsInventoryFull())
        {
                return inventoryEmpty;
        }
        else if (moldInventory.IsFirstSlotFull())
        {
            return description;
        }
        else { return description; }
           
    }

    public override string GetMatDescription()
    {
        if(S_Inventory.instance.GetMaterials() != null && S_Inventory.instance.GetMaterials() != moldInventory.GetMaterial1())
        {
            return mold.description;

        }
        else
        {
            return null;
        }
    }

    public override void Interact()
    {
        
        if (S_Inventory.instance.GetMaterials() != moldInventory.GetMaterial1())
        {

                moldInventory.refreshMoldInv();

                if(S_Inventory.instance.GetMaterials() != null)
                if (!moldInventory.IsInventoryFull() && S_Inventory.instance.GetMaterials().canBeBaked)
                {

                    moldInventory.AddToInventory(S_Inventory.instance.GetMaterials());
                    S_Inventory.instance.ClearInventory();
                    S_UI_Inventory.instance.ClearPlayerInventoryIcon();
                    
                }

                // Quand le ui sera terminer on fera comme ca au lieu du boutton bake
                if (moldInventory.IsInventoryFull())
                {
                    moldInventory.CheckRecipeMaterialWithMoldMaterial();
                    
                 }
               
                //isInInventory = true;
            //}
            //else
            //{
               // S_Menu_Manager.Instance.stopPlayer(true);
                //mainInventoryGroup.gameObject.SetActive(false);
                //reducedInventory.gameObject.SetActive(true);
                //S_UI_Inventory.instance.ClearMoldInventoryStatueIcon();
                //isInInventory = false;
                //BakeButton.onClick.RemoveAllListeners();

            //}
        }
       
        

        

    }

    
}
