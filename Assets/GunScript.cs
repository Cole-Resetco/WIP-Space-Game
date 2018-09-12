using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {


    public Vector3 RotationVector;
    public Vector3 MouseVector;
    public GameObject Ship;
    public GameObject Shot;
    public GameObject ShotCreator;
    public float Angle;
    public float rotation = 0;


	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        

        //---------------------------------Rotation---------------------------------------------//
        RotationVector.x = Input.mousePosition.x - Ship.transform.position.x - 294;
        RotationVector.y = Input.mousePosition.y - Ship.transform.position.y - 320;
        MouseVector.x = Input.mousePosition.x;
        MouseVector.y = Input.mousePosition.y;
        Angle = Mathf.Atan2(RotationVector.y, RotationVector.x);
        Angle = Mathf.Rad2Deg * Angle;

        transform.rotation = Quaternion.Euler(0,0, Angle);

        if(Input.GetMouseButton(0))
        {
            if(Random.Range(0, 10) > 3)
            {
                ShotCreator = Instantiate(Shot, transform.position, this.transform.rotation);
                ShotCreator.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-6, 6));
                ShotCreator.transform.localPosition += transform.TransformDirection(3.5f, 0, 0);
            }

        }

	}
}
