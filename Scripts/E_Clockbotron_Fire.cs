using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Clockbotron_Fire : MonoBehaviour
{
    public GameObject shootP;
    public GameObject fireP;
    public GameObject damageCol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootStart()
    {
        fireP.GetComponent<ParticleSystem>().Play();
        shootP.GetComponent<ParticleSystem>().Play();

        damageCol.SetActive(true);
    }

    void ShootEnd()
    {
        fireP.GetComponent<ParticleSystem>().Stop();
        shootP.GetComponent<ParticleSystem>().Stop();

        damageCol.SetActive(false);
    }
}
