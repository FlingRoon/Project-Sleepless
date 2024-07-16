















using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScript : MonoBehaviour
{
    public Transform tvSwitch;
    public Transform tvSlider1;
    public Transform tvSlider2;
    public Transform tvSlider3;
    public Transform tvSlider4;
    public Transform tvSlider5;

    public PlayerScript playerScript;

    public float switchTurnSpeed;

    public float switchSpeed;

    public bool tvOn;

    public Vector3 switch1pos;
    public Vector3 switch2pos;
    public Vector3 switch3pos;
    public Vector3 switch4pos;
    public Vector3 switch5pos;

    public Quaternion switchRot;

    public bool timer;
    public float time;

    public bool tvOff;


    [Header("Audio Files")]

    AudioSource audioData;
    public AudioClip[] audioClip;

    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

        switch1pos = new Vector3(-0.0052f, -0.0036f, 0.0085f);
        switch2pos = new Vector3(-0.005f, -0.0036f, 0.0085f);
        switch3pos = new Vector3(-0.0048f, -0.0036f, 0.0085f);
        switch4pos = new Vector3(-0.0046f, -0.0036f, 0.0085f);
        switch5pos = new Vector3(-0.0044f, -0.0036f, 0.0085f);
    }

    // Update is called once per frame
    void Update()
    {
        tvOff = playerScript.tvOff;
        tvOn = playerScript.tvOn;
        playerScript.tvOff = tvOff;
        playerScript.tvOn = tvOn;

        tvSlider1.transform.localPosition = Vector3.MoveTowards(
        tvSlider1.transform.localPosition, 
        switch1pos, (Time.deltaTime * switchSpeed / 1000));

        tvSlider2.transform.localPosition = Vector3.MoveTowards(
        tvSlider2.transform.localPosition, 
        switch2pos, (Time.deltaTime * switchSpeed / 1000));

        tvSlider3.transform.localPosition = Vector3.MoveTowards(
        tvSlider3.transform.localPosition, 
        switch3pos, (Time.deltaTime * switchSpeed / 1000));

        tvSlider4.transform.localPosition = Vector3.MoveTowards(
        tvSlider4.transform.localPosition, 
        switch4pos, (Time.deltaTime * switchSpeed / 1000));

        tvSlider5.transform.localPosition = Vector3.MoveTowards(
        tvSlider5.transform.localPosition, 
        switch5pos, (Time.deltaTime * switchSpeed / 1000));

        tvSwitch.transform.localRotation = Quaternion.RotateTowards(
        tvSwitch.transform.localRotation,
        switchRot, (Time.deltaTime * switchTurnSpeed));
        
        if (tvOn)
        {
            light.SetActive(true);
            if (switch1pos != new Vector3
            (-0.0052f, -0.0036f, 0.0082f) 
            && tvOff)
            {
                PlayAudio(0);
            }//, gameObject, true

            //tvOff = false;
            switch1pos = new Vector3
            (-0.0052f, -0.0036f, 0.0082f);
            switch2pos = new Vector3
            (-0.005f, -0.0036f, 0.0080f);
            switch3pos = new Vector3
            (-0.0048f, -0.0036f, 0.0084f);
            switch4pos = new Vector3
            (-0.0046f, -0.0036f, 0.0086f);
            switch5pos = new Vector3
            (-0.0044f, -0.0036f, 0.0083f);

            switchRot =
            Quaternion.Euler(0, -45, 0);
        }
        else
        {
            light.SetActive(false);
            audioData.Stop();

            /*tvOff = true;
            //switch1pos = new Vector3(-0.0052f, -0.0036f, 0.0085f);
            //switch2pos = new Vector3(-0.005f, -0.0036f, 0.0085f);
            //switch3pos = new Vector3(-0.0048f, -0.0036f, 0.0085f);
            //switch4pos = new Vector3(-0.0046f, -0.0036f, 0.0085f);
            //switch5pos = new Vector3(-0.0044f, -0.0036f, 0.0085f);
            //switchRot = Quaternion.Euler(0, 15, 0);*/
        }

        if (timer)
        {
            time += Time.deltaTime;
        }
        
        // If the tv os not off in this scenerio. the switches will start moving back to the off position
        if (!tvOff)
        {
            timer = true;
            if (time != 0)
            {
                switch1pos = new Vector3(-0.0052f, -0.0036f, 0.0085f);
            }

            if (time > 1)
            {
                switch2pos = new Vector3(-0.005f, -0.0036f, 0.0085f);
            }

            if (time > 2)
            {
                switch3pos = new Vector3(-0.0048f, -0.0036f, 0.0085f);
            }

            if (time > 3)
            {
                switch4pos = new Vector3(-0.0046f, -0.0036f, 0.0085f);
            }

            if (time > 4)
            {
                switch5pos = new Vector3(-0.0044f, -0.0036f, 0.0085f);
            }

            if (time > 4.5f)
            {
                switchRot = Quaternion.Euler(0, 15, 0);


                playerScript.tvOn = false;
                tvOn = false;
                tvOff = true;
                playerScript.tvOff = true;
                time = 0;
                audioData.Stop();
            }
        }
        else
        {
            timer = false;
        }
    }

    public void PlayAudio(int clipCount)
    {//, GameObject playAt, bool play
        //if (play)
        {
            //AudioSource.PlayClipAtPoint(audioClip[clipCount], playAt.transform.position);
            audioData.clip = audioClip[clipCount];
            audioData.Play();
        }
    }
}
