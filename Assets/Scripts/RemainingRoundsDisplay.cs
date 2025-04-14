using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingRoundsDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<ExpeditionManager>().expeditionStarted)
        {
            GetComponent<TextMeshProUGUI>().text = "Expedition started. Rounds left to completion: " + FindObjectOfType<ExpeditionManager>().roundsLeft.ToString();
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
}
