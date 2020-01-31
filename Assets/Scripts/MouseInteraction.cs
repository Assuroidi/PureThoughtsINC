using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for calling other scripts' methods when clicking on the attached gameobject.
/// A collider is required, such as BoxCollider2D.
/// </summary>

public class MouseInteraction : MonoBehaviour
{
    public float longClickTime;
    [SerializeField]
    private bool mousePressed;
    [SerializeField]
    private float timeClicked;
    public UnityEvent shortClickEvents;
    public UnityEvent longClickEvents;
    public UnityEvent instantClickEvents;

    private void Start()
    {
        if (longClickTime <= 0f)
        {
            longClickTime = 2f;
        }
    }

    private void OnMouseDown()
    {
        instantClickEvents?.Invoke();
        if (!mousePressed)
        {
            mousePressed = true;
            StartCoroutine(MouseDownCounter());
        }
    }

    private void OnMouseUp()
    {
        mousePressed = false;
    }

    IEnumerator MouseDownCounter()
    {
        timeClicked = 0f;
        while (mousePressed)
        {
            timeClicked += Time.deltaTime;
            yield return null;
        }
        if (timeClicked < longClickTime)
        {
            shortClickEvents?.Invoke();
        }
        else
        {
            longClickEvents?.Invoke();
        }
    }

    public void TestFunction1()
    {
        Debug.Log("Testing function 1");
    }
    public void TestFunction2()
    {
        Debug.Log("Testing function 2");
    }
    public void TestFunction3()
    {
        Debug.Log("Testing function 3");
    }

    //Kätevä, kannattaa kopioida
#if UNITY_EDITOR
    //using UnityEditor;

    [UnityEditor.CustomEditor(typeof(MouseInteraction))]
    public class MouseInteractionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MouseInteraction testScript = (MouseInteraction)target;
            if (GUILayout.Button("Test wut"))
            {
                testScript.TestFunction1();
            }
        }
    }
#endif

}
