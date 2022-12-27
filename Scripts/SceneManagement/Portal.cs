using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;

    public void OnPlayerTriggered(PlayerController player)
    {
        Debug.Log("Player entered the Portal");
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
