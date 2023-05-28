using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class destelloCod : MonoBehaviour
{
    public Sprite [] destello;
    // Start is called before the first frame update
    void Start()
    {
        cambioDestellos(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void cambioDestellos(int pos){
        this.GetComponent<Image>().sprite = destello [pos];
    }
}
