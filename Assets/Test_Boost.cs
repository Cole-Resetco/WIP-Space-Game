using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Boost : MonoBehaviour {

    //public GameObject ship;

    public float alpha = 0.0f;
    public float xDir, yDir;
    public float rotation = 0;
    public int startFrames = 15;
    public Color color; 
    // Use this for initialization
    void Start () {
        color = GetComponent<SpriteRenderer>().color;
        color.r = 1f;

        startFrames = 15;
        color.a = 1f;

        Destroy(gameObject, 1.2f);

        //transform.parent = ship.transform;
        GetComponent<SpriteRenderer>().color = color;
        //transform.rotation = Quaternion.Euler(0, 0, rotation); 
        xDir = 0.2f * (Mathf.Cos((rotation -90) * Mathf.Deg2Rad));
        yDir = 0.2f * (Mathf.Sin((rotation -90) * Mathf.Deg2Rad));
        transform.position = transform.position - new Vector3(xDir * 15, yDir * 15, 0);
        //transform.rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update() {
        
        if (startFrames > 7)
        {
           // color.a += 0.04f;
            
            transform.position = transform.position - new Vector3(xDir * 2.5f, yDir * 2.5f, 0);
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            GetComponent<SpriteRenderer>().color = color;
            startFrames--;

        }
        else if (startFrames > 0)
        {
            color.a -= 0.02f;
            transform.position = transform.position - new Vector3(xDir * 1.5f, yDir * 1.5f, 0);
            GetComponent<SpriteRenderer>().color = color;
            startFrames--;

        }
        else
        {
            transform.position = transform.position - new Vector3(xDir * 1.5f, yDir * 1.5f, 0);
            color.r -= 0.1f;
            color.a -= 0.03f;
            GetComponent<SpriteRenderer>().color = color;

            transform.localScale += new Vector3(.02f, .02f, 0);
        }


    }
}
