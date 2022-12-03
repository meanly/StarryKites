using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleButton : MonoBehaviour
{

    bool IsRunning;
    bool StopRunning;
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
            Speed = 1;
            Invoke("RunVariableTrue", 0.5f);
        }
    }

    void Update ()
    {
        if(IsRunning)
        {
            RunVariableFalse();
            Speed = 10;
            Debug.Log("Running!");
        }
    }
}
