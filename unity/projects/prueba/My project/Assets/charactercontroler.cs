using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroler : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        procesarmovimiento();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            procesarsalto();
        }
    }
    void procesarmovimiento(){
        float inputmovimiento = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = new Vector2(inputmovimiento * speed, rigidbody.velocity.y);
    }
    void procesarsalto(){
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 5);
    }
}
