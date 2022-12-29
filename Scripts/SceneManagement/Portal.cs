using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;

    PlayerController player;


    public void OnPlayerTriggered(PlayerController player)
    {
        player.IsRunning = false;
        this.player = player;
        Debug.Log("Player entered the Portal");
        StartCoroutine(SwitchScene());
    }

    Fader fader;
    private void Start() {  
        fader =  FindObjectOfType<Fader>();
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);
        //start of script and fading

        GameController.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.Character.SetPositionandSnapToTile(destPortal.SpawnPoint.position);

        //end
        yield return fader.FadeOut(0.5f);
        
        GameController.Instance.PauseGame(false);
    
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}

public enum DestinationIdentifier {A, B, C, D, E,}
