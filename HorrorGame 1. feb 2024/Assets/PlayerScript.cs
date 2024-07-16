
















using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System;
using UnityEditor;
using UnityEngine.TextCore.Text;

public class PlayerScript : MonoBehaviour
{
    #region["FUCK"]
    [Header("Nights & Danger")]
    [Range(1, 8)]
    public int night = 1;
    public int dangerLevel;

    [Header("Sleep")]
    public float maxSleep;
    public float baseSleep;
    public float sleep;
    public float sleepCount;

    [Header("Eyes")]
    public bool closeEyes;
    [Range(0, -50)]
    public float eyesClosedUp;
    [Range(0, -45)]
    public float eyesClosedDown;
    public float eyeCloseSpeed;

    [Header("Turning & Rotating")]
    public bool canTurn;
    public Quaternion backLay = Quaternion.Euler(-90, 0, 0);
    public Quaternion rightLay = Quaternion.Euler(-170, -90, 90);
    public Quaternion leftLay = Quaternion.Euler(-10, -90, 90);
    public Quaternion rotToo;
    public float turnSpeed;

    public Vector3 moveRight = new Vector3(-2.4f, 0.9f, -3);
    public Vector3 moveLeft = new Vector3(-3.1f, 0.9f, -3);
    public Vector3 onBack = new Vector3(-2.75f, 0.75f, -3);
    public float moveSpeed;

    float rotationX;
    public float upLimit;
    public float downLimit;
    float rotationY;
    public float rightLimit;
    public float leftLimit;

    [Header("Mouse & View")]
    public float sensitivity;
    public bool liftScreen;
    public float screenCenter;
    
    [Header("Health")]
    [Range(0, 3)]
    public int health = 3;

    [Header("FlashLight")]
    public bool canFlash;

    [Header("Move, bed & tv")]
    public bool canMove;
    public int z;
    public float timer = 1;
    public bool move;
    public float speed = 1;

    public Vector3 bed;
    public Vector3 tv;

    #region["STUFF"]

    [Range(0, 360)]
    public float maxUpBed = 90;
    [Range(0, 360)]
    public float maxDownBed = 0;
    [Range(0, 360)]
    public float maxLeftBed = 20;
    [Range(0, 360)]
    public float maxRightBed = 20;


    [Range(0, 360)]
    public float maxUpBed1 = 60;
    [Range(0, 360)]
    public float maxDownBed1 = 20;
    [Range(0, 360)]
    public float maxLeftBed1 = 86;
    [Range(0, 360)]
    public float maxRightBed1 = 90;


    [Range(0, 360)]
    public float maxUpBed2 = 90;
    [Range(0, 360)]
    public float maxDownBed2 = 0;
    [Range(0, 360)]
    public float maxLeftBed2 = 20;
    [Range(0, 360)]
    public float maxRightBed2 = 20;

    [Range(0, 360)]
    public float maxUpTv = 5;
    [Range(0, 360)]
    public float maxDownTv = 20;
    [Range(0, 360)]
    public float maxLeftTv = 50;
    [Range(0, 360)]
    public float maxRightTv = 20;

    #endregion

    [SerializeField]
    [Range(0, 2)]
    public int i = 1;
    public bool canLook;

    [Header("Extras")]
    public Transform cam;
    public Camera camcam;
    public Image upperEyelid;
    public Image lowerEyelid;
    public Image fade;
    public GameObject flashlight;
    #endregion

    //State Stuff
    public BaseState currentState;
    public TvState AtTvState = new TvState();
    public BedState InBedState = new BedState();
    public RightState LayRight = new RightState();
    public LeftState LayLeft = new LeftState();

    public bool left;
    public bool back;
    public bool right;

    public bool tvOn;
    public bool tvOff;

    //public Image SleepBar;
    public Slider SleepSlider;

    bool fuckOff = true;


    [Header("Audio Files")]

    AudioSource audioData;
    public AudioClip[] audioClip;

    void Start()
    {
        audioData = GetComponent<AudioSource>();

        tvOn = false;
        tvOff = true;
        dangerLevel = 1;
        closeEyes = true;
        currentState = InBedState;
        currentState.EnterState(this);
    }
    void Update()
    {
        SleepSlider.maxValue = maxSleep;

        SleepSlider.value = sleep;

        rotationX += -Input.GetAxisRaw("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -upLimit, downLimit);
        //cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        rotationY += Input.GetAxisRaw("Mouse X") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -leftLimit, rightLimit);

        cam.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //camcam.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        /*
        if (liftScreen)
        {
            if (screenCenter <= 220)
            {
                screenCenter += 10;
            }
        }
        else
        {
            if (screenCenter >= 70)
            {
                screenCenter -= 10;
            }
        }*/

        // Danger Level
        dangerLevel = night + i;
        // Sleep system
        float elNight = night;
        maxSleep = baseSleep + baseSleep * ((elNight / 9) + (elNight / 6));

        if (sleep > maxSleep)
        {
            sleep = maxSleep;
        }
        if (eyesClosedUp < -50)
        {
            eyesClosedUp = -50;
        }
        if (eyesClosedUp > 0)
        {
            eyesClosedUp = 0;
        }
        if (eyesClosedDown < -45)
        {
            eyesClosedDown = -45;
        }
        if (eyesClosedDown > 0)
        {
            eyesClosedDown = 0;
        }

        fade.color = new Color(0, 0, 0, 1 - (-eyesClosedUp * 2) / 80);

        upperEyelid.rectTransform.localPosition = new Vector3(0, 210 - (eyesClosedUp * 6), 0);
        lowerEyelid.rectTransform.localPosition = new Vector3(0, -250 + (eyesClosedDown * 3), 0);

        if(health <= 0)
        {
            EndGame();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            closeEyes = true;
            canMove = false;
        }
        else
        {   
            closeEyes = false;
            canMove = true;
        }

        if (closeEyes)
        { Sleep(i, true); }
        else
        { Sleep(i, false); }

        if (canFlash)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && flashlight.active == false)
            {
                // flashlight
                
                    PlayAudio(0, gameObject, true);
                
                flashlight.SetActive(true);
            } else
            if(Input.GetKeyDown(KeyCode.Mouse0) && flashlight.active == true)
            {

                    PlayAudio(0, gameObject, true);
                
                flashlight.SetActive(false);
            }
        }
        else
        {
            flashlight.SetActive(false);
        }
        float a = 15;
        if (eyesClosedUp >= -.3f)
        {
            if (SleepSlider.transform.localPosition.y >= a)
            {
                fuckOff = true;
            }

            else if (SleepSlider.transform.localPosition.y <= -a)
            {
                fuckOff = false;
            }

            if (fuckOff)
            {
                SleepSlider.transform.localPosition =
                    Vector3.MoveTowards(SleepSlider.transform.localPosition,
                    new Vector3(0, -a, 0), .5f);
            }else
            {
                SleepSlider.transform.localPosition =
                    Vector3.MoveTowards(SleepSlider.transform.localPosition,
                    new Vector3(0, a, 0), 2);
            }
        }



        #region "Clear Null"
        //Debug.Log("Janez " + this is null);
        //Debug.Log("Stuff " + currentState is null);

        if (currentState is null)
        {
            currentState = InBedState;
            currentState.EnterState(this);
        }

        if (this is null)
        {
            this.currentState = InBedState;
            currentState.EnterState(this);
        }

        //Debug.Log("Janez 1 " + this is null);
        //Debug.Log("Stuff 1 " + currentState is null);
        #endregion
        currentState.UpdaterState(this);
        
    }


    public void PlayAudio(int clipCount, GameObject playAt, bool play)
    {
        if (play)
        {
            AudioSource.PlayClipAtPoint(audioClip[clipCount], playAt.transform.position);
        }
    }

    public void EndGame()
    {
        Debug.Log("GameOver");
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("took " + damage + "hearts of damage");
    }
    public void Sleep(int SleepPosition, bool sleep)
    {
        if (sleep) CloseEyes(SleepPosition, true);
        else CloseEyes(SleepPosition, false);
    }
    float time = 0;

    public void CloseEyes(int SleepPosition, bool Close)
    {
        sleepCount = 1 + SleepPosition;
        if (Close)
        {
            eyesClosedUp += eyeCloseSpeed;
            eyesClosedDown += eyeCloseSpeed;

            time += Time.deltaTime;
            if (time >= 1)
            {
                sleep = sleep + sleepCount;
                dangerLevel = night + i + 1;
            }

            if (eyesClosedUp >= -.3f)
            {
                SleepSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            eyesClosedUp -= eyeCloseSpeed;
            eyesClosedDown -= eyeCloseSpeed;
            time = 0;

            if (eyesClosedUp < -.3f)
            {
                SleepSlider.gameObject.SetActive(false);
            }
        }   
    }
    public IEnumerator ToTv()
    {
        //Close Eyes
        closeEyes = true;
        sleepCount = -2;
        yield return new WaitForSeconds(.3f);
        transform.rotation = Quaternion.Euler(-90, -90, 90);
        transform.position = new Vector3(-2.75f, 0.75f, -3);
        //moves head to tv
        cam.transform.localPosition = Vector3.MoveTowards(bed, tv, speed);
        yield return new WaitForSeconds(.2f);
        //open eyes
        closeEyes = false;

        /* Alternative that i might use

        closeEyes = true;
        sleepCount = -2;
        yield return new WaitForSeconds(.3f);
        transform.rotation = Quaternion.Euler(-90, -90, 90);
        //moves head to tv
        cam.transform.localPosition = Vector3.MoveTowards(bed, tv, speed);
        yield return new WaitForSeconds(.2f);
        //open eyes
        //start playing animation
        closeEyes = false;
        //finish playing animation
        */

        canTurn = false;
        move = true;
    }
    public IEnumerator ToBed()
    {
        //Close Eyes
        closeEyes = true;
        sleepCount = -2;
        yield return new WaitForSeconds(.3f);
        //moves head to bed
        cam.transform.localPosition = Vector3.MoveTowards(tv, bed, speed);
        yield return new WaitForSeconds(.2f);
        //open eyes
        closeEyes = false;

        /* Alternative that i might use

        closeEyes = true;
        sleepCount = -2;
        yield return new WaitForSeconds(.3f);
        //moves head to tv
        cam.transform.localPosition = Vector3.MoveTowards(tv, bed, speed);
        yield return new WaitForSeconds(.2f);
        //open eyes
        //start playing animation
        closeEyes = false;
        //finish playing animation
        */

        canTurn = true;
        move = false;
    }
    public IEnumerator CloseE(float i)
    {
        closeEyes = true;
        yield return new WaitForSeconds(i);
        closeEyes = false;
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}