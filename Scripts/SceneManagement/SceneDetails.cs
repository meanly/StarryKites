using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;

    public bool IsLoaded { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player");
        {
            Debug.Log($"Entered {gameObject.name}");
            LoadScene();

            //load all connected scenes
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }
        }
    }

    public void LoadScene()
    {
        if (!IsLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }
}
