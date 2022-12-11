using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject exclamation;
    Character character;

    private void Awake() {
        character = GameComponent<Character>();
    }

    public IEnumerator TriggerTrainer(PlayerController player) //TriggerTrainerBattle
    {
        //show the exclamation sprite
        exclamation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclamation.SetActive(false);
        
        //walk towards the player
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        //show the dialogue
        StartCoroutine(DialogueManager.instance.ShowDialogue(dialogue, () => 
        {
            Debug.Log("Starting Trainer.");
        }));
        
    
    }
}
