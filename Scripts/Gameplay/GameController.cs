using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialogue, Menu, Cutscene, Paused}

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

        menuController.onBack += () =>
        {
            state = GameState.FreeRoam;
        };

        menuController.onMenuSelected += OnMenuSelected;
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
                state = GameState.Menu;
            }
            
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Menu)
        {
            menuController.HandleUpdate();
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

    //actions for selections of menu
    void OnMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {
            //continue
        }
        else if (selectedItem == 1)
        {
            //mainmenu
        }
        else if (selectedItem == 2)
        {
            //inventory
        }
        else if (selectedItem == 3)
        {
            SavingSystem.i.Save("saveSlot1");
        }
        else if (selectedItem == 4)
        {
            SavingSystem.i.Load("saveSlot1");
        }

        state = GameState.FreeRoam;
    }
}
