using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepParalisisDemonScript : MonoBehaviour
{
    public GameObject demon;
    


    [Header("Extras")]
    public PlayerScript playerScript;

    bool active;
    public int demonDangerLevel;

    [Header("Nights")]
    [SerializeField]
    int night1DL;
    [SerializeField]
    int night2DL, night3DL, night4DL, night5DL, night6DL, night7DL, night8DL;

    [Header("Time")]

    [SerializeField]
    float time;

    public float randomTime;
    public float fullPercentage;
    public float randomValuePercentage;
    public float randomValue;
    public float percentage;

    //public bool attack;

    bool doOnce;

    public bool EyesClosed = false;

    float timer;

    bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        //attack = false;
        demon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
        }

        switch (playerScript.night)
        {
            case 2:
                {
                    demonDangerLevel = night2DL;
                    break;
                }
            case 3:
                {
                    demonDangerLevel = night3DL;
                    break;
                }
            case 4:
                {
                    demonDangerLevel = night4DL;
                    break;
                }
            case 5:
                {
                    demonDangerLevel = night5DL;
                    break;
                }
            case 6:
                {
                    demonDangerLevel = night6DL;
                    break;
                }
            case 7:
                {
                    demonDangerLevel = night7DL;
                    break;
                }
            case 8:
                {
                    demonDangerLevel = night8DL;
                    break;
                }
            default: // 1
                {
                    demonDangerLevel = night1DL;
                    break;
                }
        }

        if (timer >= 10)
        {
            fullPercentage = percentage * randomValuePercentage;

            randomValuePercentage = (12f + demonDangerLevel) / 10;

            time += Time.deltaTime;

            if (time >= randomTime)
            {
                randomValue = Random.value * 100;
                time = 0;
            }

            percentage = 27 + demonDangerLevel;

            if (doOnce)
            {
                demonDangerLevel += 1; doOnce = false;
            }

            if (demonDangerLevel != 0)
            {
                if (0.19 * playerScript.maxSleep <
                playerScript.sleep && playerScript.sleep
                < 0.2 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (0.39 * playerScript.maxSleep <
                playerScript.sleep && playerScript.sleep
                < 0.4 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (0.59 * playerScript.maxSleep <
                playerScript.sleep && playerScript.sleep
                < 0.6 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (randomValue <= percentage)
                {
                    if (EyesClosed)
                    {
                        demon.SetActive(true);
                        playerScript.canMove = false;
                        playerScript.canTurn = false;
                        playerScript.canFlash = false;
                    }
                }

                if (randomValue >= fullPercentage)
                {
                    if (EyesClosed)
                    {
                        playerScript.canMove = true;
                        playerScript.canTurn = true;
                        playerScript.canFlash = true;
                        demon.SetActive(false);
                    }
                }

                if (playerScript.eyesClosedUp >= -1 ||
                playerScript.i == 2) { EyesClosed = true; }
                else if (playerScript.eyesClosedUp <= -1 ||
                playerScript.i == 1) { EyesClosed = false; }

                /*
                if (attack)
                {
                    demon.SetActive(true);
                    playerScript.canMove = false;
                    playerScript.canTurn = false;
                    playerScript.canFlash = false;
                }
                else
                {
                    playerScript.canMove = true;
                    playerScript.canTurn = true;
                    playerScript.canFlash = true;
                    demon.SetActive(false);
                }*/
            }
        }
    }
}

