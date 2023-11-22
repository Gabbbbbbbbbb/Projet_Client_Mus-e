using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Menu_Manager : MonoBehaviour
{
    private static S_Menu_Manager instance;
    private S_Menu_Manager() { } //au cas o� certains fous tenteraient qd m�me d'utiliser le mot cl� "new"

    // M�thode d'acc�s statique (point d'acc�s global)
    public static S_Menu_Manager Instance
    {// ajout ET cr�ation du composant � un GameObject nomm� "SingletonHolder"
        get { return instance ?? (instance = new GameObject("MenuManager").AddComponent<S_Menu_Manager>()); }
        private set { instance = value; }
    }

    public S_PlayerController playerController;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance pr�c�dente (s�curit�...s�curit�...)

        instance = this;
        playerController = GameObject.FindAnyObjectByType<S_PlayerController>();
    }

    public void stopPlayer(bool b)
    {
        playerController.setIsNotInMenu(b);
    }
}
