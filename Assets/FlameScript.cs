using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour {

    public float sineTime = 0;
    public float mySineTime = 0;
    public float timeAllowed = 60;
    public float alpha = 2.0f;
    public float rand_speed = 0;
    public float rand_exists = 0;
    public float offset_color = 0;
    public bool first_frame;
    public Color color;

    // Use this for initialization
    void Start () {
        color = GetComponent<SpriteRenderer>().color;
        first_frame = true;
        offset_color = 0.5f;
        alpha += 1.0f;
        rand_speed = Random.Range(0.4f, 1.4f);
        rand_exists = Random.Range(0, 10);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        Destroy(gameObject, 0.37f);
	}
	
	// Update is called once per frame
	void Update () {

        if (first_frame == true)
        {
            first_frame = false;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        }

        //if (rand_exists > 3)
        //{
            sineTime += 1f;
            if (sineTime > 6.28)
            {
                sineTime = 0;
            }
            transform.position += transform.TransformDirection(0, rand_speed, 0);
            mySineTime = Mathf.Sin(sineTime) * 1.0f;

            if (offset_color < 1f)
            {
                offset_color += 0.1f;
            }

            color.r -= 0.05f;
            GetComponent<SpriteRenderer>().color = color;


            transform.localScale -= new Vector3(0.08f, 0.08f);
       // }

        //else
        //{
         //   alpha = 0;
        //    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        //}




        //transform.localScale = new Vector3 (1, (0.2f * mySineTime) + 1.5f, 1);



        //Debug.Log("Sine Time: " + sineTime);

    }
}
