using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Clockbotron_HitCollider : MonoBehaviour
{
    public GameObject enemy;
    public GameObject explosion;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            enemy.SetActive(false);

            var deadExplosion = Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hit" || col.gameObject.tag == "Stomp" || col.gameObject.tag == "Dash")
        {
            dead = true;
        }
    }
}
