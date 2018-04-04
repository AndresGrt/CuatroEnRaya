
//librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Doc_24 : MonoBehaviour {

//buleano para activar las esferas
    public bool activar;
    //buleano para cambiar de color
    public bool cambiar;

//enteros para valores aleatorios para filas
    int rangoA;
    //entero para valores aleatorias para columnas
    int rangoB;

//para dar coloeres a las esferas
    int colorear;

// matris de las esferas
    public GameObject[,] matrisR;

//funcion usada para los checkbox para dar paso a las demas funciones que se neesiten
    public void Update()
    {
        //un if para el buleano activar y llamar la funcion grupo
        if (activar)
        {
            //buleano activar lo hago falso
            activar = false;
            //llamo la funcion
            Grupo();
        }
        //un if para el buleano cambiar y llamar la funcion buscador
        if (cambiar)
        {
            //buleano cambiar lo hago falso
            cambiar = false;
            //llamo la funcion
            Buscador();
        }
    }

//funcion para crear la matris
    public void Grupo()
    {
        //rangoA se le asigna valor aleatoria
        rangoA = (Random.Range(3, 12));
        //rangoB se le asigna valor aleatoria
        rangoB = (Random.Range(3, 12));
        
        //asigno las variables aleatorias a la matris
        matrisR = new GameObject[rangoA,rangoB];


//los for para istanciar las esferas en pantalla cuando el bool de activar sea verdadera
        for (int i = 0; i < rangoA ; i++)//filas
        {
            for (int j = 0; j < rangoB; j++)//columnas
            {

                GameObject bola = GameObject.CreatePrimitive(PrimitiveType.Sphere);//creando la primitiva de las esferas
                bola.transform.position = new Vector3(i * 2.0f, j * 2.0f, 0f);
               
               //doi un aleatorio de colores para las esferas y uso un swuitch para dar paso al aleatorio
                colorear = (Random.Range(1, 5));
                //switch para la seleccion del color
                switch (colorear)
                {
                    //rojo si es el valor es 1
                    case 1:
                        bola.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    //rojo si es el valor es 2
                    case 2:
                        bola.GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                    //rojo si es el valor es 3
                    case 3:
                        bola.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    //rojo si es el valor es 4
                    case 4:
                        bola.GetComponent<Renderer>().material.color = Color.green;
                        break;
                }

                
                matrisR[i,j] = bola;//asigno las esferas a la matris
            }
        } 
    }

//la funcion que activa el buleano cambiar que identifica las esferas que se van a volver negras de manera logica
    public void Buscador(){

        for(int i = 0; i < rangoA; i++)//filas
        {
            for (int j = 0; j< rangoB; j++)//columnas
            {
                if (i < rangoA-1)
                {
                    if (matrisR[i,j].GetComponent<Renderer>().material.color == matrisR[i+1,j].GetComponent<Renderer>().material.color)//evalua si cumple la condicion de se la siguiente es igual
                    {
                        matrisR[i,j].GetComponent<Renderer>().material.color = Color.black;//hago negra la primer esfera
                        matrisR[i+1,j].GetComponent<Renderer>().material.color = Color.black;//hago negra la esfera que le sige a la primera
                    }
                }
            }
        }
    }
}
