//Creation : CM
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class S_ButtonOrder : MonoBehaviour
{
    public GameObject buttonContainer;
    private S_ButtonOrderSingleB[] buttonListComponent = new S_ButtonOrderSingleB[9];
    public TextMeshProUGUI stateText;

    [HideInInspector]
    public bool useGradient;
    [HideInInspector]
    public Gradient buttonGradient;

    [Header("Mini Game Parameters")]
    [SerializeField]
    int buttonNumberToPress;
    [SerializeField]
    float scorePerButtonMax = 100;

    [Header("Button Shrink Parameters")]
    [SerializeField]
    private float maxSize;
    [SerializeField]
    private float minSize, timeToPress;

    [Header("Button Colors")]
    [SerializeField]
    private ColorBlock ButtonColor = ColorBlock.defaultColorBlock;

    

    [Header("Score Text")]
    [SerializeField]
    private string scoreTextString;
    [SerializeField]
    private Color scoreColor = Color.green;

    float totalScore = 0;


    bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonContainer == null) { //Check if the button container is set up and if not tries to find or send an error
            Transform tp = transform.Find("Horizontal Layout");
            if (tp == null ) {
                Debug.LogError("Missing Button Container, the name of the gameobject that was in this variable is/was : Horizontal Layout");
            }
            else
            {
                buttonContainer = tp.gameObject;
            }
        }

        if (stateText == null)  //Check if the state text is set up and if not tries to find or send an error
        {
            Transform tp = transform.Find("State Text");
            if (tp == null)
            {
                Debug.LogError("Missing State Text, the name of the gameobject that was in this variable is/was : State Text");
            }
            else
            {
                stateText = tp.GetComponent<TextMeshProUGUI>();
            }
        }

        stateText.enabled = false; //Disable the state text

        getButton();

        ButtonPressed(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Setup all the buttons
    /// </summary>
    void getButton()
    {
        int index = 0;
        foreach (Transform btt in buttonContainer.transform)
        {
            foreach (Transform bt in btt.transform)
            {
                Transform b = bt.GetChild(0);
                if (b.GetComponent<S_ButtonOrderSingleB>() != null)
                {
                    buttonListComponent[index] = b.GetComponent<S_ButtonOrderSingleB>(); //Set up variables for the buttons
                    buttonListComponent[index].SetActivated(false);
                    buttonListComponent[index].setButtonManager(this);
                    buttonListComponent[index].SetColor(ButtonColor);
                    buttonListComponent[index].minSize = minSize;
                    buttonListComponent[index].maxSize = maxSize;
                    buttonListComponent[index].timeToPress = timeToPress;
                    buttonListComponent[index].setSize(0);
                    buttonListComponent[index].score = scorePerButtonMax;
                    buttonListComponent[index].useGradient = useGradient;
                    buttonListComponent[index].ButtonGradient = buttonGradient;
                    index++;
                }
            }
        }
    }

    /// <summary>
    /// Spawns a new button and reset the others
    /// </summary>
    void newButtonSelect()
    {
        int rdb = Random.Range(0, buttonListComponent.Length);
        
        foreach(S_ButtonOrderSingleB b in buttonListComponent)
        {
            b.SetActivated(false);
            b.setSize(0);
        }
        buttonListComponent[rdb].SetActivated(true);
        
    }

    /// <summary>
    /// Function to either spawn another button to press or finish the game if there in no more buttons to spawn
    /// </summary>
    /// <param name="score"> score to add to the total </param>
    public void ButtonPressed(float score)
    {
        totalScore += score;
        
        if (buttonNumberToPress > 0)
        {
            buttonNumberToPress--;
            newButtonSelect();
        }
        else
        {
            finishGame();
        }
    }

    /// <summary>
    /// Launch this function when the all the buttons are pressed
    /// </summary>
    public void finishGame()
    {
        buttonContainer.SetActive(false);
        stateText.enabled = true;
        stateText.text = scoreTextString + Mathf.Round(totalScore);
        stateText.color = scoreColor;
    }

    
}

#if UNITY_EDITOR
[CustomEditor(typeof(S_ButtonOrder))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        S_ButtonOrder script = (S_ButtonOrder)target;

        // draw checkbox for the bool
        script.useGradient = EditorGUILayout.Toggle("Use Gradient ?", script.useGradient);
        if (script.useGradient) // if bool is true, show other fields
        {
            script.buttonGradient = EditorGUILayout.GradientField("Button Gradient",script.buttonGradient);
        }
    }
}
#endif
