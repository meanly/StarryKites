using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleButton : MonoBehaviour
{

/////PENDING WORK: IMPLEMENTING THIS BUTTON FOR RUNNING
    CharacterAnimator characterAnimator;
    bool IsRunning;
    bool StopRunning;

    private void start()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void pointerDown()
    {
        StopRunning = false;
        Invoke("RunVariableTrue", 0.5f);
    }
    
    public void pointerUp()
    {
        IsRunning = false;
        StopRunning = true;
    }

    void RunVariableTrue()
    {
        IsRunning = true;
    }

    void RunVariableFalse()
    {
        IsRunning = false;
        if (StopRunning == false)
        {
            
            Invoke("RunVariableTrue", 0.5f);
        }
    }

    void Update ()
    {
        if(IsRunning)
        {
            RunVariableFalse();
            
            Debug.Log("The player is now running!");  //i need to change the value of the movementspeed and animation in this block
        }
    }
}
