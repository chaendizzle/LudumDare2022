using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMonitor : MonoBehaviour
{
    public SpriteRenderer HourTen;
    public SpriteRenderer HourOne;
    public SpriteRenderer SecondOne;

    public List<Sprite> NumberSprites;

    int prev;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = God.secondsMax - God.Instance.seconds;
        if (prev != seconds && God.Instance.seconds < God.secondsMax)
        {
            audioSource.Play();
        }
        HourTen.sprite = NumberSprites[(seconds / 100) % 10];
        HourOne.sprite = NumberSprites[(seconds / 10) % 10];
        SecondOne.sprite = NumberSprites[seconds % 10];
        prev = seconds;
    }
}
