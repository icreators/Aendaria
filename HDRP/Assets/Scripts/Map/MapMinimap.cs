using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMinimap : MonoBehaviour
{
    void set(float nowX,float nowY,float angle)
    {
        Vector2 pos=MapPoint.instance.playerPosition(nowX,nowY);

        Material mat = Instantiate(this.GetComponent<Image>().material);
        mat.SetFloat("_OffsetX", pos.x);
        mat.SetFloat("_OffsetY", pos.y);
        this.GetComponent<Image>().material = mat;

        transform.localRotation = Quaternion.Euler(1,1,angle);
    }
}
