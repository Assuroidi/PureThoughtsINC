using UnityEngine;
using System;
using System.Collections.Generic;

class Node {
  public static Vector3 gravity = new Vector3(0, 0, -9.0f);
  public static float resistance = 0.1f;

  public Vector3 location = new Vector3();
  public Vector3 speed = new Vector3();

  public Node(Vector3 start) {
    location = start;
  }

  public void Update() {
    speed += gravity * Time.deltaTime;
    speed *= 1.0f - (resistance * Time.deltaTime);
    location += speed * Time.deltaTime;
    if (location.z < -5.0 && speed.z < 0) speed.z = -speed.z;
  }
}

public class WobblyPlane : MonoBehaviour {
  Mesh mesh;
  Node[] nodes;

  void Start() {
    mesh = GetComponent<MeshFilter>().mesh;
    nodes = new Node[mesh.vertices.Length];
    for (int i = 0; i < mesh.vertices.Length; i++) nodes[i] = new Node(mesh.vertices[i]);
    int[] tls = mesh.triangles;
    for (int i = 0; i < tls.Length; i += 3) {
      int v1a = tls[i],   v1b = tls[i+1];
      if (v1a > v1b) (v1a, v1b) = (v1b, v1a);
      int v2a = tls[i+1], v2b = tls[i+2];
      if (v2a > v2b) (v2a, v2b) = (v2b, v2a);
      int v3a = tls[i+2], v3b = tls[i];
      if (v3a > v3b) (v3a, v3b) = (v3b, v3a);
      Debug.Log("Edge: " + v1a + ' ' + v1b);
    }
  }

  void Update() {
    foreach (Node nd in nodes) nd.Update();
 
    Vector3[] tmp = new Vector3[nodes.Length];
    for (int i = 0; i < nodes.Length; i++) tmp[i] = nodes[i].location;

    mesh.vertices = tmp;
    mesh.RecalculateBounds();
  }

#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(WobblyPlane))]
  public class WobblyPlaneEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();
      WobblyPlane plane = (WobblyPlane)target;

      if (GUILayout.Button("Test")) {
        Debug.Log(plane.mesh.triangles);
      }
    }
  }
#endif
}
