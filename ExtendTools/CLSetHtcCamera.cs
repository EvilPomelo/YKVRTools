using UnityEngine;
using System.Collections;

public class CLSetHtcCamera : MonoBehaviour
{
    public Transform m_HTCCameraHead, m_HTCCameraRig, m_headPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraPosition();

    }
    void UpdateCameraPosition()
    {
        Vector3 tempPoint = m_HTCCameraHead.position;
        Vector3 _htcCameraRig = m_HTCCameraRig.position;
        Vector3 point = m_headPoint.position;
        //头到底部的单位向量
        Vector3 _norma = (_htcCameraRig - tempPoint).normalized;
        //头到底部的距离
        float _dis = Vector3.Distance(_htcCameraRig, tempPoint);
        //头到底部的位移
        Vector3 tempPoint2 = point + _norma * _dis;
        m_HTCCameraRig.position = tempPoint2;
    }
}
