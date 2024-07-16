
















using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftState : BaseState
{
    public override void EnterState(PlayerScript playerScript)
    {
        playerScript.flashlight.SetActive(false);
        playerScript.maxDownBed2 = 2;

        playerScript.canFlash = false;
        playerScript.back = false;
        playerScript.left = true;
        playerScript.right = false;

        //playerScript.liftScreen = true;
        playerScript.StartCoroutine(playerScript.CloseE(.75f));
        // turn back
        playerScript.StartCoroutine(playerScript.Wait(.6f));
    }
    public override void UpdaterState(PlayerScript playerScript)
    {
        if(playerScript.transform.rotation == playerScript.leftLay)
        {
            playerScript.i = 2;
        }

        if (playerScript.canTurn)
        {
            if (Input.GetKey(KeyCode.D))
            {
                playerScript.SwitchState(playerScript.InBedState);
                playerScript.liftScreen = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerScript.SwitchState(playerScript.InBedState);
            }
        }

        playerScript.leftLimit = playerScript.maxLeftBed2;
        playerScript.rightLimit = playerScript.maxRightBed2;
        playerScript.upLimit = playerScript.maxUpBed2;

        if (playerScript.maxDownBed2 <= 70)
        {
            playerScript.maxDownBed2++;
        }
        playerScript.downLimit = -playerScript.maxDownBed2;



        /*
        if (playerScript.i == 2){
            //if (playerScript.screenCenter >= 250)
            {
                if (playerScript.mousePos.y > playerScript.maxUpBed2)
                { playerScript.mousePos.y = playerScript.maxUpBed2; }
                if (playerScript.mousePos.y < playerScript.maxDownBed2)
                { playerScript.mousePos.y = playerScript.maxDownBed2; }
            }

            if (playerScript.mousePos.x > playerScript.maxLeftBed2)
            { playerScript.mousePos.x = playerScript.maxLeftBed2; }
            if (playerScript.mousePos.x < playerScript.maxRightBed2)
            { playerScript.mousePos.x = playerScript.maxRightBed2; }
        }*/

        //move and turn player
        playerScript.transform.localPosition = 
            Vector3.MoveTowards(playerScript.transform.position, 
            playerScript.moveLeft, playerScript.moveSpeed * Time.deltaTime);

        playerScript.transform.rotation = 
            Quaternion.RotateTowards(playerScript.transform.rotation, 
            playerScript.leftLay, playerScript.turnSpeed * Time.deltaTime);
    }
    public override void Other(PlayerScript playerScript)
    {

    }
}