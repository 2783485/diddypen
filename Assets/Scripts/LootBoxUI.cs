using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<UIManager>().HideLootboxPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
