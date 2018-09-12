using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    // to start off i am going to be creating variables to calculate the vector from the object to the mouse
    public GameObject trail; 
    public GameObject testBoost; // boost prefab set
    public GameObject testBoostEdit;
    public Test_Boost Boost_Script;
    public GameObject testFlame; //Flame prefab set
    public GameObject testFlameEdit;
    public GameObject testFlameEdit2;

    private float boostOffset = 1.5f;
    private bool boostOffsetDir = true;
    private float trailInterval = 3;
    private float boostInterval = 2;
    private float sineTime = 1;
    private float tempAngleLast;

    private float speed;

    private float drift_scalar;
    private float scalar_angle_1;
    private float scalar_angle_2;

    private float xDifferential;
    private float yDifferential;
    private float xUnit;
    private float yUnit;

    private Vector3 TrueDirection;
    private Vector3 CurrentDirection;
    private bool TDCheck; //for instantiation of TrueDirection
    private float DifX, DifY;

    private float rotation = 0;
    private float vecMag;

    private float baseAccel = 0.035f;


	// Use this for initialization
	void Start ()
    {
        speed = 0.0f;
        TrueDirection.Set(0, 0, 0);
        CurrentDirection.Set(0, 0, 0);
        TDCheck = false;



    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------------------------ROTATION-----------------------------------------------------------
        //isolate mouse position relative to camera
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //isolate object position with respect to camera 
        Vector2 objPos = (Vector2)Camera.main.ScreenToWorldPoint(transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //calculate angle


        //find direction vector raw data


        //Debug.Log("current mxdif: " + transform.position.x);
        //Debug.Log("current xdif: " + xDifferential);
        //Debug.Log("current ydif: " + yDifferential);
        //Debug.Log("current rotation: " + rotation);


        //rotate
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);




        //-----------------------------------------------MOVEMENT----------------------------------------------------------

        //speed and directional buttons
        bool Forward = Input.GetKey("w");
        bool left = Input.GetKey("d");
        bool right = Input.GetKey("a");

        if (right == true)
        {
            rotation += 4;
            if(rotation >= 180)
            {
                rotation = -180;
            }
        }
        if (left == true)
        {
            if (rotation <= -180)
            {
                rotation = 180;
            }
            rotation -= 4;
        }


        //----------------------------------------------PHYSICS-------------------------------------------------------------


        CurrentDirection.Set(0, 0, rotation); // Setting Directional vectors
        CurrentDirection.x = (Mathf.Cos(CurrentDirection.z * Mathf.Deg2Rad));
        CurrentDirection.y = (Mathf.Sin(CurrentDirection.z * Mathf.Deg2Rad));

        if (TDCheck == false)
        { 
            TrueDirection.z = rotation;
            TrueDirection.x = 0;
            TrueDirection.y = 0;

            TDCheck = true;
        }

        speed = Mathf.Sqrt(Mathf.Pow(TrueDirection.x, 2) + Mathf.Pow(TrueDirection.y, 2));

        if(Forward == true)
        {

            if (speed > 1.0f) //Max Speed
            {
                //Debug.Log("current xdir: " + TrueDirection.x);
                //Debug.Log("current ydir: " + TrueDirection.y);

                TrueDirection.x += (baseAccel - 0.01f) * (CurrentDirection.x) * ((speed) / 2 + 0.5f);
                TrueDirection.y += (baseAccel - 0.01f) * (CurrentDirection.y) * ((speed) / 2 + 0.5f);

                float tempAngle = Mathf.Atan2(TrueDirection.x, TrueDirection.y);

                TrueDirection.y = 1.0f *(Mathf.Cos(tempAngle));
                TrueDirection.x = 1.0f *(Mathf.Sin(tempAngle));

                //Debug.Log("current after xdir: " + TrueDirection.x);
                //Debug.Log("current after ydir: " + TrueDirection.y);




            }
            else //Acceleration
            { 
                TrueDirection.x += baseAccel * (CurrentDirection.x) * ((speed)/2 + 0.5f);
                TrueDirection.y += baseAccel * (CurrentDirection.y) * ((speed)/2 + 0.5f);

                //Debug.Log("Speed: " + speed);
            }
            //Drag force durring Acceleration
            TrueDirection.x = ((990 * TrueDirection.x) / 1000);
            TrueDirection.y = ((990 * TrueDirection.y) / 1000);
        }
        else
        {


            TrueDirection.x += (0.01f * speed) * (CurrentDirection.x);
            TrueDirection.y += (0.01f * speed) * (CurrentDirection.y);
            TrueDirection.x = (97 * TrueDirection.x) / 100;
            TrueDirection.y = (97 * TrueDirection.y) / 100;
        }
        

        transform.position = transform.position - new Vector3((TrueDirection.x),(TrueDirection.y), 0);
        //transform.position = transform.position - new Vector3(speed * (xUnit), speed * (yUnit), 0);

        //======================================================EFFECTS=====================================================

        

        //-------------------------------------Trail Info-------------------------------------------------------------------
        if (trailInterval == 0)
        {
            //Instantiate(trail, this.transform.position , this.transform.rotation);
            trailInterval = 3;
        }
        else
        {
            trailInterval -= 1;
        }
        
        // Boost
        if(Input.GetKeyDown("w")) // this if is virtually usleless but for some reason required to proc the else
        {
            //Debug.Log("Proc");
            //Instantiate(testBoost, this.transform.position, this.transform.rotation);
            Boost_Script = testBoost.GetComponent<Test_Boost>();
            Boost_Script.rotation = rotation - 90;

            boostInterval = 1;
        }
        else if (((boostInterval <= 0) && (Input.GetKeyDown("w"))) || ((boostInterval == 0) && (Input.GetKey("w"))))
        {
            //Debug.Log("Proc");
            float TruDirMult = 0.8f;

            if ((Input.GetKey("a")) || (Input.GetKey("d")))
                {
                //TruDirMult = 3.2f;
            }

            float tempAngle2 = Mathf.Atan2(CurrentDirection.x +  (TruDirMult * (TrueDirection.x - CurrentDirection.x)), CurrentDirection.y + (TruDirMult * (TrueDirection.y - CurrentDirection.y)));         
            tempAngle2 = tempAngle2 * Mathf.Rad2Deg;
            Debug.Log("TempAngle: " + tempAngle2);
            /*
            if(float.IsNaN(tempAngleLast))
            {
                tempAngleLast = tempAngle2;
            }
            if((tempAngleLast - tempAngle2) > 0)
            {
                tempAngleLast += 0.1f;
            }
            else if ((tempAngleLast - tempAngle2) < 0)
            {
                tempAngleLast -= 0.1f;
            }
            */

        //----------------------------------------------------------Trail---------------------------------------------------------------

            testBoostEdit = Instantiate(testBoost, this.transform.localPosition  , Quaternion.Euler(0, 0, -tempAngle2 )); //local position is the position of the parent
            testBoostEdit.transform.position = this.transform.localPosition  +  transform.TransformDirection(boostOffset, 1.2f, 0);

            testBoostEdit = Instantiate(testBoost, this.transform.localPosition , Quaternion.Euler(0, 0, -tempAngle2));
            testBoostEdit.transform.position = this.transform.localPosition  + transform.TransformDirection(-boostOffset, 1.2f, 0);

            //tempAngleLast = tempAngle2;

        
            float boostPuffModifier = Random.Range(-0.2f, 0.2f);

            float rand_range = Random.Range(0, 10);
            //if (rand_range > 3)
            //{
        //------------------------------------------------------Particles----------------------------------------------------------------------
                testFlameEdit = Instantiate(testFlame, this.transform.localPosition + new Vector3(-3.1f, 1.5f, 0), Quaternion.Euler(0, 0, rotation - 90 + Random.Range(-20, 20)));



                //testFlameEdit.transform.position = this.transform.localPosition + transform.TransformDirection((0.8f * -boostOffset) + 0.2f, -3f, 0);
                //testFlameEdit2.transform.position = this.transform.localPosition + transform.TransformDirection((0.8f * boostOffset) + 0.2f, -3f, 0);
                testFlameEdit.transform.position = this.transform.localPosition + transform.TransformDirection(0.8f * -boostOffset, -1.5f, 0);
                testFlameEdit.transform.localScale = 1.0f * this.transform.localScale +  new Vector3(boostPuffModifier, boostPuffModifier, 0);


            //}

            rand_range = Random.Range(0, 10);
            //if (rand_range > 3)
            //{

                testFlameEdit2 = Instantiate(testFlame, this.transform.localPosition + new Vector3(-3.1f, 1.5f, 0), Quaternion.Euler(0, 0, rotation - 90 + +Random.Range(-20, 20)));
                testFlameEdit2.transform.position = this.transform.localPosition + transform.TransformDirection(0.8f * boostOffset, -1.5f, 0);
                testFlameEdit2.transform.localScale = 1.0f * this.transform.localScale + new Vector3(boostPuffModifier, boostPuffModifier, 0); 

            //}

        //-------------------------------------------------------Sine Wave---------------------------------------------------------------------------
            Boost_Script = testBoost.GetComponent<Test_Boost>();
            Boost_Script.rotation = rotation - 90;

            boostInterval = 0;

            sineTime += 0.1f;
            if(sineTime > 6.28)
            {
                sineTime = 0;
            }

            boostOffset = Mathf.Sin(sineTime) * 1.0f;


        }
        else
        {
            boostInterval -= 1;

        }

    }
}
