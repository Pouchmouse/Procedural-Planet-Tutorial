using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float m_MouseDragSensitivity = 1.0f;
    public float m_MouseZoomSensitivity = 1.0f;

    public float m_MinZoom = 2.0f;
    public float m_MaxZoom = 10.0f;

    Vector3 m_PrevMousePosition;
    float   m_Zoom;

    private void Start()
    {
        m_Zoom = Mathf.Lerp(m_MinZoom, m_MaxZoom, 0.5f);
    }

    void Update ()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
            m_PrevMousePosition = currentMousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDisplacement = currentMousePosition - m_PrevMousePosition;

            mouseDisplacement.x /= Screen.width;
            mouseDisplacement.y /= Screen.height;

            Quaternion yaw   = Quaternion.AngleAxis(mouseDisplacement.x * m_MouseDragSensitivity,  transform.up);
            Quaternion pitch = Quaternion.AngleAxis(mouseDisplacement.y * m_MouseDragSensitivity, -transform.right);

            transform.localRotation = yaw * pitch * transform.localRotation;
        }

        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(mouseWheelInput != 0.0f)
        {
            m_Zoom += m_MouseZoomSensitivity * mouseWheelInput;
            m_Zoom  = Mathf.Clamp(m_Zoom, m_MinZoom, m_MaxZoom);
        }

        transform.position = transform.forward * -m_Zoom;

        m_PrevMousePosition = currentMousePosition;
    }
}
