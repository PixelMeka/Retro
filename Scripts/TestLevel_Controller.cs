using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel_Controller : MonoBehaviour
{
    public bool active = false;
    public GameObject poofEffect;
    public GameObject pinkObjects;

    public int totalBots = 3;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, true);

        PlayerPrefs.SetInt("ringsTotalCount", 14);
        PlayerPrefs.SetInt("spyBotTotalCount", totalBots);
        PlayerPrefs.SetInt("spyBotCount", 0);
        PlayerPrefs.SetInt("health", 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            pinkObjects.SetActive(true);

            var effect = Instantiate(poofEffect, transform.position, transform.rotation);

            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Stomp")
        {
            active = true;
        }
    }
}
