using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Enemy>() == null)
        {
            Time.timeScale = 1f;
        }
    }
    public void TimeSetZero()
    {
        Time.timeScale = 0f;
    }
    public void TimeSetOne()
    {
        Time.timeScale = 1f;
    }
}
