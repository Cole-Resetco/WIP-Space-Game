using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public float alpha = 1;
    

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
    }
	
	// Update is called once per frame
	void Update () {

        alpha -= 0.02f;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);

	}
}
