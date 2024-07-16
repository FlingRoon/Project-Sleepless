
















using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightState : BaseState
{
    public override void EnterState(PlayerScript playerScript)
    {
        //playerScript.turnAngle = -90;

        playerScript.canFlash = true;
        playerScript.back = false;
        playerScript.left = false;
        playerScript.right = true;
        playerScript.i = 0;

        playerScript.StartCoroutine(playerScript.CloseE(.75f));
        // turn back
        playerScript.StartCoroutine(playerScript.Wait(.6f));
    }
    public override void UpdaterState(PlayerScript playerScript)
    {
        if (playerScript.canTurn){
            if (Input.GetKey(KeyCode.A)){
                playerScript.SwitchState(playerScript.InBedState);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerScript.SwitchState(playerScript.InBedState);
            }
        }

        playerScript.leftLimit = playerScript.maxLeftBed;
        playerScript.rightLimit = playerScript.maxRightBed;
        playerScript.upLimit = playerScript.maxUpBed;
        playerScript.downLimit = playerScript.maxDownBed;

        /*
        if (playerScript.i == 0)
        {
            //if (playerScript.screenCenter >= 0)
            {
                if (playerScript.mousePos.y > playerScript.maxUpBed)
                { playerScript.mousePos.y = playerScript.maxUpBed; }
                if (playerScript.mousePos.y < playerScript.maxDownBed)
                { playerScript.mousePos.y = playerScript.maxDownBed; }
            }

            if (playerScript.mousePos.x > playerScript.maxLeftBed)
            { playerScript.mousePos.x = playerScript.maxLeftBed; }
            if (playerScript.mousePos.x < playerScript.maxRightBed)
            { playerScript.mousePos.x = playerScript.maxRightBed; }
        }*/

        //playerScript.liftScreen = false;

        //move and turn player
        playerScript.transform.localPosition =
            Vector3.MoveTowards(playerScript.transform.position,
            playerScript.moveRight, playerScript.moveSpeed * Time.deltaTime);

        playerScript.transform.rotation =
            Quaternion.RotateTowards(playerScript.transform.rotation,
            playerScript.rightLay, playerScript.turnSpeed * Time.deltaTime);
    }
    public override void Other(PlayerScript playerScript)
    {

    }
}
