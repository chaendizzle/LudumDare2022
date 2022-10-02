using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToPlay : MonoBehaviour
{
    bool clicked;

    public SpriteRenderer blackScreen;
    public float Brightness;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return Fade(1f, 1f);
        SceneManager.LoadScene("GameScene");
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
}
