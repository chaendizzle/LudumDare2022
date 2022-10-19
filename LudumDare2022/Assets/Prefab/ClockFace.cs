using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFace : MonoBehaviour
{
    AudioSource audioSource;
    int prev;
    int prevsec;

    public GameObject HourHand;
    public GameObject SecondHand;

    public AudioClip bong;
    public AudioClip tick;

    public CameraShake camShake;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = God.Instance.VisibleSeconds;
        int hour = (God.Instance.VisibleSeconds / 10) % 10;
        if (prev != hour && God.Instance.seconds < God.secondsMax)
        {
            audioSource.PlayOneShot(bong, 5f);
            camShake.ShakeCamera(1f, 0.75f);
        }
        if (prevsec != seconds && God.Instance.seconds < God.secondsMax)
        {
            audioSource.PlayOneShot(tick, 2f);
        }
        HourHand.transform.eulerAngles = new Vector3(0f, 0f, -30f * (seconds / 10));
        SecondHand.transform.eulerAngles = new Vector3(0f, 0f, -36f * (seconds % 10));
        prev = hour;
        prevsec = seconds;
    }
}
