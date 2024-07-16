using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothManScript : MonoBehaviour
{
    public GameObject moth;

    [Header("Extras")]
    public PlayerScript playerScript;

    bool active;
    public int mothManDangerLevel;

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

    public bool attack;

    bool doOnce;

    public bool EyesClosed;

    bool start;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        moth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
        }

        if(timer > 10)
        {

            fullPercentage = percentage * randomValuePercentage;
            randomValuePercentage = (12f + mothManDangerLevel) / 10;

            time += Time.deltaTime;
            if (time >= randomTime)
            {
                randomValue = Random.value * 100;
                time = 0;
            }

            percentage = 40 + mothManDangerLevel;

            switch (playerScript.night)
            {
                case 2:
                    {
                        mothManDangerLevel = night2DL;
                        break;
                    }
                case 3:
                    {
                        mothManDangerLevel = night3DL;
                        break;
                    }
                case 4:
                    {
                        mothManDangerLevel = night4DL;
                        break;
                    }
                case 5:
                    {
                        mothManDangerLevel = night5DL;
                        break;
                    }
                case 6:
                    {
                        mothManDangerLevel = night6DL;
                        break;
                    }
                case 7:
                    {
                        mothManDangerLevel = night7DL;
                        break;
                    }
                case 8:
                    {
                        mothManDangerLevel = night8DL;
                        break;
                    }
                default: // 1
                    {
                        mothManDangerLevel = night1DL;
                        break;
                    }
            }
            if (doOnce)
            {
                mothManDangerLevel += 1; doOnce = false;
            }

            if (mothManDangerLevel != 0)
            {
                if (0.19 * playerScript.maxSleep < playerScript.sleep && playerScript.sleep < 0.2 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (0.39 * playerScript.maxSleep < playerScript.sleep && playerScript.sleep < 0.4 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (0.59 * playerScript.maxSleep < playerScript.sleep && playerScript.sleep < 0.6 * playerScript.maxSleep)
                {
                    doOnce = true;
                }

                if (randomValue <= percentage)
                {
                    if (EyesClosed)
                    {
                        attack = true;
                    }
                }

                if (randomValue >= fullPercentage)
                {
                    if (EyesClosed)
                    {
                        attack = false;
                    }
                }

                if (playerScript.eyesClosedUp >= -1 || playerScript.i == 2) { EyesClosed = true; } else if (playerScript.eyesClosedUp <= -1 || playerScript.i == 1) { EyesClosed = false; }

                if (attack)
                {
                    moth.SetActive(true);
                }
                else
                {
                    moth.SetActive(false);
                }
            }
        }
    }
}
