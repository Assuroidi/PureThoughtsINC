using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringePlunging : MonoBehaviour
{
    public GameObject obfuscator;   // Used for getting transform's coordinates
    public bool useObfuscation;
    public float timer;
    private float speed;
    private bool plungingInProgress;

    // Start is called before the first frame update
    void Start()
    {
        plungingInProgress = false;
        if (speed <= 0)
        {
            speed = 1f;
        }
    }

    public void StartPlunging(float timerDuration = 3f)
    {
        if (!plungingInProgress)
        {
            plungingInProgress = true;
            StartCoroutine(Plunging(timerDuration));
        }
    }

    IEnumerator Plunging(float timerDuration)
    {
        timer = timerDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Time.deltaTime * speed, transform.localPosition.z);
            yield return null;
        }
        EndPlunging();
    }

    public void EndPlunging()
    {
        plungingInProgress = false;
    }

#if UNITY_EDITOR
    //using UnityEditor;

    [UnityEditor.CustomEditor(typeof(SyringePlunging))]
    public class SyringePlungingEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            SyringePlunging testScript = (SyringePlunging)target;
            if (GUILayout.Button("Test plunging"))
            {
                testScript.StartPlunging();
            }
        }
    }
#endif

}
