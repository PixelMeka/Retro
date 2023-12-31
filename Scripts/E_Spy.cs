using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spy : MonoBehaviour
{
    GameObject player;
    public GameObject enemy;
    public GameObject explosion;
    public bool dead = false;
    public bool deadSpybot = false;

    public int bot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 place = new Vector3(player.transform.position.x - this.transform.position.x, 0f, player.transform.position.z - this.transform.position.z); //The place to look at, y is neglected.
        Quaternion rot = Quaternion.LookRotation(place); //Tells the npc to literally look at that place.
        this.transform.rotation = rot; //Rotate the gameobject to that place.

        bot = PlayerPrefs.GetInt("spyBotCount");

        if (dead)
        {
            var deadExplosion = Instantiate(explosion, transform.position, transform.rotation);

            bot += 1;
            PlayerPrefs.SetInt("spyBotCount", bot);

            enemy.SetActive(false);
            deadSpybot = true;
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
