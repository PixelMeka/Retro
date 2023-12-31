using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Shocker : MonoBehaviour
{
    public GameObject shock;
    public GameObject spark;
    public GameObject enemy;
    public GameObject damageCol;
    public GameObject explosion;
    public bool dead = false;
    public bool start = true;

    public float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        t = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            enemy.SetActive(false);

            var deadExplosion = Instantiate(explosion, transform.position, transform.rotation);
        }

        if (start)
        {
            t += Time.deltaTime;

            if (t >= 3)
            {
                t = 3;
                start = false;
                shock.GetComponent<ParticleSystem>().Play();
                spark.GetComponent<ParticleSystem>().Play();
                damageCol.SetActive(true);
            }
        }
        

        if (!start)
        {
            t -= Time.deltaTime;

            if (t <= 0)
            {
                t = 0;
                start = true;
                shock.GetComponent<ParticleSystem>().Stop();
                spark.GetComponent<ParticleSystem>().Stop();
                damageCol.SetActive(false);
            }
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
