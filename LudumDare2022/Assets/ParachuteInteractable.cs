using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteInteractable : MonoBehaviour, Interactable
{
    public InteractableTypes Text => InteractableTypes.Equip;
    bool interacted;

    public float Dist;
    public GameObject ToParent;
    public Vector2 ParentPos;

    public bool CanInteract(GameObject player)
    {
        return !interacted && Vector2.Distance(player.transform.position, transform.position) < Dist;
    }

    public void Interact()
    {
        interacted = true;
        transform.parent = ToParent.transform;
        transform.localPosition = ParentPos;
        God.Instance.Parachute = true;
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
