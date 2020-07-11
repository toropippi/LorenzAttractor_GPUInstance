using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAround : MonoBehaviour
{
    //回転させるスピード
    float rotateSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        //中心位置情報
        Vector3 centerPos = new Vector3(2f, -2f, 25f);
        //カメラを回転させる
        this.transform.RotateAround(centerPos, Vector3.up, rotateSpeed);
    }
}