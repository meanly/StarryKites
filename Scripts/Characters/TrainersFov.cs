using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainersFov : MonoBehaviour, IPlayerTriggerable
{
    public void OnPlayerTriggered(PlayerController player)
    {
        GameController.Instance.OnEnterTrainersView(GetComponentInParent<TrainerController>());
    }
}
