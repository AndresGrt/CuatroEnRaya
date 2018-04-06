using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forinline : MonoBehaviour {


    public int Altura;
    public int Ancho;  
    public GameObject Pieza; 
    private GameObject[,] matriz;

    public bool Click;  
    int contador; 


    // Use this for initialization
    void Start () {
        matriz = new GameObject[Ancho, Altura];
        for (int x = 0; x < Ancho; x++)  
        {
            for (int y = 0; y < Altura; y++) 
            {
                GameObject go = GameObject.Instantiate(Pieza) as GameObject; 
                Vector3 position = new Vector3(x, y, 0);
                go.transform.position = position;  
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                matriz[x, y] = go;
            }

        }
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
        UpdatePickedPiece(mPosition); 


    }

   

    void UpdatePickedPiece(Vector3 position)
    {
        int x = (int)(position.x + 0.5f); 
        int y = (int)(position.y + 0.5f);

       

        if (Click) 
        {
            
            if (x >= 0 && y >= 0 && x < Ancho && y < Altura )
            {
                GameObject go = matriz[x, y];
                if (go.GetComponent<Renderer>().material.color == Color.white)  
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                        Click = false;
                          Comprobar(position); 
                    }
                }
            }
        }
        if (!Click)
        {
           
            if (x >= 0 && y >= 0 && x < Ancho && y < Altura)
            {
                GameObject go = matriz[x, y];
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (go.GetComponent<Renderer>().material.color == Color.white)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        Click = true;
                         Comprobar(position); 
                    }
                }
            }
        }

    }

    void Comprobar(Vector3 position)
    {
        int x = (int)(position.x + 0.5f);
        int y = (int)(position.y + 0.5f);


        if (x >= 0 && y >= 0 && x < Ancho && y < Altura)
        {
            GameObject go = matriz[x, y];
            Color blue = go.GetComponent<Renderer>().material.color;
            Color black = go.GetComponent<Renderer>().material.color;

            if (blue == Color.blue)
            {
                for (int i = 0; i < Ancho; i++)
                {
                    Color horizontal = matriz[i, y].GetComponent<Renderer>().material.color;

                    if (blue == horizontal)
                    {
                        contador++;
                    }
                    else
                        contador = 0;

                    if (contador == 4)
                    {
                    
                        //Score.vidasp2 -= 1; 
                        contador = 0;
                    }
                }

                for (int i = 0; i < Altura; i++)
                {
                    Color vertical = matriz[x, i].GetComponent<Renderer>().material.color;

                    if (blue == vertical)
                    {
                        contador++;
                    }
                    else
                        contador = 0;

                    if (contador == 4)
                    {
                        //Score.vidasp2 -= 1; 
                        contador = 0;
                        Borrar();
                    }
                }

                for (int i = -Ancho; i < Ancho; i++)
                {

                    if (x + i >= 0 && y + i >= 0 && x + i < Ancho && y + i < Altura)
                    {
                        Color diagonal = matriz[x + i, y + i].GetComponent<Renderer>().material.color;

                        if (blue == diagonal)
                        {
                            contador++;
                        }
                        else
                            contador = 0;

                        if (contador == 4)
                        {
                            //  Score.vidasp2 -= 1; 
                            contador = 0;
                            Borrar();
                        }
                    }
                }
                for (int i = -Ancho; i < Ancho; i++)
                {

                    if (x - i >= 0 && y + i >= 0 && x - i < Ancho && y + i < Altura)
                    {
                        Color diagonal = matriz[x - i, y + i].GetComponent<Renderer>().material.color;

                        if (blue == diagonal)
                        {
                            contador++;
                        }
                        else
                            contador = 0;

                        if (contador == 4)
                        {
                            //  Score.vidasp2 -= 1;
                            contador = 0;
                            Borrar();
                        }
                    }
                }
            }
            if (black == Color.black)
            {

                for (int i = -Ancho; i < Ancho; i++)
                {

                    if (x - i >= 0 && y + i >= 0 && x - i < Ancho && y + i < Altura)
                    {
                        Color diagonal = matriz[x - i, y + i].GetComponent<Renderer>().material.color;

                        if (black == diagonal)
                        {
                            contador++;
                        }
                        else
                            contador = 0;

                        if (contador == 4)
                        {
                            //  Score.vidasP1 -= 1; 
                            contador = 0;
                            Borrar();
                        }
                    }
                }
                for (int i = -Ancho; i < Ancho; i++)
                {

                    if (x + i >= 0 && y + i >= 0 && x + i < Ancho && y + i < Altura)
                    {
                        Color diagonal = matriz[x + i, y + i].GetComponent<Renderer>().material.color;

                        if (black == diagonal)
                        {
                            contador++;
                        }
                        else
                            contador = 0;

                        if (contador == 4)
                        {
                            //  Score.vidasP1 -= 1; 
                            contador = 0;
                            Borrar();
                        }
                    }
                }
                for (int i = 0; i < Ancho; i++)
                {
                    Color horizontal = matriz[i, y].GetComponent<Renderer>().material.color;

                    if (black == horizontal)
                    {
                        contador++;
                    }
                    else
                        contador = 0;

                    if (contador == 4)
                    {
                        //  Score.vidasP1 -= 1; 
                        contador = 0;
                        Borrar();

                    }

                }

                for (int i = 0; i < Altura; i++)
                {
                    Color vertical = matriz[x, i].GetComponent<Renderer>().material.color;

                    if (black == vertical)
                    {
                        contador++;
                    }
                    else
                        contador = 0;

                    if (contador == 4)
                    {
                        //  Score.vidasP1 -= 1; 
                        contador = 0;
                        Borrar();
                    }
                }

            }

        }

    }

    void Borrar()
    {
        for (int x = 0; x < Ancho; x++)
        {
            for (int y = 0; y < Altura; y++)
            {

                GameObject go = matriz[x, y];
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }

        }
    }

    void Destruir() 
    {
        for (int x = 0; x < Ancho; x++) 
        {
            for (int y = 0; y < Altura; y++) 
            {

                GameObject go = matriz[x, y];
                Destroy(go.gameObject); 
            }

        }
    }


}
