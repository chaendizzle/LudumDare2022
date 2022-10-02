using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, Interactable
{
    public InteractableTypes Text => God.Instance.outside ? InteractableTypes.Enter : InteractableTypes.Exit;

    public float Dist;

    public void Interact()
    {
        God.Instance.outside = !God.Instance.outside;
        var activate = God.Instance.outside ? God.Instance.OutsideObjects : God.Instance.InsideObjects;
        var deactivate = God.Instance.outside ? God.Instance.InsideObjects : God.Instance.OutsideObjects;
        foreach (var go in activate)
        {
            go.SetActive(true);
        }
        foreach (var go in deactivate)
        {
            go.SetActive(false);
        }
    }
    public bool CanInteract(GameObject player)
    {
        return Vector2.Distance(player.transform.position, transform.position) < Dist;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
