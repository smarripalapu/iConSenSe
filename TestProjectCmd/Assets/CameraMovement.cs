using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public float speed = 3.0f;
    public float speedH = 3.0f;
   public float speedV = 3.0f;

   private float yaw = 0.0f;
   private float pitch = 0.0f;



    // Update is called once per frame
    void Update()
    {


      if(Input.GetKey(KeyCode.RightArrow))
     {
         transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.LeftArrow))
     {
         transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.DownArrow))
     {
         transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
     }
     if(Input.GetKey(KeyCode.UpArrow))
     {
         transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
     }
     if(Input.GetKey(KeyCode.W))
     {
         transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
     }
     if(Input.GetKey(KeyCode.S))
     {
         transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
     }


     yaw += speedH * Input.GetAxis("Mouse X");
     pitch -= speedV * Input.GetAxis("Mouse Y");

     transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");
            if (Input.GetMouseButton(0))
                 {
                     Debug.Log("Pressed left click.");
                 }

            if (Input.GetMouseButton(1))
                 {
                     Debug.Log("Pressed right click.");
                 }

            if (Input.GetMouseButton(2))
                 {
                     Debug.Log("Pressed middle click.");
                 }

    }
}
