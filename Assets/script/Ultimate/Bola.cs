using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour {

    public bool foco;
	// Use this for initialization
	void Start () {
        foco = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //funcion guarda con un buleano foco que se hace verdadero cuando la clase grid lo llama
    public void Guarda()
    {
        foco = true;
    }
}
