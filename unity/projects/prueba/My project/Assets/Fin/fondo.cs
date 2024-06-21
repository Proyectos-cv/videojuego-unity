using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class fondo : MonoBehaviour
{
    [SerializeField] private GameObject lejos;
    [SerializeField] private GameObject cerca;

    [SerializeField] private GameObject estrella;
    [SerializeField] private GameObject luna;

    /* [SerializeField] private GameObject front; */

    [SerializeField] private GameObject soml;
    [SerializeField] private GameObject somc;

    [SerializeField] private float speed;

    private Renderer sombrac;
    private Renderer sombral;
    /* private Renderer fron; */

    private Renderer estre;
    private Renderer lun;

    private Renderer lejosrenderer;
    private Renderer cercarenderer;
    private float inix,difx;
    // Start is called before the first frame update
    void Start()
    {
        lejosrenderer=lejos.GetComponent<Renderer>();
        cercarenderer=cerca.GetComponent<Renderer>();
        sombrac=somc.GetComponent<Renderer>();
        sombral=soml.GetComponent<Renderer>();
        /* fron=front.GetComponent<Renderer>(); */
        estre=estrella.GetComponent<Renderer>();
        lun=luna.GetComponent<Renderer>();
        inix=transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        difx=inix-transform.position.x;

        lejosrenderer.material.mainTextureOffset=new Vector2(difx*(speed*0.1f)*-1,0.0f);
        cercarenderer.material.mainTextureOffset= new Vector2(difx*(speed*0.3f)*-1,0.0f); 

        sombrac.material.mainTextureOffset= new Vector2(difx*(speed*0.3f)*-1,0.0f);
        sombral.material.mainTextureOffset= new Vector2(difx*(speed*0.1f)*-1,0.0f);
        /* fron.material.mainTextureOffset= new Vector2(difx*(speed)*-1,0.0f); */

        estre.material.mainTextureOffset= new Vector2(difx*(speed*0.01f)*-1,0.0f);
        lun.material.mainTextureOffset= new Vector2(difx*(speed*0.01f)*-1,0.0f);

        lejos.transform.position=new Vector3(transform.position.x,lejos.transform.position.y,lejos.transform.position.z);
        cerca.transform.position=new Vector3(transform.position.x,cerca.transform.position.y,cerca.transform.position.z);

        //front.transform.position=new Vector3(transform.position.x,front.transform.position.y,front.transform.position.z);

        somc.transform.position=new Vector3(transform.position.x,somc.transform.position.y,somc.transform.position.z);
        soml.transform.position=new Vector3(transform.position.x,soml.transform.position.y,soml.transform.position.z);

        estrella.transform.position=new Vector3(transform.position.x,estrella.transform.position.y,estrella.transform.position.z);
        luna.transform.position=new Vector3(transform.position.x,luna.transform.position.y,luna.transform.position.z);
    }
}
