using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public GameObject trafficCollider;
    public GameObject trafficLightTemp;
    public GameObject trafficLight;

    public bool inRange = false;

    public GameObject lightRed;
    public GameObject lightGreen;
    public GameObject traffic1;
    public GameObject traffic2;
    public GameObject traffic3;
    public GameObject traffic4;
    public GameObject traffic5;
    public GameObject traffic6;

    Animator anim1;
    Animator anim2;
    Animator anim3;
    Animator anim4;
    Animator anim5;
    Animator anim6;

    public int index;

    public float tOff = 3;
    public float tOn = 0;
    public bool off = true;
    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        anim1 = traffic1.GetComponentInChildren<Animator>();
        anim2 = traffic2.GetComponentInChildren<Animator>();
        anim3 = traffic3.GetComponentInChildren<Animator>();
        anim4 = traffic4.GetComponentInChildren<Animator>();
        anim5 = traffic5.GetComponentInChildren<Animator>();
        anim6 = traffic6.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trafficCollider.GetComponent<TrafficChecker>().traffic && !inRange)
        {
            trafficLightTemp.SetActive(false);
            trafficLight.SetActive(true);
            inRange = true;
        }
        if (!trafficCollider.GetComponent<TrafficChecker>().traffic)
        {
            trafficLightTemp.SetActive(true);
            lightRed.SetActive(false);
            lightGreen.SetActive(false);
            inRange = false;

            traffic1.SetActive(false);
            traffic2.SetActive(false);
            traffic3.SetActive(false);
            traffic4.SetActive(false);
            traffic5.SetActive(false);
            traffic6.SetActive(false);
        }

        if(trafficCollider.GetComponent<TrafficChecker>().traffic)
        {
            if (off)
            {
                tOff -= Time.deltaTime;

                if (tOff <= 0)
                {
                    off = false;
                    on = true;

                    lightRed.SetActive(true);
                    lightGreen.SetActive(false);

                    traffic1.SetActive(false);
                    traffic2.SetActive(false);
                    traffic3.SetActive(false);
                    traffic4.SetActive(false);
                    traffic5.SetActive(false);
                    traffic6.SetActive(false);

                    index = Random.Range(1, 7);

                    if (index == 1)
                    {
                        traffic1.SetActive(true);
                        anim1.SetTrigger("Go");
                    }
                    if (index == 2)
                    {
                        traffic2.SetActive(true);
                        anim2.SetTrigger("Go");
                    }
                    if (index == 3)
                    {
                        traffic3.SetActive(true);
                        anim3.SetTrigger("Go");
                    }
                    if (index == 4)
                    {
                        traffic4.SetActive(true);
                        anim4.SetTrigger("Go");
                    }
                    if (index == 5)
                    {
                        traffic5.SetActive(true);
                        anim5.SetTrigger("Go");
                    }
                    if (index == 6)
                    {
                        traffic6.SetActive(true);
                        anim6.SetTrigger("Go");
                    }

                    tOn = 2.3f;
                }
            }
            if (on)
            {
                tOn -= Time.deltaTime;

                if (tOn <= 0)
                {
                    off = true;
                    on = false;

                    lightRed.SetActive(false);
                    lightGreen.SetActive(true);

                    tOff = 3f;
                }
            }
        }
    }
}
