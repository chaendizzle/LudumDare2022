using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    List<Interactable> interactables;

    public GameObject EKey;
    public GameObject EText;

    public List<Sprite> textSprites;

    public List<GameObject> EnableAtStart;

    // Start is called before the first frame update
    void Start()
    {
        ReloadInteractables();
        foreach (var go in EnableAtStart)
        {
            go.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Interactable selected = null;
        foreach (Interactable pi in interactables)
        {
            if (pi != null && pi.CanInteract(gameObject))
            {
                selected = pi;
                EText.GetComponent<SpriteRenderer>().sprite = textSprites[(int)pi.Text];
            }
        }
        EKey.SetActive(selected != null);
        EText.SetActive(selected != null);

        if (selected != null && Input.GetKeyDown(KeyCode.E))
        {
            selected.Interact();
        }
    }

    public void ReloadInteractables()
    {
        interactables = new List<Interactable>();
        foreach (var mb in FindObjectsOfType<MonoBehaviour>())
        {
            Interactable pi = mb as Interactable;
            if (pi != null)
            {
                interactables.Add(pi);
            }
        }
    }
}

public interface Interactable
{
    void Interact();
    bool CanInteract(GameObject player);
    InteractableTypes Text { get; }
}

public enum InteractableTypes
{
    Enter, Exit, Smash, Equip, Press, ItsBroken
}