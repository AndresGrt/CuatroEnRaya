using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour {

    public bool color;
	// Use this for initialization
	void Start () {
        color = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Guarda()
    {
        color = true;
    }
}
