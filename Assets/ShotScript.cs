using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.TransformDirection(2, 0 , 0);
		
	}
}
