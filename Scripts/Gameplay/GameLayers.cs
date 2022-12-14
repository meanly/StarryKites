using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectLayer;
    [SerializeField] LayerMask secondObjectLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask fovLayer;
    [SerializeField] LayerMask portalLayer;


    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public LayerMask SolidLayer {
        get => solidObjectLayer;
    }
    public LayerMask SecondObjectLayer {
        get => secondObjectLayer;
    }
    public LayerMask InteractableLayer {
        get => interactableLayer;
    }
    public LayerMask PlayerLayer {
        get => playerLayer;
    }

    public LayerMask FovLayer {
        get => fovLayer;
    }

    public LayerMask PortalLayer {
        get => portalLayer;
    }
    
    public LayerMask TriggerableLayers {
        get => portalLayer | fovLayer;
    }
    
}
