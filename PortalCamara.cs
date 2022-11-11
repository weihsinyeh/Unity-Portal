using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalCamara : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform portal;
    public Transform another_portal;

    // Update is called once per frame
    
    void Update()
    {
        
                
        
        float angle1 = another_portal.transform.eulerAngles.x - portal.transform.eulerAngles.x;
        float sinx = Mathf.Sin(angle1 * Mathf.Deg2Rad);
        float cosx = Mathf.Cos(angle1 * Mathf.Deg2Rad);

        float angle2 = another_portal.transform.eulerAngles.y - portal.transform.eulerAngles.y;
        float siny = Mathf.Sin(angle2 * Mathf.Deg2Rad);
        float cosy = Mathf.Cos(angle2 * Mathf.Deg2Rad);

        float angle3 = another_portal.transform.eulerAngles.z - portal.transform.eulerAngles.z;
        float sinz = Mathf.Sin(angle3 * Mathf.Deg2Rad);
        float cosz = Mathf.Cos(angle3 * Mathf.Deg2Rad);

        float r11 =  cosy * cosz + siny * sinx * sinz;
        float r12 = -cosy * sinz + siny * sinx * cosz;
        float r13 =  siny * cosx;
        float r21 =  cosx * sinz;
        float r22 =  cosx * cosz;
        float r23 = -sinx;
        float r31 = -siny * cosz + cosy * sinx * sinz;
        float r32 =  siny * sinz + cosy * sinx * cosz;
        float r33 =  cosy * cosx;

        Vector3 vector1 = (player.GetChild(0).position - another_portal.position);

        Vector3 vectorNew = new Vector3(vector1.x * r11 + vector1.y * r12 + vector1.z * r13,
                                        vector1.x * r21 + vector1.y * r22 + vector1.z * r23,
                                        vector1.x * r31 + vector1.y * r32 + vector1.z * r33);
     
        transform.position = portal.position + vectorNew;

        Vector3 newCameraDircetion = new Vector3(player.GetChild(0).forward.x * r11 + player.GetChild(0).forward.y * r12 + player.GetChild(0).forward.z * r13,
                                                 player.GetChild(0).forward.x * r21 + player.GetChild(0).forward.y * r22 + player.GetChild(0).forward.z * r23,
                                                 player.GetChild(0).forward.x * r31 + player.GetChild(0).forward.y * r32 + player.GetChild(0).forward.z * r33);

        transform.rotation = Quaternion.LookRotation(newCameraDircetion, Vector3.up);
        
    }
}
