using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnImagenes : MonoBehaviour
{
    public GameObject prefab;

    public float interval;
    public float minX;
    public float maxX;
    public float y;

    public Sprite[] imagenHechizo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn(){
        GameObject instance = Instantiate(prefab);
        instance.transform.position = new Vector2(Random.Range(minX, maxX),y);

        //selccionar un sprite para los objetos
        Sprite randomSprite = imagenHechizo[Random.Range(0, imagenHechizo.Length)];
        instance.GetComponent<SpriteRenderer> ().sprite = randomSprite;
    }
}
