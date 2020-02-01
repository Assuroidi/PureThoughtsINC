﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Node {
  private WobblyPlane parent;
  public Vector3 location = new Vector3();
  public Vector3 speed = new Vector3();
  float plane = 0;

  public Node(WobblyPlane parent, Vector3 start) {
    this.parent = parent;
    location = start;
    plane = start.y;
  }

  public void Update() {
    speed += parent.gravity * Time.deltaTime;
    speed *= (float)(1.0d - parent.resistance * Time.deltaTime);
    location += speed * Time.deltaTime;
    location.y = plane;
    if (location.z < parent.floor && speed.z < 0) {
      location.z -= (float)(location.z - parent.floor);
      speed.z = -speed.z;
    }
  }
}

public class Connection {
  private WobblyPlane parent;
  public int start, end;
  public double size;
  float force = 1.0f;

  public Connection(WobblyPlane parent, int start, int end, float force) {
    this.parent = parent;
    this.start = start;
    this.end = end;
    this.force = force;

    this.size = (parent.nodes[end].location - parent.nodes[start].location).magnitude;

//    Debug.Log("Connection: " + start + " - " + end + ": " + size); //XXX
  }

  public void Update() {
    Vector3 vec = parent.nodes[end].location - parent.nodes[start].location;
    double mag = size - vec.magnitude;
    if (mag != 0) {
      Vector3 frc = vec.normalized * (float)mag;
//      Debug.Log("Vector: " + vec + " * Magnitude: " + mag + " = Force: " + frc); //XXX
      parent.nodes[start].location -= frc * 0.49f;
      parent.nodes[end].location += frc * 0.49f;
      parent.nodes[start].speed -= frc * force * parent.jelly_force;
      parent.nodes[end].speed += frc * force * parent.jelly_force;
    }
  }
}

public class WobblyPlane : MonoBehaviour {
  public Vector3 gravity = new Vector3(0, 0, -9.0f);
  public float jelly_force = 10.0f;
  public double resistance = 0.1f;
  public double floor = -6.0;

  public Mesh mesh;
  public Node[] nodes;
  public Connection[] connections;

  void Start() {
    mesh = GetComponent<MeshFilter>().mesh;

    Vector3 average = new Vector3();
    nodes = new Node[mesh.vertices.Length + 1];
    for (int i = 0; i < mesh.vertices.Length; i++) {
      nodes[i] = new Node(this, mesh.vertices[i]);
      average += mesh.vertices[i];
    }
    average /= mesh.vertices.Length;
    nodes[nodes.Length - 1] = new Node(this, average);

    int[] tls = mesh.triangles;
    HashSet<(int, int, float)> edges = new HashSet<(int, int, float)>();
    for (int i = 0; i < tls.Length; i += 3) {
      int v1a = tls[i],   v1b = tls[i+1];
      if (v1a > v1b) (v1a, v1b) = (v1b, v1a);
      edges.Add((v1a, v1b, 1.0f));
      int v2a = tls[i+1], v2b = tls[i+2];
      if (v2a > v2b) (v2a, v2b) = (v2b, v2a);
      edges.Add((v2a, v2b, 1.0f));
      int v3a = tls[i+2], v3b = tls[i];
      if (v3a > v3b) (v3a, v3b) = (v3b, v3a);
      edges.Add((v3a, v3b, 1.0f));
//      Debug.Log("Edge: " + v1a + ' ' + v1b); //XXX
    }
    for (int i = 0; i < mesh.vertices.Length; i++) edges.Add((i, nodes.Length - 1, 2.0f));

    connections = new Connection[edges.Count];
    int edge_index = 0;
    foreach ((int, int, float) tp in edges) connections[edge_index++] = new Connection(this, tp.Item1, tp.Item2, tp.Item3);
  }

  void Update() {
    foreach (Node nd in nodes) nd.Update();
    Vector3 average = new Vector3();
    for (int i = 0; i < mesh.vertices.Length; i++) average += nodes[i].location;
    average /= mesh.vertices.Length;
//    nodes[nodes.Length - 1] = new Node(this, average);
    foreach (Connection cnt in connections) cnt.Update();
 
    Vector3[] nVec = new Vector3[nodes.Length];
    for (int i = 0; i < nodes.Length; i++) nVec[i] = nodes[i].location;

    mesh.vertices = nVec;
    mesh.RecalculateBounds();
  }

#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(WobblyPlane))]
  public class WobblyPlaneEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();
      WobblyPlane plane = (WobblyPlane)target;

      if (GUILayout.Button("Poke")) {
        Debug.Log(plane.mesh.triangles);
      }
    }
  }
#endif
}