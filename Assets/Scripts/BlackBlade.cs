using System.Collections;
using System.Collections.Generic;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;

public class BlackBlade : MonoBehaviour
{
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        PlaySwing();
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySwing()
    {
        if (FindObjectOfType<PlayerController>().attackType == 1)
        {
            anim.Play("SwordSwingUp");
        }
        else if (FindObjectOfType<PlayerController>().attackType == 2)
        {
            anim.Play("SwordSwingRight");
        }
        else if (FindObjectOfType<PlayerController>().attackType == 3)
        {
            anim.Play("SwordSwingDown");
        }
        else if (FindObjectOfType<PlayerController>().attackType == 4)
        {
            anim.Play("SwordSwingLeft");
        }
    }
}
