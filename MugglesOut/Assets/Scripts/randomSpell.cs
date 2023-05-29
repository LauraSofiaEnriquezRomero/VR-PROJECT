using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpell : MonoBehaviour
{
    public GameObject [] hechizosDefinidos;
    int rand;

	// Use this for initialization
	void Start () {
        Generar();
	}
	
	void Update () {
		
	}

    void Generar()
    {        
    //con  esta sintaxis hacemos un bucle foreach de los Transforms de los
    //objetos hijos               
        foreach (Transform child in transform)
        {
            rand = Random.Range(0, hechizosDefinidos.Length);
            Instantiate(hechizosDefinidos[rand], child.position, Quaternion.identity);
        }
    }
}

