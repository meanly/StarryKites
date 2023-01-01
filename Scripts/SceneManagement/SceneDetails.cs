using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;

    public bool IsLoaded { get; private set; }

    List<SavableEntity> savableEntities;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player");
        {
            Debug.Log($"Entered {gameObject.name}");

            LoadScene();
            GameController.Instance.SetCurrentScene(this);

            //load all connected scenes
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            //unload the scenes
            if(GameController.Instance.PrevScene != null)
            {
                var previouslyLoadedScenes = GameController.Instance.PrevScene.connectedScenes;
                foreach (var scene in previouslyLoadedScenes)
                {
                    if (!connectedScenes.Contains(scene) && scene != this) //if connected and the current scene
                    {
                        scene.UnloadScene();
                    }
                }
            }

        }
    }

    public void LoadScene()
    {
        if (!IsLoaded)
        {
            var operation = SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;

            operation.completed += (AsyncOperation op) =>
            {
                savableEntities = GetSavableEntitiesInScene();
                SavingSystem.i.RestoreEntityStates(savableEntities);
            };

        }
    }

    public void UnloadScene()
    {
        if (IsLoaded)
        {
            SavingSystem.i.CaptureEntityStates(savableEntities);

            SceneManager.UnloadSceneAsync(gameObject.name);
            IsLoaded = false;
        }
    }

    List<SavableEntity> GetSavableEntitiesInScene()
    {
        var currScene = SceneManager.GetSceneByName(gameObject.name);
        var = savableEntities = FindObjectsOfType<SavableEntity>().Where(x => x.gameObject.scene == currScene).ToList();

        return savableEntities;
    }
}
