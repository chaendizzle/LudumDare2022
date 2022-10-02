using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WarnLight : MonoBehaviour
{
    Light2D lgt;
    AudioSource audioSource;

    bool blink;

    // Start is called before the first frame update
    void Start()
    {
        lgt = GetComponent<Light2D>();
        audioSource = GetComponent<AudioSource>();
    }

    int prev;

    // Update is called once per frame
    void Update()
    {
        if (lgt != null && God.Instance.seconds > God.secondsMax - 40)
        {
            lgt.color = new Color(lgt.color.r, lgt.color.g, lgt.color.b, blink ? 0.5f : 1f);
        }
        if (prev != God.Instance.seconds)
        {
            blink = !blink;
            if (God.Instance.seconds > God.secondsMax - 40)
            {
                audioSource.Play();
            }
        }
        prev = God.Instance.seconds;
    }
}
