using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Blader : MonoBehaviour
{
    public GameObject enemy;
    public GameObject damageCol;
    public GameObject explosion;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = new Ray(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), -Vector3.up);

        if (Physics.Raycast(ray, out hit))
        {
            var newHit = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
            if (dead)
            {
                enemy.SetActive(false);

                var deadExplosion = Instantiate(explosion, newHit, hit.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hit" || col.gameObject.tag == "Dash")
        {
            dead = true;
        }

        if (col.gameObject.tag == "Stomp")
        {
            dead = true;
            damageCol.SetActive(false);
        }
    }
}
