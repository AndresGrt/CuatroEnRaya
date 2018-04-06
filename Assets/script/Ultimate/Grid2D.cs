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
    public int contaGana2;


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
        if (suiche1)
        {
            Player2(x,y);
        }
        if (!suiche1)
        {
            Player1(x,y);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (suiche1 == true)
            {
                //player1
                suiche1 = false;
            }
            else 
            {
                //Player 2
                suiche1 = true;
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
    public int suma;
    public int suma2;
    void Verifica(int x, int y)
    {
        
        if (x >= 0 && y >= 0 && x < width && y < height )
        {
            GameObject go = grid[x, y];
            Color colorP1 = go.GetComponent<Renderer>().material.color;
            Color colorP2 = go.GetComponent<Renderer>().material.color;

            if (colorP1 == Color.red)
            {

                for (int _x = 0; _x < width; _x++)
                {
                    
                    Color orientaH = grid[x, y].GetComponent<Renderer>().material.color;
                    contaGana = 0;
                    if (colorP2 == orientaH && bola[x, y].foco == true)
                    {
                        contaGana = contaGana + 1;
                        suma += contaGana;
                    }
                        
                    else
                        contaGana = 0;

                    if (suma == 40)
                    {
                        print("gana");
                    }
                }
            }

            if (colorP2 == Color.blue)
            {

                for (int _x = 0; _x < width; _x++)
                {

                    Color orientaH = grid[x, y].GetComponent<Renderer>().material.color;
                    contaGana2 = 0;
                    if (colorP2 == orientaH && bola[x, y].foco == true)
                    {
                        contaGana2 = contaGana2 + 1;
                        suma2 += contaGana2;
                    }

                    else
                        contaGana2 = 0;

                    if (suma2 == 40)
                    {
                        print("gana");
                    }
                }
            }

        }

        //    for (int _x = 0; _x < width; _x++)
        //    {
        //        for (int _y = 0; _y < height; _y++)
        //        {
        //            if (_x < width - 1)//aun no entiendo por que me sale fuera de rango
        //            {
        //                if (grid[_x, _y].GetComponent<Renderer>().material.color == grid[_x + 1, _y].GetComponent<Renderer>().material.color
        //                    && grid[_x, _y].GetComponent<Renderer>().material.color != Color.black && bola[_x, _y].foco == true)
        //                {
        //                    grid[_x, _y].GetComponent<Renderer>().material.color = Color.green;//hago negra la primer esfera
        //                    grid[_x + 1, _y].GetComponent<Renderer>().material.color = Color.green;//hago negra la esfera que le sige a la primera
        //                }
        //            }
        //        }
        //    }
    }
}
