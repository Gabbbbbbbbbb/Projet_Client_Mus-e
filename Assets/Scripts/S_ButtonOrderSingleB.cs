//Creation : CM
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_ButtonOrderSingleB : MonoBehaviour
{
    bool activated = false;
    Button selfButton;
    S_ButtonOrder buttonManager = null;
    [HideInInspector]
    public float maxSize, minSize, timeToPress;
    [HideInInspector]
    public float score;

    RectTransform rectTransform;

    [HideInInspector]
    public bool useGradient = false;
    [HideInInspector]
    public Gradient ButtonGradient;

    float currentSize;
    // Start is called before the first frame update
    void Awake()
    {
        selfButton = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (currentSize == minSize) //If the button reached his minimum size then this button is lost
            {
                SetActivated(false);
                setSize(0);

                buttonManager.ButtonPressed(-score / 2); //Remove points for losing one button and deactivates the button
            }
            else
            {
                currentSize = Mathf.Clamp(currentSize - (Time.deltaTime * ((maxSize - minSize) / timeToPress)), minSize, maxSize); //Change the size of the button
                rectTransform.sizeDelta = new Vector2(currentSize, currentSize);
                if (useGradient)
                {
                    ColorBlock tempCB = selfButton.colors;
                    tempCB.normalColor = ButtonGradient.Evaluate((currentSize - minSize) / (maxSize - minSize));
                    selfButton.colors = tempCB;
                }
            }
        }
    }
    /// <summary>
    /// Set the button order manager
    /// </summary>
    /// <param name="bm">The main script managing all the buttons</param>
    public void setButtonManager(S_ButtonOrder bm)
    {
        buttonManager = bm;
    }
    /// <summary>
    /// Change the colors of the button
    /// </summary>
    /// <param name="cb"> the desired colors of the button</param>
    public void SetColor(ColorBlock cb)
    {
        selfButton.colors = cb;
    }

    /// <summary>
    /// Activate the button 
    /// </summary>
    /// <param name="a">state of the button</param>
    public void SetActivated(bool a)
    {
        activated = a;
        if (activated)
        {
            currentSize = maxSize;
        }
    }

    /// <summary>
    /// Change the size of the button from an external object
    /// </summary>
    /// <param name="s"> desired size </param>
    public void setSize(float s)
    {
        currentSize = s;
        rectTransform.sizeDelta = new Vector2(currentSize, currentSize);
    }

    /// <summary>
    /// Function called when the button is pressed
    /// </summary>
    public void Pressed() 
    {
        if (activated)
        {
            float s = score * ((currentSize-minSize) / (maxSize-minSize));
            buttonManager.ButtonPressed(s);
        }
        
    }


}
