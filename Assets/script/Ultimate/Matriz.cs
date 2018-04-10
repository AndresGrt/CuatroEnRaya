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
    void Verifica(int x , int y )
    {
        //if (x >= 0 && y >= 0 && x < width && y < height )
        //{
        //    GameObject go = grid[x, y];
        //    Color colorP1 = go.GetComponent<Renderer>().material.color;
        //    Color colorP2 = go.GetComponent<Renderer>().material.color;

        //    if (colorP1 == Color.red)
        //    {

        //        for (int _x = 0; _x < width; _x++)
        //        {

        //            Color orientaH = grid[_x, y].GetComponent<Renderer>().material.color;

        //            if (colorP1 == orientaH && bola[x, y].foco == true)
        //            {
        //                contaGana ++;
        //            }

        //            else
        //                contaGana = 0;

        //            if (contaGana == 4)
        //            {
        //                print("gana");
        //            }
        //        }
        //    }
        //}

        //for (int _x = 0; _x < width; _x++)
        //{
        //    for (int _y = 0; _y < height; _y++)
        //    {
        //        if (_x < width - 1)//
        //        {
        //            if (grid[_x, _y].GetComponent<Renderer>().material.color == grid[_x + 1, _y].GetComponent<Renderer>().material.color
        //                && grid[_x, _y].GetComponent<Renderer>().material.color != Color.black && bola[_x, _y].foco == true)
        //            {
        //                grid[_x, _y].GetComponent<Renderer>().material.color = Color.green;//hago negra la primer esfera
        //                grid[_x + 1, _y].GetComponent<Renderer>().material.color = Color.green;//hago negra la esfera que le sige a la primera
        //            }
        //        }
        //    }
        //}

        //for (int _x = 0; _x < width; _x++)
        //{
        //    for (int _y = 0; _y < height; _y++)
        //    {
        //        for (int c = _x + 1; c < width; c++)
        //        {
        //            if (grid[_x, _y].GetComponent<Renderer>().material.color == grid[c, _y].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
        //            {
        //                if (Input.GetButtonDown("Fire1"))
        //                {
        //                    inicia = true;
        //                }
        //            }
        //        }
        //    }
        //}
        //if (inicia)
        //{
        //    contaGana++;
        //    print("entra");
        //    inicia = false;
        //}
        //else
        //    contaGana = 0;

        //if (contaGana == 4)
        //{
        //    print("gana");
        //}
    }
}
