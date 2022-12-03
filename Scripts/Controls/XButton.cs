using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class XButton: MonoBehaviour
{
	PlayerController playercontrollerscript;
	DialogueManager dialoguemanagerscript;

    public void ButtonClick(){

		playercontrollerscript = FindObjectOfType<PlayerController>();
		playercontrollerscript.isClicked = true;
		playercontrollerscript.interact();
    }
}
