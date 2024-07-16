
















using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
public class BedState : BaseState
{
    public override void EnterState(PlayerScript playerScript)
    {
        //playerScript.turnAngle = 0;

        playerScript.canFlash = true;
        playerScript.back = true;
        playerScript.left = false;
        playerScript.right = false;
        playerScript.i = 1;

        playerScript.StartCoroutine(playerScript.ToBed());

        /*
        playerScript.StartCoroutine(playerScript.CloseE(.75f));
        // turn back
        playerScript.StartCoroutine(playerScript.Wait(.6f));*/

        if (Input.GetKey(KeyCode.D))
        {
            playerScript.SwitchState(playerScript.LayRight);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerScript.SwitchState(playerScript.LayLeft);
        }
    }
    public override void UpdaterState(PlayerScript playerScript)
    {
        if (playerScript.canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerScript.SwitchState(playerScript.AtTvState);
            }
        }

        playerScript.leftLimit = playerScript.maxLeftBed1;
        playerScript.rightLimit = playerScript.maxRightBed1;
        playerScript.upLimit = playerScript.maxUpBed1;
        playerScript.downLimit = playerScript.maxDownBed1;

        /*
        if (playerScript.i == 1)
        {
            //if (playerScript.screenCenter >= 0)
            {
                if (playerScript.mousePos.y > playerScript.maxUpBed1)
                { playerScript.mousePos.y = playerScript.maxUpBed1; }
                if (playerScript.mousePos.y < playerScript.maxDownBed1)
                { playerScript.mousePos.y = playerScript.maxDownBed1; }
            }

            if (playerScript.mousePos.x > playerScript.maxLeftBed1)
            { playerScript.mousePos.x = playerScript.maxLeftBed1; }
            if (playerScript.mousePos.x < playerScript.maxRightBed1)
            { playerScript.mousePos.x = playerScript.maxRightBed1; }
        }*/

        if (playerScript.canTurn)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerScript.SwitchState(playerScript.LayLeft);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                playerScript.SwitchState(playerScript.LayRight);
            }

            if (playerScript.back)
            {
                playerScript.i = 1;
                playerScript.liftScreen = false;

                //move and turn player
                playerScript.transform.localPosition =
                    Vector3.MoveTowards(playerScript.transform.position,
                    playerScript.onBack, playerScript.moveSpeed * Time.deltaTime);

                playerScript.transform.rotation =
                    Quaternion.RotateTowards(playerScript.transform.rotation,
                    playerScript.backLay, playerScript.turnSpeed * Time.deltaTime);
            }
        }
    }

    public override void Other(PlayerScript playerScript)
    {

    }
}
