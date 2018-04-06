using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matriz : MonoBehaviour {

    public int width;
    public int height;
    public GameObject puzzlePiece;
    private GameObject[,] grid;
    public bool suiche;

    void Start () {

        grid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;
                Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                grid[x, y] = go;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CambioTrue(mPosition);
    }

    void CambioTrue(Vector3 position)
    {
        int x = (int)(position.x + .4f);
        int y = (int)(position.y + .4f);

        if (suiche)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                GameObject go = grid[x, y];

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (go.GetComponent<Renderer>().material.color == Color.black)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    }
                    suiche = false;
                }
            }
        }
        if (!suiche)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                GameObject go = grid[x, y];
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (go.GetComponent<Renderer>().material.color == Color.black)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                    suiche = true;
                }
            }
        }
    }
}
