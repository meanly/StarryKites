using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleButton: MonoBehaviour {

  /////I DONT USE THIS SCRIPT ANYMORE (TRIANGLE BUTTON IS IN THE PLAYERCONTROLLER.)
    /////
      /////
        /////
          ///// MIGHT BE USEFUL SOMEDAY.
  private Character character;
  private PlayerController playerController;
  bool IsRunning;
  bool StopRunning;

  private void start() {
    character = GetComponent<Character>();
    playerController = GetComponent<PlayerController>();
  }

  public void pointerDown() {
    StopRunning = false;
    Invoke("RunVariableTrue", 0.5f);
  }

  public void pointerUp() {
    IsRunning = false;
    StopRunning = true;
    playerController.IsRunning = false;
    character.Animator.IsRunning = false;
  }

  void RunVariableTrue() {
    IsRunning = true;
  }

  void RunVariableFalse() {
    IsRunning = false;
    if (StopRunning == false) {
      Invoke("RunVariableTrue", 0.5f);
    }
  }

  void Update() {
    if (IsRunning) {
      RunVariableFalse();
      playerController.IsRunning = true;
      character.Animator.IsRunning = true;
      Debug.Log("The player is now running!"); 
    }
  }
}