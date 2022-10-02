using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class God : MonoBehaviour
{
    public static God Instance;

    public int seconds;
    public const int secondsMax = 120;

    public bool outside;

    public List<GameObject> InsideObjects;
    public List<GameObject> OutsideObjects;

    public SpriteRenderer blackScreen;

    public void Awake()
    {
        // destroy all false gods
        foreach (God god in FindObjectsOfType<God>())
        {
            if (god.gameObject != gameObject)
            {
                Destroy(god.gameObject);
            }
        }
        // assert self as the one true god
        Instance = this;
        blackScreen.color = Color.white;
        StartCoroutine(Fade(0f, 1f));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClockCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ClockCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            seconds++;
        }
    }

    public void Restart()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        yield return Fade(1f, 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator Fade(float target, float dur)
    {
        float initial = blackScreen.color.a;
        float time = 0f;
        while (time < dur)
        {
            blackScreen.color = new Color(1f, 1f, 1f, Mathf.Lerp(initial, target, time / dur));
            time += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = new Color(1f, 1f, 1f, target);
    }
}
