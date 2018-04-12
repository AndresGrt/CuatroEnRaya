using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid2D : MonoBehaviour
{

    public int width;//ancho de la matriz
    public int height;//alto de la matriz
    public GameObject puzzlePiece;//esfera que va ainstanciar
    private GameObject[,] grid;//array bidimencional para guardar las esferas
    public Bola[,] bola;//array para guardar la clase bola de las esferas
    public int contaGana;//contador entero para saver si gana o no al llegar a cierta cantidad
    public Text iniciar;//texto del canvas que se usa para iniciar la partida

    public bool suiche;//boleano que verifica si es player 1 o player 2
    public bool reinicio;//boleano para usarse cuando alguien gana todo se pause para empezar la nueva partida

    void Start()
    {
        suiche = false;//inicializa suiche en falso

        grid = new GameObject[width, height];//grid es igual a la matris de objetos con el ancho y el alto
        for (int x = 0; x < width; x++)//for para el ancho
        {
            for (int y = 0; y < height; y++)//for para el alto
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;//instancie las esferas
                Vector3 position = new Vector3(x, y, 0);//las posiciones en que se van a istanciar
                go.transform.position = position;//le da las posiciones a las esferas
                go.AddComponent(typeof(Bola));//les agrega la clase bola
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);//pinta todas las esferas en negro
                grid[x, y] = go;//se guarda go en la matriz
            }
        }

        bola = new Bola[width, height];//bola es igual a la matriz de clases en ancho y alto
        for (int a = 0; a < width; a++)//for para el ancho
        {
            for (int b = 0; b < height; b++)//for para el alto
            {
                bola[a, b] = grid[a, b].GetComponent<Bola>();//se guardan las clases en las mismas posiciones de las esferas
            }
        }


    }
    void Update()
    {

        if (!reinicio)//si reinicio es falso
        {
            for (int x = 0; x < width; x++)//for que recorre el ancho de la matriz
            {
                for (int y = 0; y < height; y++)//for que recorre el alto de la matriz
                {
                    GameObject go = grid[x, y];
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    bola[x, y].foco = false;
                    iniciar.gameObject.SetActive(true);
                    if (suiche)
                    {
                        suiche = false;
                    }
                    else if (!suiche)
                    {
                        suiche = true;
                    }
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                reinicio = true;
                iniciar.gameObject.SetActive(false);
            }
            return;
        }
        else if (reinicio)
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CambioTrue(mPosition);
        }

    }
    //para evaluar si es player1 en juego o player2 en juego
    void CambioTrue(Vector3 position)
    {
        int x = (int)(position.x + .4f);
        int y = (int)(position.y + .4f);

        if (suiche)
        {
            Player2(x, y);
        }
        if (!suiche)
        {
            Player1(x, y);
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

                Verifica(x, y);
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
                Verifica(x, y);
            }
        }
    }


    public bool inicia = false;

    void Verifica(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];
            for (int c = 0; c < width; c++)
            {
                if (grid[x, y].GetComponent<Renderer>().material.color == grid[c, y].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                {
                    contaGana++;
                }
                else
                    contaGana = 0;
                if (contaGana == 4)
                {
                    contaGana = 0;
                    reinicio = false;
                    if (!suiche)
                    {
                        Debug.Log("Gana Jugador 1");
                    }
                    else if (suiche)
                    {
                        Debug.Log("Gana Jugador 2");
                    }
                }
            }

            for (int h = 0; h < height; h++)
            {
                if (grid[x, y].GetComponent<Renderer>().material.color == grid[x, h].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                {
                    contaGana++;
                }
                else
                    contaGana = 0;
                if (contaGana == 4)
                {
                    contaGana = 0;
                    reinicio = false;
                    if (!suiche)
                    {
                        Debug.Log("Gana Jugador 1");
                    }
                    else if (suiche)
                    {
                        Debug.Log("Gana Jugador 2");
                    }
                }
            }

            for (int c = -width; c < width; c++)
            {
                if (x + c >= 0 && y + c >= 0 && x + c < width && y + c < height)
                {
                    if (grid[x, y].GetComponent<Renderer>().material.color == grid[x + c, y + c].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                    {
                        contaGana++;
                    }
                    else
                        contaGana = 0;
                    if (contaGana == 4)
                    {
                        contaGana = 0;
                        reinicio = false;
                        if (!suiche)
                        {
                            Debug.Log("Gana Jugador 1");
                        }
                        else if (suiche)
                        {
                            Debug.Log("Gana Jugador 2");
                        }
                    }
                }
            }
            for (int c = -width; c < width; c++)
            {
                if (x - c >= 0 && y + c >= 0 && x - c < width && y + c < height)
                {
                    if (grid[x, y].GetComponent<Renderer>().material.color == grid[x - c, y + c].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                    {
                        contaGana++;
                    }
                    else
                        contaGana = 0;
                    if (contaGana == 4)
                    {
                        contaGana = 0;
                        reinicio = false;
                        if (!suiche)
                        {
                            Debug.Log("Gana Jugador 1");
                        }
                        else if (suiche)
                        {
                            Debug.Log("Gana Jugador 2");
                        }
                    }
                }
            }

        }

    }
}