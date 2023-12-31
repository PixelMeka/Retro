using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Clockbotron : MonoBehaviour
{
    Animator anim;
    public bool range = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(range == true)
        {
            anim.SetInteger("state", 1);
        }
        if(range == false)
        {
            anim.SetInteger("state", 0);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            range = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            range = false;
        }
    }
}
