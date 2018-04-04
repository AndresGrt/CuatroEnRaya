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
    public void Guarda()
    {
        foco = true;
        //GetComponent<Renderer>().material.color = Color.green;
    }
}
