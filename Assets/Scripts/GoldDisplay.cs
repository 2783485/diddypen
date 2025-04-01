using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Gold:" + FindObjectOfType<GoldManager>().GetGold().ToString();
    }
}
