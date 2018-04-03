using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLists : MonoBehaviour {

    // Guarda todos los objetos de la escena
    public GameObject[] allGameObjects;
    void Start()
    {
        //crea un ArrayList
        ArrayList aList = new ArrayList();
        //crea un array con todos los objetos de la escena
        //Object[] allObjects = GameObject.FindObjectOfType(typeof(Object)) as Object[];
        Object[] allObjects = GameObject.FindObjectsOfType<Object>();
        //itera a traves de todos los objetos
        foreach (Object o in allObjects)
        {
            //si hay un GameObject se añade a la lista
            GameObject go = o as GameObject;
            if (go != null)
            {
                aList.Add(go);
            }
        }
        //inicializamos la matriz allGameObjects
        allGameObjects = new GameObject[aList.Count];
        //copiamos lista a la matriz
        aList.CopyTo(allGameObjects);

    }
	// Update is called once per frame
	void Update () {
        
	}
}
