using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour {

    public int width;
    public int height;
    public GameObject puzzlePiece;
    private GameObject[,] grid;
    public Bola[,] bola;
    public int contaGana;
    
    public bool suiche;


    void Start () {
        suiche = false;

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

        bola = new Bola[width,height];
        for(int a = 0; a < width; a++)
        {
            for (int b = 0; b < height; b++)
            {
                bola[a, b] = grid[a, b].GetComponent<Bola>();
            }
        }


    }
	void Update()
	{
		
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CambioTrue(mPosition);
	}
    //para evaluar si es player1 en juego o player2 en juego
    void CambioTrue (Vector3 position)
    {
        int x = (int)(position.x + .4f);
        int y = (int)(position.y + .4f);
        if (suiche)
        {
            Player2(x,y);
        }
        if (!suiche)
        {
            Player1(x,y);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (suiche == true)
            {
                //player1
                suiche = false;
            }
            else 
            {
                //Player 2
                suiche = true;
            }
        }

        //Verifica();
    }
    //cuando player1 esta en juego
    public void Player1(int x, int y)
    {
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {

                GameObject go = grid[_x, _y];
                if (go.GetComponent<Renderer>().material.color == Color.red && bola[_x, _y].foco == false)
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
               
            }
        }

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bola[x, y].Guarda();
            }

            if (go.GetComponent<Renderer>().material.color == Color.black)
            {
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                
                Verifica(x,y);
            }
        }

        
    }
    //cuando player2 esta en juego
    public void Player2(int x, int y)
    {
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {
                GameObject go = grid[_x, _y];

                if (go.GetComponent<Renderer>().material.color == Color.blue && bola[_x, _y].foco == false)
                {
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                }

            }
        }

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bola[x, y].Guarda();
            }
            if (go.GetComponent<Renderer>().material.color == Color.black)
            {
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                Verifica(x,y);
            }
        }
    }
    public bool inicia = false;
    void Verifica(int x, int y)
    {
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {
                for (int c = _x+1; c < width; c++)
                {
                    if (grid[_x, _y].GetComponent<Renderer>().material.color == grid[c, _y].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            inicia = true;
                        }
                    }
                }
            }
        }
        if (inicia)
        {
            contaGana++;
            print("entra");
            inicia = false;
        }
        else
            contaGana = 0;

        if (contaGana == 4)
        {
            print("gana");
        }
    }
}
