using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightTime : MonoBehaviour
{
    public float StartIntensity;
    public float EndIntensity;

    Light2D lgt;

    // Start is called before the first frame update
    void Start()
    {
        lgt = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lgt.intensity = Mathf.Lerp(StartIntensity, EndIntensity, (float)God.Instance.seconds / God.secondsMax);
    }
}
