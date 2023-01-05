using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialogue, Cutscene, Paused}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;


    TrainerController trainer;
    GameState state;

    GameState stateBeforePause;

    //unloading scenes
    public SceneDetails CurrentScene { get; private set; }
    public SceneDetails PrevScene { get; private set; }

    //menu
    MenuController menuController;

    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;

        menuController = GetComponent<MenuController>();
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

    public void PauseGame(bool pause)
    {
        if(pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        } 
        else
        {
            state = stateBeforePause;
        }

    }

    public void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            //menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuController.OpenMenu();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SavingSystem.i.Save("saveSlot1");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SavingSystem.i.Load("saveSlot1");
            }
            
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
    }

    public void OnEnterTrainersView(TrainerController trainer)
    {
        state = GameState.Cutscene;
        StartCoroutine(trainer.TriggerTrainer(playerController));
    }

    //unloading scenes
    public void SetCurrentScene(SceneDetails currScene)
    {
        PrevScene = CurrentScene;
        CurrentScene = currScene;
    }
}
