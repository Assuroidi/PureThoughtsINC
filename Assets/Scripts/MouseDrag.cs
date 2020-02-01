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
    private bool draggingEnabled;
    private ToolSelection gmToolScript;


    private void Start()
    {
        mainCamera = Camera.main;
        draggingEnabled = true;
        gmToolScript = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<ToolSelection>();
        if (gmToolScript)
        {
            gmToolScript.AnnounceToolChanged += GmToolScript_AnnounceToolChanged;
        }
        else
        {
            Debug.LogWarning("MouseDrag could not find GameManager's ToolSelection script!");
        }
    }

    private void OnDestroy()
    {
        if (gmToolScript)
        {
            gmToolScript.AnnounceToolChanged -= GmToolScript_AnnounceToolChanged;
        }
    }

    private void GmToolScript_AnnounceToolChanged(ETool toolEnum)
    {
        switch (toolEnum)
        {
            case ETool.None:
                draggingEnabled = true;
                break;
            default:
                draggingEnabled = false;
                break;
        }
    }

    private void OnMouseDown()
    {
        if (!draggingEnabled)
        {
            return;
        }
        dragPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        dragPlane.Raycast(camRay, out float planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {
        if (!draggingEnabled)
        {
            return;
        }
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        dragPlane.Raycast(camRay, out float planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }

}
