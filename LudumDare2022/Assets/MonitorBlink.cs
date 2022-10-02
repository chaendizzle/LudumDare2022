using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MonitorBlink : MonoBehaviour
{
    SpriteRenderer sr;
    Light2D lgt;

    bool blink;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lgt = GetComponentInChildren<Light2D>();
        StartCoroutine(BlinkCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, blink ? 0.5f : 1f);
        if (lgt != null)
        {
            lgt.color = new Color(lgt.color.r, lgt.color.g, lgt.color.b, blink ? 0.5f : 1f);
        }
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.69f);
            blink = !blink;
        }
    }
}
