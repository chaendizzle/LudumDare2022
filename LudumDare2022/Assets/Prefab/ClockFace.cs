using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFace : MonoBehaviour
{
    AudioSource audioSource;
    int prev;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int hour = (God.Instance.seconds / 10) % 10;
        if (prev != hour && God.Instance.seconds < God.secondsMax)
        {
            audioSource.PlayOneShot(audioSource.clip, 3f);
        }
        // HourTen.sprite = NumberSprites[(seconds / 100) % 10];
        // HourOne.sprite = NumberSprites[(seconds / 10) % 10];
        // SecondOne.sprite = NumberSprites[seconds % 10];
        prev = hour;
    }
}
