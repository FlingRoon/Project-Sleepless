using UnityEngine;

public class WendigoStateA : WendigoBaseState
{
    public override void EnterState(WendigoScript wendigoScript)
    {
        Debug.Log("Wendigo is idle");
        Debug.Log("current state is " + wendigoScript.currentState);
        wendigoScript.transform.position = wendigoScript.pos1.position;
        wendigoScript.transform.rotation = wendigoScript.pos1.rotation;
    }
    public override void UpdaterState(WendigoScript wendigoScript)
    {

        switch (wendigoScript.position)
        {
            // use a switch statement so you only evaluate once for everything
            case 1:
                {
                    wendigoScript.transform.position = wendigoScript.pos2.position;
                    break;
                }
            case 2:
                {
                    wendigoScript.transform.position = wendigoScript.pos3.position;
                    break;
                }
            case 3:
                {
                    wendigoScript.transform.position = wendigoScript.pos4.position;
                    break;
                }
            case 4:
                {
                    wendigoScript.transform.position = wendigoScript.pos5.position;
                    break;
                }
            case 5:
                {
                    wendigoScript.transform.position = wendigoScript.pos6.position;
                    break;
                }
            case 6:
                {
                    wendigoScript.transform.position = wendigoScript.pos7.position;
                    break;
                }
            case 7:
                {
                    wendigoScript.transform.position = wendigoScript.pos8.position;
                    break;
                }
            case 8:
                {

                    wendigoScript.transform.position = wendigoScript.killZone.position;
                    break;
                }
            default: // 0
                {
                    wendigoScript.transform.position = wendigoScript.pos1.position;
                    break;
                }
        }





        if (wendigoScript.attack || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.O)))
        {
            wendigoScript.SwitchState(wendigoScript.bState);
        }
    }
    public override void Other(WendigoScript wendigoScript)
    {

    }
}
