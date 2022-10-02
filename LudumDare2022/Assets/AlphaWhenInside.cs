using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaWhenInside : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, God.Instance.outside ? 1f : 0.3f);
    }
}
