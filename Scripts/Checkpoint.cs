using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool active;
    public GameObject spawnPoint;
    public GameObject poofEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            var effect = Instantiate(poofEffect, transform.position, transform.rotation);

            spawnPoint.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
            active = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hit")
        {
            active = true;
        }
    }
}
