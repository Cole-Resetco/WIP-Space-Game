using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player329: MonoBehaviour
{

    // to start off i am going to be creating variables to calculate the vector from the object to the mouse
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
    private float oldX, oldY;

    private float rotation = 0;
    private float vecMag;


    // Use this for initialization
    void Start()
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

        //----------------------------------------------ROTATION-----------------------------------------------------------



        //-----------------------------------------------MOVEMENT----------------------------------------------------------

        //speed and directional buttons
        bool Forward = Input.GetKey("w");
        bool left = Input.GetKey("d");
        bool right = Input.GetKey("a");

        if (right == true)
        {
            rotation += 4;
            if (rotation >= 180)
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

        if (Forward == true)
        {
            speed += 0.001f;
        }
        else if (speed >= 0)
        {
            speed -= 0.0005f;
        }
        else if (speed <= -2)
        {
            speed = 0;
        }

        //----------------------------------------------PHYSICS-------------------------------------------------------------

        yDifferential = objPos.y - mousePos.y;
        xDifferential = objPos.x - mousePos.x;

        vecMag = Mathf.Sqrt(Mathf.Pow(xDifferential, 2) + Mathf.Pow(yDifferential, 2));
        if (vecMag == 0)
        {
            vecMag = 0.00001f;
        }

        xUnit = xDifferential / vecMag;
        yUnit = yDifferential / vecMag;
        CurrentDirection.Set(xUnit, yUnit, rotation); // Setting Directional vectors
        if (TDCheck == false)
        {
            TrueDirection.Set(xUnit, yUnit, 0);
            TrueDirection.z = rotation;

            TDCheck = true;
        }



        //Debug.Log("Speed: " + speed);
        //Debug.Log("Unit x: " + xUnit);
        //Debug.Log("Unit y: " + yUnit);
        //Apply A rotation matrix to the True Direction
        //Debug.Log("True Directionz Before: " + TrueDirection.z);
        //Debug.Log("Current DirectionZ Before: " + CurrentDirection.z);
        //Debug.Log("True Directionx Before: " + TrueDirection.x);
        //Debug.Log("True Directiony Before: " + TrueDirection.y);

        scalar_angle_1 = Mathf.Abs((CurrentDirection.z) - (TrueDirection.z));
        drift_scalar = scalar_angle_1;
        if ((CurrentDirection.z) < 0 && (TrueDirection.z > 0))
        {
            scalar_angle_2 = Mathf.Abs((CurrentDirection.z + 180) - (TrueDirection.z - 180));
            if (scalar_angle_1 > scalar_angle_2)
            {
                drift_scalar = scalar_angle_2;
                //Debug.Log("mark: " + scalar_angle_2);
            }
        }
        else if ((CurrentDirection.z) > 0 && (TrueDirection.z < 0))
        {
            scalar_angle_2 = Mathf.Abs((CurrentDirection.z - 180) - (TrueDirection.z + 180));
            if (scalar_angle_1 > scalar_angle_2)
            {
                drift_scalar = scalar_angle_2;
                // Debug.Log("mark: " + scalar_angle_2);
            }
        }
        drift_scalar = ((drift_scalar) * 0.02f) + 0.02f;
        // drift_scalar = (0.06f * Mathf.Abs(CurrentDirection.z - TrueDirection.z)) + 0.4f;
        if ((TrueDirection.z > CurrentDirection.z) && ((CurrentDirection.z + 180) > TrueDirection.z))
        {
            TrueDirection.z -= drift_scalar;

            TrueDirection.x = (Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
            TrueDirection.y = (Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            if (TrueDirection.z <= -180)
            {
                TrueDirection.z = 180;
            }
            //Debug.Log("True Directionx After: " + TrueDirection.x);
            //Debug.Log("True Directiony After: " + TrueDirection.y);
            //TrueDirection.x = (oldX * Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad)) - (oldY * Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            //TrueDirection.y = (oldX * Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad)) + (oldY * Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
        }
        else if ((TrueDirection.z < CurrentDirection.z) && ((TrueDirection.z + 180) > CurrentDirection.z))
        {
            TrueDirection.z += drift_scalar;
            TrueDirection.x = (Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
            TrueDirection.y = (Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            if (TrueDirection.z >= 180)
            {
                TrueDirection.z = -180;
            }
            //* Mathf.Deg2Rad
            //TrueDirection.x = (TrueDirection.x * Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad)) - (TrueDirection.y * Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            //TrueDirection.y = (TrueDirection.x * Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad)) + (TrueDirection.y * Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
        }
        else if ((TrueDirection.z > CurrentDirection.z) && ((CurrentDirection.z + 180) < TrueDirection.z))
        {
            TrueDirection.z += drift_scalar;
            TrueDirection.x = (Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
            TrueDirection.y = (Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            if (TrueDirection.z >= 180)
            {
                TrueDirection.z = -180;
            }
        }
        else if ((TrueDirection.z < CurrentDirection.z) && ((TrueDirection.z + 180) < CurrentDirection.z))
        {
            TrueDirection.z -= drift_scalar;
            TrueDirection.x = (Mathf.Cos(TrueDirection.z * Mathf.Deg2Rad));
            TrueDirection.y = (Mathf.Sin(TrueDirection.z * Mathf.Deg2Rad));
            if (TrueDirection.z <= -180)
            {
                TrueDirection.z = 180;
            }
        }

        //Debug.Log("Drift Scalar: " + drift_scalar);
        //Debug.Log("True Directionz Before: " + TrueDirection.z);
        //Debug.Log("Current DirectionZ Before: " + CurrentDirection.z);
        //Debug.Log("True Directionx After: " + TrueDirection.x);
        //Debug.Log("True Directionx After: " + TrueDirection.y);
        Debug.Log("True Directionz After: " + TrueDirection.z);

        Debug.Log("Current Direction: " + CurrentDirection.z);
        //Debug.Log("Unit x: " + xUnit);
        //Debug.Log("Unit y: " + yUnit);
        transform.position = transform.position - new Vector3(speed * (TrueDirection.x), speed * (TrueDirection.y), 0);
        //transform.position = transform.position - new Vector3(speed * (xUnit), speed * (yUnit), 0);

    }
}
