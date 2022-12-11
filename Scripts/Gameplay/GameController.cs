using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialogue, Cutscene}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;


    TrainerController trainer;
    GameState state;

    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void Start()
    {

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

    public void OnEnterTrainersView(TrainerController trainer)
    {
        Debug.Log("test");
        state = GameState.Cutscene;
        StartCoroutine(trainer.TriggerTrainer(playerController));
    }
}
