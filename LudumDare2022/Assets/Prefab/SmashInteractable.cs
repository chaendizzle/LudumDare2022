using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashInteractable : MonoBehaviour, Interactable
{
    public List<GameObject> ToDestroy;
    public List<GameObject> SpawnExplosion;
    public GameObject ExplosionPrefab;
    public List<GameObject> Activate;

    public InteractableTypes Text => InteractableTypes.Smash;
    bool interacted;

    AudioSource audioSource;

    public float Dist;

    public bool CanInteract(GameObject player)
    {
        return !interacted && Vector2.Distance(player.transform.position, transform.position) < Dist;
    }

    public void Interact()
    {
        foreach (var se in SpawnExplosion)
        {
            Instantiate(ExplosionPrefab, se.transform.position, Quaternion.identity);
        }
        audioSource.Play();
        foreach (var go in ToDestroy)
        {
            Destroy(go);
        }
        foreach (var go in Activate)
        {
            go.SetActive(true);
        }
        God.Instance.StartEndgame();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
