using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterAnimator;

public class TrainerController : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] GameObject exclamation;
    [SerializeField] GameObject fov;
    Character character;

    private void Awake() {
        character = GetComponent<Character>();
    }

    private void Start() {
        SetFovRotation(character.Animator.DefaultDirection);
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
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue, () => 
        {
            Debug.Log("Starting Trainer.");
        }));
    }

    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
        else if (dir == FacingDirection.Up)
            angle = 180f;
        else if (dir == FacingDirection.Left)
            angle = 270f; //no f

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    //save and restore
}
