using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArrowInteractable : MonoBehaviour, Interactable
{
    Coroutine elevatorCoroutine;
    public GameObject ElevatorTarget;
    public GameObject ElevatorDoor1;
    public GameObject ElevatorDoor2;
    public GameObject ElevatorPlatform;

    enum State
    {
        Start, Pressed, Stopped, Broken, Smashed
    }
    State state = State.Start;

    public InteractableTypes Text => state != State.Broken ? InteractableTypes.Press : InteractableTypes.ItsBroken;

    AudioSource audioSource;

    public AudioClip DingSFX;
    public AudioClip ElevDoorSFX;
    public AudioClip ElevSFX;

    public float Dist;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        switch(state)
        {
            case State.Start:
                StartCoroutine(ElevatorCoroutine());
                break;
            case State.Stopped:
                state = State.Broken;
                break;
        }
    }

    public bool CanInteract(GameObject player)
    {
        return state != State.Pressed && Vector2.Distance(player.transform.position, transform.position) < Dist;
    }

    IEnumerator ElevatorCoroutine()
    {
        state = State.Pressed;
        audioSource.PlayOneShot(DingSFX);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(ElevDoorSFX);
        ElevatorDoor2.transform.position = new Vector3(ElevatorDoor2.transform.position.x, ElevatorDoor2.transform.position.y - 4f, ElevatorDoor2.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(ElevSFX);
        float time = 0f;
        float dur = 5f;
        float speed = (ElevatorTarget.transform.position.y - ElevatorPlatform.transform.position.y) / dur;
        while (time < dur)
        {
            ElevatorPlatform.transform.position = new Vector3(ElevatorPlatform.transform.position.x, Mathf.MoveTowards(ElevatorPlatform.transform.position.y, ElevatorTarget.transform.position.y, speed * Time.deltaTime), ElevatorPlatform.transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
        ElevatorPlatform.transform.position = new Vector3(ElevatorPlatform.transform.position.x, ElevatorTarget.transform.position.y, ElevatorPlatform.transform.position.z);
        audioSource.PlayOneShot(DingSFX);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(ElevDoorSFX);
        yield return new WaitForSeconds(0.5f);
    }
}
