using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButton: MonoBehaviour {
  PlayerController playercontrollerscript;
  DialogueManager dialoguemanagerscript;

  public void ButtonClick() {

    playercontrollerscript = FindObjectOfType <PlayerController> ();
    playercontrollerscript.IsRunning = false;
    playercontrollerscript.isClicked = true;
    playercontrollerscript.interact();
  }
}