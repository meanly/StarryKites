using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialogue, Cutscene}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;

    public void Start()
    {

        playerController.OnEnterTrainersView += (Collider2D trainerCollider) =>
        {
           var trainer = trainerCollider.GetComponentInParent<TrainerController>();
           if (trainer != null)
           {
               state = GameState.Cutscene;
               StartCoroutine(trainer.TriggerTrainerBattle(playerController));
           }

        };

        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };

        DialogueManager.Instance.OnCloseDialogue += () =>
        {
            if (state == GameState.Dialogue)
            state = GameState.FreeRoam;
        };
    }

    public void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
    }
}
