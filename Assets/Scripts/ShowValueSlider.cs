using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValueSlider : MonoBehaviour
{
    Text pourcentageText;
    // Start is called before the first frame update
    void Start()
    {
        pourcentageText = GetComponent<Text>();
    }

    public void TextUpdate(float value)
    {
        pourcentageText.text = Mathf.RoundToInt((value/256) * 100) + "%";
    }
    
}
