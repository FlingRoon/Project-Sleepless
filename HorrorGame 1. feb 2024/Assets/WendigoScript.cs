using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WendigoScript : MonoBehaviour
{
    public WendigoBaseState currentState;
    public WendigoStateA aState = new WendigoStateA();
    public WendigoStateB bState = new WendigoStateB();

    public PlayerScript playerScript;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    public Transform pos6;
    public Transform pos7;
    public Transform pos8;
    public GameObject wardrobeDoor;
    public GameObject balconyDoor;
    public GameObject roomDoor;
    public GameObject window;
    public GameObject backWindow;

    public Transform killZone;

    public bool openWardrobeDoor;
    public bool openBalconyDoor;
    public bool openRoomDoor;
    public bool openWindow;
    public bool openBackWindow;

    [Header("Nights")]
    [SerializeField]
    int night1DL;
    [SerializeField]
    int night2DL, night3DL, night4DL, night5DL, night6DL, night7DL, night8DL;

    [Header("Time")]
    public float time;
    public float randomTime;
    public float fullPercentage;
    public float randomValuePercentage;
    public float randomValue;
    public float percentage;

    public int damage;
    public int wendigoDangerLevel;
    public bool attack;
    public bool EyesClosed = false;
    bool doOnce;
    float timer;

    bool start;

    [Range(0, 8)]
    public int position;


    [Header("Audio Files")]

    AudioSource audioData;
    public AudioClip[] audioClip;


    void Start()
    {
        audioData = GetComponent<AudioSource>();


        start = true;
        attack = false;
        currentState = aState;
        currentState.EnterState(this);
    }

    void Update()
    {/*
        if (playAudio)
        {
            audioData.Play();
        }*/


        if (openBackWindow)
        {
            backWindow.transform.localRotation =
            Quaternion.RotateTowards(backWindow.transform.localRotation,
            Quaternion.Euler(-90, 0, -150), 240 * Time.deltaTime);

        }
        else
        {
            backWindow.transform.localRotation =
            Quaternion.RotateTowards(backWindow.transform.localRotation,
            Quaternion.Euler(-90, 0, -90), 240 * Time.deltaTime);
        }

        if (openWindow)
        {
            window.transform.localRotation =
            Quaternion.RotateTowards(window.transform.localRotation,
            Quaternion.Euler(-90, 0, -200), 200 * Time.deltaTime);
        }
        else
        {
            window.transform.localRotation =
            Quaternion.RotateTowards(window.transform.localRotation,
            Quaternion.Euler(-90, 0, -90), 200 * Time.deltaTime);
        }

        if (openBalconyDoor)
        {
            if (balconyDoor.transform.localRotation == Quaternion.Euler(0, 0, 0))
            {
                PlayAudio(0, balconyDoor, true);
            }

            balconyDoor.transform.localRotation =
            Quaternion.RotateTowards(balconyDoor.transform.localRotation,
            Quaternion.Euler(0, 0, -80), 50 * Time.deltaTime);
        }
        else
        {
            if (balconyDoor.transform.localRotation == Quaternion.Euler(0, 0, -80))
            {
                PlayAudio(0, balconyDoor, true);
            }

            balconyDoor.transform.localRotation =
            Quaternion.RotateTowards(balconyDoor.transform.localRotation,
            Quaternion.Euler(0, 0, 0), 50 * Time.deltaTime);
        }

        if (openRoomDoor)
        {
            roomDoor.transform.localRotation =
            Quaternion.RotateTowards(roomDoor.transform.localRotation,
            Quaternion.Euler(0, 0, 80), 50 * Time.deltaTime);
        }
        else
        {
            roomDoor.transform.localRotation =
            Quaternion.RotateTowards(roomDoor.transform.localRotation,
            Quaternion.Euler(0, 0, 0), 500 * Time.deltaTime);
        }

        if (openWardrobeDoor)
        {
            wardrobeDoor.transform.localPosition =
                Vector3.MoveTowards(wardrobeDoor.transform.localPosition,
                new Vector3(-2.125f, 0.125f, -1.5f), 5 * Time.deltaTime);
        }
        else
        {
            wardrobeDoor.transform.localPosition =
                Vector3.MoveTowards(wardrobeDoor.transform.localPosition,
                new Vector3(-1.3f, 0.125f, -1.5f), 5 * Time.deltaTime);
        }


        if (start)
        {
            timer += Time.deltaTime;
        }
        // tells the script what difficulty to set the wendigo too.
        #region "Difficulty sellection"
        switch (playerScript.night)
        {
            // use a switch statement so you only evaluate once for everything
            case 2:
                {
                    wendigoDangerLevel = night2DL;
                    break;
                }
            case 3:
                {
                    wendigoDangerLevel = night3DL;
                    break;
                }
            case 4:
                {
                    wendigoDangerLevel = night4DL;
                    break;
                }
            case 5:
                {
                    wendigoDangerLevel = night5DL;
                    break;
                }
            case 6:
                {
                    wendigoDangerLevel = night6DL;
                    break;
                }
            case 7:
                {
                    wendigoDangerLevel = night7DL;
                    break;
                }
            case 8:
                {
                    wendigoDangerLevel = night8DL;
                    break;
                }
            default: // 1
                {
                    wendigoDangerLevel = night1DL;
                    break;
                }
        }
        #endregion



        if (timer >= 10)
        {
            fullPercentage = percentage * randomValuePercentage;

            randomValuePercentage = (12f + wendigoDangerLevel) / 10;

            time += Time.deltaTime;

            if (time >= randomTime)
            {
                randomValue = Random.value * 100;
                time = 0;
            }

            percentage = 30 + wendigoDangerLevel;

            if (doOnce)
            {
                wendigoDangerLevel += 1; doOnce = false;
            }

            if (wendigoDangerLevel != 0)
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
                        attack = true;
                    }
                }

                //should update
                if (randomValue >= fullPercentage)
                {
                    if (EyesClosed)
                    {
                        attack = false;
                    }
                }

                // if tells the script the players eyes are closed.
                if (playerScript.eyesClosedUp >= -1 ||
                playerScript.i == 2) { EyesClosed = true; }
                else if (playerScript.eyesClosedUp <= -1 ||
                playerScript.i == 1) { EyesClosed = false; }

            }
        }




        #region "Clear Null"
        //Debug.Log("Janez " + this is null);
        //Debug.Log("Stuff " + currentState is null);

        if (currentState is null)
        {
            currentState = aState;
            currentState.EnterState(this);
        }

        if (this is null)
        {
            this.currentState = aState;
            currentState.EnterState(this);
        }
        //Debug.Log("Janez 1 " + this is null);
        //Debug.Log("Stuff 1 " + currentState is null);
        #endregion
        currentState.UpdaterState(this);
        // OTHER CODE
    }

    public void PlayAudio(int clipCount, GameObject playAt, bool play)
    {
        if (play)
        {
            AudioSource.PlayClipAtPoint(audioClip[clipCount], playAt.transform.position);
        }
    }

    // trigger checks (Deal damage)
    public void OnTriggerEnter(Collider other)
    {
        //checks if the wendigo has entered the killzone
        if(other.transform == killZone)
        {
            Debug.Log(other.gameObject.name + " hit");
            playerScript.health -= damage;
        }
    }
    public void SwitchState(WendigoBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}