using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcanaDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Arcana: " + FindObjectOfType<PlayerController>().GetArcana().ToString();
    }
}
