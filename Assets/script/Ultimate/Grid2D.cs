using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour {

	public int width;
	public int height;
	public GameObject puzzlePiece;
	private GameObject[,] grid;
    public Bola[] allGameObjects;
    public bool cambio;
    public bool suiche1;
    public bool suiche2;


    void Start () {
        suiche1 = false;
        suiche2 = true;

		grid = new GameObject[width,height];
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;
				Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;
                go.AddComponent(typeof(Bola));
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                grid[x,y] = go;
			}
		}


        //crea un ArrayList
        ArrayList aList = new ArrayList();
        //crea un array con todos los objetos de la escena que tengan la clase bola
        Bola[] allObjects = Bola.FindObjectsOfType<Bola>();
        //itera a traves de todos los objetos
        foreach (Bola o in allObjects)
        {
            //si hay un GameObject con la clase bola se añade a la lista
            Bola go = o as Bola;
            if (go != null)
            {
                aList.Add(go);
            }
        }
        //inicializamos la matriz allGameObjects
        allGameObjects = new Bola[aList.Count];
        //copiamos lista a la matriz
        aList.CopyTo(allGameObjects);

    }
	void Update()
	{
		
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CambioTrue(mPosition);
	}
    void CambioTrue (Vector3 position)
    {
        int x = (int)(position.x + .4f);
        int y = (int)(position.y + .4f);
        if (suiche1 == false)
        {
            for (int _x = 0; _x < width; _x++)
            {
                for (int _y = 0; _y < height; _y++)
                {
                    GameObject go = grid[_x, _y];
                    //go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    //if (go.GetComponent<Renderer>().material.color == Color.red && bola.color == false)
                    //{
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    //}
                    //else if (go.GetComponent<Renderer>().material.color == Color.blue)
                    //{
                    //    go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    //}

                }
            }

            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                GameObject go = grid[x, y];
                //if (cambio == false)
                //{
                if (go.GetComponent<Renderer>().material.color == Color.black)
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                //}
            }
        }
        if (suiche2 == false)
        {
            for (int _x = 0; _x < width; _x++)
            {
                for (int _y = 0; _y < height; _y++)
                {
                    GameObject go = grid[_x, _y];
                    //if (go.GetComponent<Renderer>().material.color == Color.blue && bola.color == false)
                    //{
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    //}
                    //else if (go.GetComponent<Renderer>().material.color == Color.red)
                    //{
                    //    go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    //}
                }
            }

            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                GameObject go = grid[x, y];
                //if (cambio == true)
                //{
                if (go.GetComponent<Renderer>().material.color == Color.black)
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                }
                //}
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (suiche1 == false)
            {
                suiche1 = true;
                suiche2 = false;
                //bola.Guarda();
            }
            else if (suiche2 == false)
            {
                suiche2 = true;
                suiche1 = false;
            }
        }
    }
}
