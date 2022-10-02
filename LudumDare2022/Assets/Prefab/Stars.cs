using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    List<GameObject> starTiles = new List<GameObject>();
    public int Width = 6;
    public int Height = 6;
    public float StarTileSize = 16;

    public float Speed;
    public float Acc;

    public GameObject StarTile;
    public Color BGColor;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                GameObject go = Instantiate(StarTile, new Vector3(StarTileSize * (-Width * 0.5f + i), StarTileSize * (-Height * 0.5f + j), 0f), Quaternion.identity);
                starTiles.Add(go);
                go.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in starTiles)
        {
            go.transform.position += Vector3.up * Speed * Time.deltaTime;
            if (go.transform.position.y > StarTileSize * 0.5f * Height)
            {
                go.transform.position -= Vector3.up * StarTileSize * Height;
                if (God.Instance != null)
                {
                    go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Max(0.15f, 1 - 2f * ((float)God.Instance.seconds / God.secondsMax)));
                }
            }
        }
        if (God.Instance != null)
        {
            Speed += Acc * Time.deltaTime / 120f;
            Color c = ((float)God.Instance.seconds / God.secondsMax) * BGColor;
            Camera.main.backgroundColor = new Color(c.r, c.g, c.b, 1f);
        }
    }
}
