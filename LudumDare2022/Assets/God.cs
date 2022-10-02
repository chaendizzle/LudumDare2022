using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class God : MonoBehaviour
{
    public static God Instance;

    public int VisibleSeconds;
    public int seconds;
    public const int secondsMax = 120;

    public bool outside;

    public List<GameObject> InsideObjects;
    public List<GameObject> OutsideObjects;

    public SpriteRenderer blackScreen;

    public float Brightness;

    public GameObject ExplosionPrefab;
    public float ExplosionEndWidth;
    public float ExplosionEndHeight;

    AudioSource audioSource;
    public AudioClip ExplosionSFX;

    bool explosionStarted;

    public bool Endgame = false;
    public bool Parachute = false;

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
        blackScreen.color = new Color(Brightness, Brightness, Brightness);
        StartCoroutine(Fade(0f, 1f));
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ClockCoroutine());
    }

    public void StartEndgame()
    {
        Endgame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds >= secondsMax && !explosionStarted)
        {
            ExplosionRestart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator ClockCoroutine()
    {
        while (seconds < secondsMax)
        {
            yield return new WaitForSeconds(1f);
            seconds++;
            if (!Endgame)
            {
                VisibleSeconds = seconds;
            }
        }
    }

    public void Restart()
    {
        StartCoroutine(RestartGameCoroutine());
    }
    public void ExplosionRestart()
    {
        explosionStarted = true;
        StartCoroutine(Explosions());
        StartCoroutine(RestartGameCoroutine(0.7f));
    }

    IEnumerator RestartGameCoroutine(float wait=0f)
    {
        yield return new WaitForSeconds(wait);
        yield return Fade(1f, 1f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator Fade(float target, float dur)
    {
        float initial = blackScreen.color.a;
        float time = 0f;
        while (time < dur)
        {
            blackScreen.color = new Color(Brightness, Brightness, Brightness, Mathf.Lerp(initial, target, time / dur));
            time += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = new Color(Brightness, Brightness, Brightness, target);
    }
    IEnumerator Explosions()
    {
        int num = 125;
        float ymin = Camera.main.transform.position.y - ExplosionEndHeight * 0.5f;
        float ymax = Camera.main.transform.position.y + ExplosionEndHeight * 0.5f;
        float xmin = Camera.main.transform.position.x - ExplosionEndWidth * 0.5f;
        float xmax = Camera.main.transform.position.x + ExplosionEndWidth * 0.5f;
        for (int i = 0; i < num; i++)
        {
            float y = Mathf.Lerp(ymin, ymax, (float)i / (num - 1));
            Instantiate(ExplosionPrefab, new Vector3(Random.Range(xmin, xmax), y, 0f), Quaternion.identity);
            if (i % 20 == 0)
            {
                audioSource.PlayOneShot(ExplosionSFX);
            }
            yield return null;
        }
    }

    public IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Brightness = 0f;
        yield return Fade(1f, 1f);
        SceneManager.LoadScene("Ending");
    }
}
