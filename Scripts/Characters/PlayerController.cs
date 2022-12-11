using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    const float offsetY = 0.3f;
    private Vector2 input;
    private Character character;

    public bool isClicked;

    public bool IsRunning;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            //for pc controls WASD
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        } 
        if (!character.isMoving)
        {
            //for mobile touchpad
            input.x = CrossPlatformInputManager.GetAxis("Horizontal");
            input.y = CrossPlatformInputManager.GetAxis("Vertical");
            
            //remove diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }

        character.HandleUpdate();
        if(isClicked == true)
        interact();

        if(Input.GetKeyDown(KeyCode.Z)) //does nt work atm
        {
            interact();
        }

        //running
        
        CheckIfRunningButtonIsPressed();
        if(!IsRunning) // Checks if the player isn't running, then he is walking
        {
            character.moveSpeed = 5f;
        } else {
            character.moveSpeed = 11f;
            Debug.Log("The player is now running!"); 
        }
    }

    public void interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        isClicked = true;
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.interact(transform);
        }
    }

    private void OnMoveOver()
    {
       var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, offsetY), 0.2f, GameLayers.i.TriggerableLayers);

       foreach (var collider in colliders)
       {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                character.Animator.isMoving = false;
                triggerable.OnPlayerTriggered(this);
                break;
            }
       }
    }

//////////////////RUNNING EXECUTION

    private void CheckIfRunningButtonIsPressed()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            IsRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            IsRunning = false;
        }
        character.Animator.IsRunning = IsRunning;
    }

    public void pointerDown(){
        IsRunning = true;
        character.Animator.IsRunning = true;
    }

    public void pointerUp() {
        IsRunning = false;
        character.Animator.IsRunning = false;
    }

} //class 
