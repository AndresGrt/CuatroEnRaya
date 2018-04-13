using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid2D : MonoBehaviour
{

    public int width;//ancho de la matriz
    public int height;//alto de la matriz
    private int contaGana;//contador entero para saver si gana o no al llegar a cierta cantidad

    public GameObject puzzlePiece;//esfera que va ainstanciar
    private GameObject[,] grid;//array bidimencional para guardar las esferas

    public Bola[,] bola;//array para guardar la clase bola de las esferas
   
    public Text iniciar;//texto del canvas que se usa para iniciar la partida

    private bool suiche;//boleano que verifica si es player 1 o player 2
    private bool inicia = false;//el buleano inicia comienza en falso
    private bool reinicio;//boleano para usarse cuando alguien gana todo se pause para empezar la nueva partida

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

                    GameObject go = grid[x, y];//objeto go es igual a la matriz de las esferas
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);//pinta las esferas de la matriz de color negro
                    bola[x, y].foco = false;//el buleano de la clase bola es igual a falso
                    iniciar.gameObject.SetActive(true);//el texto iniciar se activa en la escena

                    ///este if es para que cuando un jugador
                    ///al empezar la otra partida 
                    ///empieze con el turno del otro jugador
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

            /// este if es para que cuando unda la tecla espacio
            /// reinicio sea verdadero y el texto de iniciar
            /// sea falso 
            if (Input.GetKey(KeyCode.Space))
            {
                reinicio = true;
                iniciar.gameObject.SetActive(false);
            }

            //un return para que el juego no continue hasta que se cumpla el if de boton espacio
            return;
        }
        else if (reinicio)//si el reinicio fuera verdadero que el vector mposicion tome la posicion del cursor en la matriz
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CambioTrue(mPosition);//llama a la funcion con la posicion segun el cursor
        }

    }
    //para evaluar si es player1 en juego o player2 en juego
    void CambioTrue(Vector3 position)
    {
        int x = (int)(position.x + .4f);
        int y = (int)(position.y + .4f);

        ///si el suiche es verdadero que llame la funcion del player 2
        ///y si el suiche es falso que llame la funcion del  player 1
        if (suiche)
        {
            Player2(x, y);
        }
        if (!suiche)
        {
            Player1(x, y);
        }

        ///si da clic derecho que evalue si el suiche ya es falso
        ///o verdadero, si el suiche es verdadero
        ///que sea falso y si el suiche es falso
        ///que sea verdadero
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
    }

    //cuando player1 esta en juego
    public void Player1(int x, int y)
    {
        //for que recorre toda la matriz
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {
                ///un objeto go que se hace igual a la matriz y un if que evalua
                ///si el objeto go una esfera es color rojo y la clase bola  es falsa
                ///que la esfera sea negra
                GameObject go = grid[_x, _y];
                if (go.GetComponent<Renderer>().material.color == Color.red && bola[_x, _y].foco == false)
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

            }
        }

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];

            ///si da clic derecho que llame la clase bola al buleano guarda 
            ///que es igual a verdadero
            if (Input.GetKeyDown(KeyCode.Mouse0))
                bola[x, y].Guarda();


            ///si la esfera es igual a color negro que haga roja 
            ///y llame a la funcion verifica
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
        //for que recorre toda la matriz
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {

                ///un objeto go que se hace igual a la matriz y un if que evalua
                ///si el objeto go (una esfera) es color azul y la clase bola  es falsa
                ///que la esfera sea negra
                GameObject go = grid[_x, _y];

                if (go.GetComponent<Renderer>().material.color == Color.blue && bola[_x, _y].foco == false)
                    go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

            }
        }

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];

            ///si da clic derecho que llame la clase bola al buleano guarda 
            ///que es igual a verdadero
            if (Input.GetKeyDown(KeyCode.Mouse0))
                bola[x, y].Guarda();


            ///si la esfera es igual a color negro que haga azul
            ///y llame a la funcion verifica
            if (go.GetComponent<Renderer>().material.color == Color.black)
            {
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                Verifica(x, y);
            }
        }
    }

    
    void Verifica(int x, int y)
    {
        //if qeu evalua toda la matriz
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            GameObject go = grid[x, y];
            //siclo for de enteri c hasta el ancho de la matriz
            for (int c = 0; c < width; c++)
            {
                ///este if compara si la esfera es igual a la que esta en la misma linea en horizontal
                if (grid[x, y].GetComponent<Renderer>().material.color == grid[c, y].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                    contaGana++;//si es igual el contador se suma en 1

                else
                    contaGana = 0;//si no es igual el contador vuelve a cero

                ///si en el contador es igual a 4 el contador es 0, reinicio es falso
                ///en el suiche evalua si gano el jugador 1 o el jugador 2
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

            //siclo for de enteri c hasta el alto de la matriz
            for (int h = 0; h < height; h++)
            {
                ///este if compara si la esfera es igual a la que esta en la misma linea en vertical
                if (grid[x, y].GetComponent<Renderer>().material.color == grid[x, h].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                    contaGana++;//si es igual el contador se suma en 1

                else
                    contaGana = 0;//si no es igual el contador vuelve a cero

                ///si en el contador es igual a 4 el contador es 0, reinicio es falso
                ///en el suiche evalua si gano el jugador 1 o el jugador 2
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

            //siclo for de enteroc que es ugual al ancho menos 1 hasta la ultima posicion de la matriz
            for (int c = -width; c < width; c++)
            {
                ///en este if evalua si X o Y cada uno mas C son menores del ancho o el alto
                ///evalua para la linea diagonal
                if (x + c >= 0 && y + c >= 0 && x + c < width && y + c < height)
                {
                    ///este if compara si la esfera es igual a la que esta 
                    ///en la misma linea en diagonal superior derecha e inferior izquierda
                    if (grid[x, y].GetComponent<Renderer>().material.color == grid[x + c, y + c].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                        contaGana++;//si es igual el contador se suma en 1

                    else
                        contaGana = 0;//si no es igual el contador vuelve a cero

                    ///si en el contador es igual a 4 el contador es 0, reinicio es falso
                    ///en el suiche evalua si gano el jugador 1 o el jugador 2
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

            //siclo for de enteroc que es ugual al ancho menos 1 hasta la ultima posicion de la matriz
            for (int c = -width; c < width; c++)
            {
                ///en este if evalua si X menos C y Y mas C son menores del ancho o el alto
                ///evalua para la linea diagonal
                if (x - c >= 0 && y + c >= 0 && x - c < width && y + c < height)
                {
                    ///este if compara si la esfera es igual a la que esta 
                    ///en la misma linea en diagonal superior izquierda e inferior derecha
                    if (grid[x, y].GetComponent<Renderer>().material.color == grid[x - c, y + c].GetComponent<Renderer>().material.color && bola[x, y].foco == true)
                        contaGana++;//si es igual el contador se suma en 1

                    else
                        contaGana = 0;//si no es igual el contador vuelve a cero

                    ///si en el contador es igual a 4 el contador es 0, reinicio es falso
                    ///en el suiche evalua si gano el jugador 1 o el jugador 2
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