using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFix : MonoBehaviour
{
    public Transform Player;
    public Transform Camera;
    
    public Transform DeCamera;

    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        if(moveDirection.z > 0.1f || moveDirection.z < -0.1f)
        {
            Vector3 backupcamera=DeCamera.rotation.eulerAngles;

            

            float normalization=normalize(Player.rotation.eulerAngles.y,DeCamera.rotation.eulerAngles.y,5);
            //Player.rotation = Quaternion.Euler( Player.rotation.eulerAngles.x,DeCamera.rotation.eulerAngles.y,Player.rotation.eulerAngles.z);
            Player.rotation = Quaternion.Euler( Player.rotation.eulerAngles.x,normalization,Player.rotation.eulerAngles.z);
            


            Camera.Rotate(new Vector3(0,-(DeCamera.rotation.eulerAngles.y-backupcamera.y),0));

            
        }
    }

    float normalize(float a,float b,float str)
    {
        
        a=cut(a);
        b=cut(b);
        
        float c=cut(a-b);

        if(c<=str*2)
            return b;

        if(c>180)
            a+=str;
        else
            a-=str;

        return a;
    }
    float cut(float angle)
    {
        while(angle>360f)
            angle-=360f;
        while(angle<0)
            angle+=360;
        return angle;
    }
}
