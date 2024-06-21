using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
   
    public GameObject fin;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicion = transform.position;
        posicion.x = fin.transform.position.x;
        transform.position = posicion;
    }
}

