using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//teleports player to a different location without switching scenes
public class LocationPortal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;

    PlayerController player;


    public void OnPlayerTriggered(PlayerController player)
    {
        player.IsRunning = false;
        this.player = player;
        Debug.Log("Player entered the Portal");
        StartCoroutine(Teleport());
    }

    Fader fader;
    private void Start() {  
        fader =  FindObjectOfType<Fader>();
    }

    IEnumerator Teleport()
    {
        GameController.Instance.PauseGame(true);
        yield return fader.FadeIn(0.5f);

        var destPortal = FindObjectsOfType<LocationPortal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.Character.SetPositionandSnapToTile(destPortal.SpawnPoint.position);

        //end
        yield return fader.FadeOut(0.5f);
        
        GameController.Instance.PauseGame(false);
    }

    public Transform SpawnPoint => spawnPoint;
}
