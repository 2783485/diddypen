using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {  
        FindObjectOfType<UIManager>().HideExpeditionPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
