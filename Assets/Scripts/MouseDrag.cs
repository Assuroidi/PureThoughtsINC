using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Used for dragging the attached gameobject across a 2D plane that is perpendicular to the main camera's forward vector.
/// A collider is required, such as BoxCollider2D.
/// </summary>

public class MouseDrag : MonoBehaviour
{
    private Plane dragPlane;
    private Camera mainCamera;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        dragPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        dragPlane.Raycast(camRay, out float planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        dragPlane.Raycast(camRay, out float planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }

}
