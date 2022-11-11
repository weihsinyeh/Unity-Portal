using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public bool transport = false;
    public Vector3 newposition;
    public float angle_x,angle_y,angle_z;
    bool rotate = false;
    // Update is called once per frame


    void FixedUpdate()
    {
       

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (transport == false)
        {
            Vector3 move = transform.right * x + transform.forward * (z);
            controller.Move(move * speed * Time.fixedDeltaTime);

        }
        else if(transport == true && rotate == false)
        {
            controller.transform.position = newposition;
            rotate = true;
        }
        else if(transport == true && rotate == true)
        {
            controller.transform.Rotate(angle_x, angle_y, angle_z);
            rotate = false;
            transport = false;
        }
            
    }
   
}
