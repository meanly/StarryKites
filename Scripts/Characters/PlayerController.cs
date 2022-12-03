using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    private Vector2 input;
    private Character character;

    public bool isClicked;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            input.x = CrossPlatformInputManager.GetAxis("Horizontal");
            input.y = CrossPlatformInputManager.GetAxis("Vertical");

            //remove diagonal
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input));
            }
        }
        character.HandleUpdate();
        if(isClicked == true)
        interact();
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

} //class 
