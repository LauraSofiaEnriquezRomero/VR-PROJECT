using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoSpawnImagen : MonoBehaviour
{
    public float xMinSpeed;
    public float xMaxSpeed;
    public float yMinSpeed;
    public float yMaxSpeed;

    public float lifeTime;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(xMinSpeed,xMaxSpeed), Random.Range(yMinSpeed,yMaxSpeed));

         Destroy (gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
