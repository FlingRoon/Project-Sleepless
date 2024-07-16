
















using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvState : BaseState
{
    public override void EnterState(PlayerScript playerScript)
    {
        playerScript.flashlight.SetActive(false);
        playerScript.canFlash = false;
        playerScript.StartCoroutine(playerScript.ToTv());
        playerScript.StartCoroutine(playerScript.CloseE(.75f));
        // turn back
        playerScript.StartCoroutine(playerScript.Wait(.6f));
    }
    public override void UpdaterState(PlayerScript playerScript)
    {
        if (Input.GetKey(KeyCode.Space)){
            playerScript.sleep -= 1.5f;}
        if (playerScript.tvOn)
        {   
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //playerScript.tvOn = false;
                playerScript.tvOff = false;
            }
            else
            {
                playerScript.tvOff = true;
            }
        }


        if (playerScript.canMove)
        {
            if (Input.GetKey(KeyCode.S))
            {
                playerScript.SwitchState(playerScript.InBedState);
            }
        }
        playerScript.leftLimit = playerScript.maxLeftTv;
        playerScript.rightLimit = playerScript.maxRightTv;
        playerScript.upLimit = playerScript.maxUpTv;
        playerScript.downLimit = playerScript.maxDownTv;


    }

    public override void Other(PlayerScript playerScript)
    {

    }
}
