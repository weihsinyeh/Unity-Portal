using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.Collections.LowLevel.Unsafe;
//using System.Numerics;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    public Transform player;
    public Transform another_portal;

 
    void OnTriggerEnter(Collider other)
    {
        
         Vector3 portalToPlayer = player.transform.position - transform.transform.position;
         float dotProduct = Vector3.Dot(transform.transform.forward, portalToPlayer);
        Debug.Log(dotProduct);
        Debug.Log(portalToPlayer);
        if (other.tag == "Player" && dotProduct>0.0 && player.GetComponent<PlayerMovement>().transport == false )
         {
             float angle_x = transform.transform.eulerAngles.x - another_portal.transform.eulerAngles.x;
             float sin_x = Mathf.Sin(angle_x * Mathf.Deg2Rad);
             float cos_x = Mathf.Cos(angle_x * Mathf.Deg2Rad);

             float angle_y = transform.transform.eulerAngles.y - another_portal.transform.eulerAngles.y;
             float sin_y = Mathf.Sin(angle_y * Mathf.Deg2Rad);
             float cos_y = Mathf.Cos(angle_y * Mathf.Deg2Rad);

             float angle_z = transform.transform.eulerAngles.z - another_portal.transform.eulerAngles.z;
             float sin_z = Mathf.Sin(angle_z * Mathf.Deg2Rad);
             float cos_z = Mathf.Cos(angle_z * Mathf.Deg2Rad);

             float r11 =  cos_y * cos_z + sin_y * sin_x * sin_z;
             float r12 = -cos_y * sin_z + sin_y * sin_x * cos_z;
             float r13 =  sin_y * cos_x;
             float r21 =  cos_x * sin_z;
             float r22 =  cos_x * cos_z;
             float r23 = -sin_x;
             float r31 = -sin_y * cos_z + cos_y * sin_x * sin_z;
             float r32 =  sin_y * sin_z + cos_y * sin_x * cos_z;
             float r33 =  cos_y * cos_x;

             Vector3 vectorNew = new Vector3(portalToPlayer.x * r11 + portalToPlayer.y * r12 + portalToPlayer.z * r13,
                                             -(portalToPlayer.x * r21 + portalToPlayer.y * r22 + portalToPlayer.z * r23),
                                             portalToPlayer.x * r31 + portalToPlayer.y * r32 + portalToPlayer.z * r33);
            
            player.GetComponent<PlayerMovement>().newposition = another_portal.position + vectorNew;
            player.GetComponent<PlayerMovement>().angle_x = angle_x;
            player.GetComponent<PlayerMovement>().angle_y = angle_y;
            player.GetComponent<PlayerMovement>().angle_z = angle_z;
            player.GetComponent<PlayerMovement>().transport = true;
        }
    }

   
    void OnTriggerExit(Collider other)
    {
    }
}
