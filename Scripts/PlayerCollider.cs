using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PSX;

public class PlayerCollider : MonoBehaviour
{
    Animator anim;
    public Camera cam;
    public float defaultFOV = 60;
    public float endFOV = 70;
    public float tFOV = 0;

    public GameObject pauseScreen;
    public float deathTimer = 0;
    public bool dead = false;
    public bool starting = true;

    public GameObject ringEffect;
    public GameObject ballEffect;
    public GameObject heartEffect;
    public GameObject gem1Effect;
    public GameObject damageEffect;
    public GameObject spawnPoint;
    public GameObject bottom;
    public GameObject[] spybots;

    public float t = 0;
    public float t2 = 0;
    public bool area2D;
    public bool gotIt = false;
    public bool damaged = false;
    public bool levelEnd = false;
    public float levelEndTimer = 0;
    public float levelEndTimer2 = 0;

    public GameObject healthUI;
    public GameObject ballUI;
    public GameObject gemUI;
    public GameObject spyBotUI;
    public GameObject ringUI;

    public float tLerpHealth;
    public bool healthGot = false;
    public bool healthGot2 = false;
    public bool healthGotEnd = false;
    public float tHealth = 0;

    public float tLerpBall;
    public bool ballGot = false;
    public bool ballGot2 = false;
    public bool ballGotEnd = false;
    public float tBall = 0;

    public float tLerpGem;
    public bool gemGot = false;
    public bool gemGot2 = false;
    public bool gemGotEnd = false;
    public float tGem = 0;

    public float tLerpSpy;
    public bool spyGot = false;
    public bool spyGot2 = false;
    public bool spyGotEnd = false;
    public float tSpy = 0;

    public float tLerpRing;
    public bool ringGot = false;
    public bool ringGot2 = false;
    public bool ringGotEnd = false;
    public float tRing = 0;

    public TMP_Text healthText;
    public TMP_Text ballsText;
    public TMP_Text ringsText;
    public TMP_Text ringsTextSlash;
    public TMP_Text totalRingsText;
    public TMP_Text botsText;
    public TMP_Text botsTextSlash;
    public TMP_Text totalBotsText;

    Color32 invisibleColorHealth;
    Color32 visibleColorHealth;

    Color32 invisibleColorBall;
    Color32 visibleColorBall;

    Color32 invisibleColorBot;
    Color32 visibleColorBot;

    Color32 invisibleColorRing;
    Color32 visibleColorRing;

    public int balls = 0;
    public int ballsCheck = 0;
    public int rings = 0;

    public int health;
    public int healthCheck;

    public bool falling = false;
    public bool falling2 = false;
    public bool healthReduce = false;
    public float tFade;
    public float t2Fade;

    public float tLerpFade;
    public float tLerpColorFade;

    public GameObject worldController;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        Time.timeScale = 1f;

        anim = GetComponentInChildren<Animator>();

        health = PlayerPrefs.GetInt("health");

        spybots = GameObject.FindGameObjectsWithTag("Spybot");

        healthText = healthText.GetComponent<TextMeshProUGUI>();
        ballsText = ballsText.GetComponent<TextMeshProUGUI>();
        ringsText = ringsText.GetComponent<TextMeshProUGUI>();
        totalRingsText = totalRingsText.GetComponent<TextMeshProUGUI>();
        botsText = botsText.GetComponent<TextMeshProUGUI>();
        totalBotsText = totalBotsText.GetComponent<TextMeshProUGUI>();

        invisibleColorHealth = new Color32(54, 235, 255, 0);
        visibleColorHealth = new Color32(54, 235, 255, 255);
        healthText.color = invisibleColorHealth;

        invisibleColorBall = new Color32(254, 255, 0, 0);
        visibleColorBall = new Color32(254, 255, 0, 255);
        ballsText.color = invisibleColorBall;

        invisibleColorBot = new Color32(178, 178, 178, 0);
        visibleColorBot = new Color32(178, 178, 178, 255);
        botsText.color = invisibleColorBot;
        botsTextSlash.color = invisibleColorBot;
        totalBotsText.color = invisibleColorBot;

        invisibleColorRing = new Color32(255, 139, 0, 0);
        visibleColorRing = new Color32(255, 139, 0, 255);
        ringsText.color = invisibleColorRing;
        ringsTextSlash.color = invisibleColorRing;
        totalRingsText.color = invisibleColorRing;

        ballGot = true;
        tBall = 0;

        gemGot = true;
        tGem = 0;

        ringGot = true;
        tRing = 0;

        healthGot = true;
        tHealth = 0;

        spyGot = true;
        tSpy = 0;

        worldController.GetComponent<FogController>().fogDistance = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        healthCheck = PlayerPrefs.GetInt("health");

        healthText.text = PlayerPrefs.GetInt("health").ToString();
        ballsText.text = balls.ToString();
        ringsText.text = rings.ToString();
        totalRingsText.text = PlayerPrefs.GetInt("ringsTotalCount").ToString();
        botsText.text = PlayerPrefs.GetInt("spyBotCount").ToString();
        totalBotsText.text = PlayerPrefs.GetInt("spyBotTotalCount").ToString();

        //Fade in when starting a level
        if(starting)
        {
            t2Fade += Time.deltaTime;

            worldController.GetComponent<FogController>().fogDistance = Mathf.Lerp(20, 0, tLerpFade);
            healthText.color = Color32.Lerp(invisibleColorHealth, visibleColorHealth, tLerpColorFade);
            ballsText.color = Color32.Lerp(invisibleColorBall, visibleColorBall, tLerpColorFade);
            botsText.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
            botsTextSlash.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
            totalBotsText.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
            ringsText.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);
            ringsTextSlash.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);
            totalRingsText.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);

            tLerpFade += 2.5f * Time.deltaTime;
            tLerpColorFade += 2f * Time.deltaTime;

            if (t2Fade >= 1f)
            {
                t2Fade = 0;
                tLerpFade = 0;
                tLerpColorFade = 0;

                starting = false;
            }
        }

        // + 125 in Mathf.Lerp are the y coordinates of the 3D UI parent object camera
        // + 480 in Mathf.Lerp is the height of the canvas
        //For health UI
        if (healthGot)
        {
            healthUI.transform.position = new Vector3(healthUI.transform.position.x, Mathf.Lerp(25.59f, 24.41f, tLerpHealth) + 125, healthUI.transform.position.z);
            healthText.transform.position = new Vector3(healthText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpHealth) + 768, healthText.transform.position.z);

            tLerpHealth += 1.5f * Time.deltaTime;

            if (tLerpHealth > 1.0f)
            {
                healthGot2 = true;
            }
        }
        if (healthGot2)
        {
            tHealth += Time.deltaTime;

            if (tHealth >= 2f)
            {
                tHealth = 0;
                tLerpHealth = 0.0f;
                healthGot = false;
                healthGot2 = false;
                healthGotEnd = true;
            }
        }
        if (healthGotEnd)
        {
            healthUI.transform.position = new Vector3(healthUI.transform.position.x, Mathf.Lerp(24.41f, 25.59f, tLerpHealth) + 125, healthUI.transform.position.z);
            healthText.transform.position = new Vector3(healthText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpHealth) + 768, healthText.transform.position.z);

            tLerpHealth += Time.deltaTime;

            if (tLerpHealth > 1.0f)
            {
                tLerpHealth = 0.0f;
                healthGotEnd = false;
            }
        }

        //For balls UI
        if (ballGot)
        {
            ballUI.transform.position = new Vector3(ballUI.transform.position.x, Mathf.Lerp(23.73f, 22.9f, tLerpBall) + 125f, ballUI.transform.position.z);
            ballsText.transform.position = new Vector3(ballsText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpBall) + 768, ballsText.transform.position.z);

            tLerpBall += 1.5f * Time.deltaTime;

            if (tLerpBall > 1.0f)
            {
                ballGot2 = true;
            }
        }
        if (ballGot2)
        {
            tBall += Time.deltaTime;

            if (tBall >= 2f)
            {
                tBall = 0;
                tLerpBall = 0.0f;
                ballGot = false;
                ballGot2 = false;
                ballGotEnd = true;
            }
        }
        if (ballGotEnd)
        {
            ballUI.transform.position = new Vector3(ballUI.transform.position.x, Mathf.Lerp(22.9f, 23.73f, tLerpBall) + 125f, ballUI.transform.position.z);
            ballsText.transform.position = new Vector3(ballsText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpBall) + 768, ballsText.transform.position.z);

            tLerpBall += Time.deltaTime;

            if (tLerpBall > 1.0f)
            {
                tLerpBall = 0.0f;
                ballGotEnd = false;
            }
        }

        //For gem UI
        if (gemGot)
        {
            gemUI.transform.position = new Vector3(gemUI.transform.position.x, Mathf.Lerp(24.16f, 22.91f, tLerpGem) + 125, gemUI.transform.position.z);
            tLerpGem += 1.5f * Time.deltaTime;

            if (tLerpGem > 1.0f)
            {
                gemGot2 = true;
            }
        }
        if (gemGot2)
        {
            tGem += Time.deltaTime;

            if (tGem >= 2f)
            {
                tGem = 0;
                tLerpGem = 0.0f;
                gemGot = false;
                gemGot2 = false;
                gemGotEnd = true;
            }
        }
        if (gemGotEnd)
        {
            gemUI.transform.position = new Vector3(gemUI.transform.position.x, Mathf.Lerp(22.91f, 24.16f, tLerpGem) + 125, gemUI.transform.position.z);
            tLerpGem += Time.deltaTime;

            if (tLerpGem > 1.0f)
            {
                tLerpGem = 0.0f;
                gemGotEnd = false;
            }
        }

        //For ring UI
        if (ringGot)
        {
            ringUI.transform.position = new Vector3(ringUI.transform.position.x, Mathf.Lerp(25.55f, 24.41f, tLerpRing) + 125, ringUI.transform.position.z);
            ringsText.transform.position = new Vector3(ringsText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpRing) + 768, ringsText.transform.position.z);
            ringsTextSlash.transform.position = new Vector3(ringsTextSlash.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpRing) + 768, ringsTextSlash.transform.position.z);
            totalRingsText.transform.position = new Vector3(totalRingsText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpRing) + 768, totalRingsText.transform.position.z);

            tLerpRing += 1.5f * Time.deltaTime;

            if (tLerpRing > 1.0f)
            {
                ringGot2 = true;
            }
        }
        if (ringGot2)
        {
            tRing += Time.deltaTime;

            if (tRing >= 2f)
            {
                tRing = 0;
                tLerpRing = 0.0f;
                ringGot = false;
                ringGot2 = false;
                ringGotEnd = true;
            }
        }
        if (ringGotEnd)
        {
            ringUI.transform.position = new Vector3(ringUI.transform.position.x, Mathf.Lerp(24.41f, 25.55f, tLerpRing) + 125, ringUI.transform.position.z);
            ringsText.transform.position = new Vector3(ringsText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpRing) + 768, ringsText.transform.position.z);
            ringsTextSlash.transform.position = new Vector3(ringsTextSlash.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpRing) + 768, ringsTextSlash.transform.position.z);
            totalRingsText.transform.position = new Vector3(totalRingsText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpRing) + 768, totalRingsText.transform.position.z);

            tLerpRing += Time.deltaTime;

            if (tLerpRing > 1.0f)
            {
                tLerpRing = 0.0f;
                ringGotEnd = false;
            }
        }

        //Finds all existing Spybots in the scene and checks whether any of them dies
        foreach (GameObject spybot in spybots)
        {
            if (spybot.GetComponent<E_Spy>().deadSpybot)
            {
                spyGot = true;
                tSpy = 0;

                spybot.GetComponent<E_Spy>().deadSpybot = false;
            }
        }

        //For Spybot UI
        if (spyGot)
        {
            spyBotUI.transform.position = new Vector3(spyBotUI.transform.position.x, Mathf.Lerp(25.5f, 24.25f, tLerpSpy) + 125, spyBotUI.transform.position.z);
            botsText.transform.position = new Vector3(botsText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpSpy) + 768, botsText.transform.position.z);
            botsTextSlash.transform.position = new Vector3(botsTextSlash.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpSpy) + 768, botsTextSlash.transform.position.z);
            totalBotsText.transform.position = new Vector3(totalBotsText.transform.position.x, Mathf.Lerp(6.8f, -29f, tLerpSpy) + 768, totalBotsText.transform.position.z);

            tLerpSpy += 1.5f * Time.deltaTime;

            if (tLerpSpy > 1.0f)
            {
                spyGot2 = true;
            }
        }
        if (spyGot2)
        {
            tSpy += Time.deltaTime;

            if (tSpy >= 2f)
            {
                tSpy = 0;
                tLerpSpy = 0.0f;
                spyGot = false;
                spyGot2 = false;
                spyGotEnd = true;
            }
        }
        if (spyGotEnd)
        {
            spyBotUI.transform.position = new Vector3(spyBotUI.transform.position.x, Mathf.Lerp(24.25f, 25.5f, tLerpSpy) + 125, spyBotUI.transform.position.z);
            botsText.transform.position = new Vector3(botsText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpSpy) + 768, botsText.transform.position.z);
            botsTextSlash.transform.position = new Vector3(botsTextSlash.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpSpy) + 768, botsTextSlash.transform.position.z);
            totalBotsText.transform.position = new Vector3(totalBotsText.transform.position.x, Mathf.Lerp(-29f, 6.8f, tLerpSpy) + 768, totalBotsText.transform.position.z);

            tLerpSpy += Time.deltaTime;

            if (tLerpSpy > 1.0f)
            {
                tLerpSpy = 0.0f;
                spyGotEnd = false;
            }
        }

        //To make sure that you don't get x2 the amount of items
        if (gotIt)
        {
            t += Time.deltaTime;

            if (t >= 0.05f)
            {
                t = 0;
                gotIt = false;
            }
        }

        //When damaged, immunity timer
        if (damaged)
        {
            t2 += Time.deltaTime;

            healthGot = true;
            tHealth = 0;

            if (t2 >= 1f)
            {
                t2 = 0;
                damaged = false;
            }
        }

        if (ballsCheck >= 25)
        {
            ballsCheck = 0;
            health += 1;

            healthGot = true;
            tHealth = 0;

            heartEffect.GetComponent<ParticleSystem>().Play();
        }

        //If dead by not falling
        if (healthCheck <= 0 && !falling2)
        {
            dead = true;
            PlayerPrefs.SetInt("health", 0);
            anim.SetInteger("state", 7);
            player.GetComponent<Movement>().enabled = false;

            healthGot = true;
            tHealth = 0;

            deathTimer += Time.deltaTime;

            if (deathTimer >= 3f)
            {
                tFade += Time.deltaTime;

                worldController.GetComponent<FogController>().fogDistance = Mathf.Lerp(0, 30, tLerpFade);
                healthText.color = Color32.Lerp(visibleColorHealth, invisibleColorHealth, tLerpColorFade);
                ballsText.color = Color32.Lerp(visibleColorBall, invisibleColorBall, tLerpColorFade);
                botsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                botsTextSlash.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                totalBotsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                ringsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                ringsTextSlash.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                totalRingsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);

                tLerpFade += 1.1f * Time.deltaTime;
                tLerpColorFade += 2f * Time.deltaTime;

                if (tFade >= 1f)
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
        }

            //When falling
            if (falling)
            {
                tFade += Time.deltaTime;

                worldController.GetComponent<FogController>().fogDistance = Mathf.Lerp(0, 30, tLerpFade);
                healthText.color = Color32.Lerp(visibleColorHealth, invisibleColorHealth, tLerpColorFade);
                ballsText.color = Color32.Lerp(visibleColorBall, invisibleColorBall, tLerpColorFade);
                botsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                botsTextSlash.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                totalBotsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                ringsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                ringsTextSlash.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                totalRingsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);

                tLerpFade += 1.1f * Time.deltaTime;
                tLerpColorFade += 2f * Time.deltaTime;

                if (tFade >= 1f)
                {
                    health -= 1;

                    healthGot = true;
                    tHealth = 0;

                    tFade = 0;
                    tLerpFade = 0;
                    tLerpColorFade = 0;
                    player.transform.position = spawnPoint.transform.position;

                    falling = false;
                    falling2 = true;
                }
            }
            if (falling2)
            {
                t2Fade += Time.deltaTime;

                worldController.GetComponent<FogController>().fogDistance = Mathf.Lerp(20, 0, tLerpFade);
                healthText.color = Color32.Lerp(invisibleColorHealth, visibleColorHealth, tLerpColorFade);
                ballsText.color = Color32.Lerp(invisibleColorBall, visibleColorBall, tLerpColorFade);
                botsText.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
                botsTextSlash.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
                totalBotsText.color = Color32.Lerp(invisibleColorBot, visibleColorBot, tLerpColorFade);
                ringsText.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);
                ringsTextSlash.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);
                totalRingsText.color = Color32.Lerp(invisibleColorRing, visibleColorRing, tLerpColorFade);

                tLerpFade += 2.5f * Time.deltaTime;
                tLerpColorFade += 2f * Time.deltaTime;

                //If dead by falling
                if (healthCheck <= 0)
                {
                    dead = true;
                    PlayerPrefs.SetInt("health", 0);

                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }

                if (t2Fade >= 1f)
                {
                    t2Fade = 0;
                    tLerpFade = 0;
                    tLerpColorFade = 0;

                    falling2 = false;
                }
            }

            //Level end
            if (levelEnd)
            {
                player.GetComponent<Movement>().levelEnded = true;
                levelEndTimer += Time.deltaTime;

                ballGot = true;
                tBall = 0;

                gemGot = true;
                tGem = 0;

                ringGot = true;
                tRing = 0;

                healthGot = true;
                tHealth = 0;

                spyGot = true;
                tSpy = 0;

                cam.fieldOfView = Mathf.Lerp(defaultFOV, endFOV, tFOV);
                tFOV += 3.5f * 2 * Time.deltaTime;

                if (levelEndTimer >= 0.1f)
                {
                    player.GetComponent<Movement>().enabled = false;
                    levelEndTimer2 += Time.deltaTime;

                    if (levelEndTimer2 >= 2f)
                    {
                        tFade += Time.deltaTime;

                        worldController.GetComponent<FogController>().fogDistance = Mathf.Lerp(0, 30, tLerpFade);
                        healthText.color = Color32.Lerp(visibleColorHealth, invisibleColorHealth, tLerpColorFade);
                        ballsText.color = Color32.Lerp(visibleColorBall, invisibleColorBall, tLerpColorFade);
                        botsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                        botsTextSlash.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                        totalBotsText.color = Color32.Lerp(visibleColorBot, invisibleColorBot, tLerpColorFade);
                        ringsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                        ringsTextSlash.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);
                        totalRingsText.color = Color32.Lerp(visibleColorRing, invisibleColorRing, tLerpColorFade);

                        tLerpFade += 1.1f * Time.deltaTime;
                        tLerpColorFade += 2f * Time.deltaTime;

                        if (tFade >= 1f)
                        {
                            Scene scene = SceneManager.GetActiveScene();
                            SceneManager.LoadScene(scene.name);
                        }
                    }
                }
            }

            //Check stats
            if (Input.GetKey(KeyCode.Q) && !dead)
            {
                ballGot = true;
                tBall = 0;

                gemGot = true;
                tGem = 0;

                ringGot = true;
                tRing = 0;

                healthGot = true;
                tHealth = 0;

                spyGot = true;
                tSpy = 0;
            }

            //Pause menu
            if (player.GetComponent<Movement>().paused)
            {
                pauseScreen.SetActive(true);

                ballGot = true;
                tBall = 0;

                gemGot = true;
                tGem = 0;

                ringGot = true;
                tRing = 0;

                healthGot = true;
                tHealth = 0;

                spyGot = true;
                tSpy = 0;
            }
            if (!player.GetComponent<Movement>().paused)
            {
                pauseScreen.SetActive(false);
            }

            //Set health value
            if(!dead)
            {
                PlayerPrefs.SetInt("health", health);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                Destroy(other.gameObject);

                if (!gotIt)
                {
                    balls += 1;
                    ballsCheck += 1;
                    ballGot = true;
                    tBall = 0;

                    ballEffect.GetComponent<ParticleSystem>().Play();
                }
            }

            if (other.gameObject.tag == "Gem")
            {
                Destroy(other.gameObject);

                gemUI.SetActive(true);

                if (!gotIt)
                {
                    gemGot = true;
                    tGem = 0;

                    gem1Effect.GetComponent<ParticleSystem>().Play();

                    gotIt = true;
                }

            }

            if (other.gameObject.tag == "Ring")
            {
                Destroy(other.gameObject);

                if (!gotIt)
                {
                    rings += 1;

                    ringGot = true;
                    tRing = 0;

                    ringEffect.GetComponent<ParticleSystem>().Play();

                    gotIt = true;
                }
            }

            if (other.gameObject.tag == "RingEnd")
            {
                Destroy(other.gameObject);

                ringEffect.GetComponent<ParticleSystem>().Play();
                heartEffect.GetComponent<ParticleSystem>().Play();

                levelEnd = true;

                if (!gotIt)
                {
                    gotIt = true;
                }
            }

            if (other.gameObject.tag == "Heart")
            {
                Destroy(other.gameObject);

                if (!gotIt)
                {
                    health += 5;
                    healthGot = true;
                    tHealth = 0;

                    heartEffect.GetComponent<ParticleSystem>().Play();

                    gotIt = true;
                }
            }

            if (other.gameObject.tag == "Damage")
            {
                if (!damaged && !dead)
                {
                    health -= 1;
                    damageEffect.GetComponent<ParticleSystem>().Play();

                    damaged = true;
                }
            }

            if (other.gameObject.tag == "Bottom")
            {
                falling = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "2D")
            {
                area2D = true;
            }

            if (other.gameObject.tag == "Damage")
            {
                if (!damaged)
                {
                    health -= 1;
                    damageEffect.GetComponent<ParticleSystem>().Play();

                    damaged = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "2D")
            {
                area2D = false;
            }
        }


        //To resume the game
        public void Resume()
        {
            Time.timeScale = 1f;
            player.GetComponent<Movement>().paused = false;
        }

        //To restart the current level
        public void Restart()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        //To exit the game
        public void Quit()
        {
            Application.Quit();
        }
    }