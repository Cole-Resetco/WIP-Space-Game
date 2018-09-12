using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Ship;
    public Vector3 offset;

	// Use this for initialization
	void Start () {

        offset = transform.position - Ship.transform.position;

	}

    // Update is called once per frame
    void Update()
    {

        transform.position = Ship.transform.position + offset;

    }
}


