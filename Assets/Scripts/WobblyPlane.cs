﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Node {
  private WobblyPlane parent;
  public Vector3 location = new Vector3();
  public Vector3 speed = new Vector3();
  public Vector3 force = new Vector3();
  public float connections = 0.0f;
  private Vector3 start;
  bool backbone = false;

  public Node(WobblyPlane parent, Vector3 start, bool backbone=false) {
    this.backbone = backbone;
    this.parent = parent;
    this.start = location;
    this.location = start;

    speed = parent.gravity * 1.0f;
  }

  public void Update() {
    speed += parent.gravity * Time.deltaTime + force;
    speed *= (float)(1.0d - parent.resistance * Time.deltaTime);
    if (parent.firstGround && parent.has_floor && speed.magnitude > parent.max_magnitude)
      speed = speed.normalized * parent.max_magnitude;
    location += speed * Time.deltaTime;
    force = new Vector3();
    location.y = start.y;
//    if (backbone) location.x = start.x;
    if (parent.has_floor && location.z < parent.floor && speed.z < 0) {
      parent.firstGround = true;
      location.z = (float)parent.floor;
      speed.z = -speed.z;
    }
    force = new Vector3();
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

    parent.nodes[start].connections += 1.0f;
    parent.nodes[end].connections += 1.0f;

//    Debug.Log("Connection: " + start + " - " + end + ": " + size); //XXX
  }

  public void Update() {
    Vector3 vec = parent.nodes[end].location - parent.nodes[start].location;
    double mag = size - vec.magnitude;
    if (mag != 0) {
      Vector3 frc = vec.normalized * (float)mag;
      parent.nodes[start].force -= frc * force * parent.jelly_force;
      parent.nodes[end].force += frc * force * parent.jelly_force;
    }
  }
}

public class WobblyPlane : MonoBehaviour {
  public static WobblyPlane current;
  private static System.Random rnd = new System.Random();

  public Vector3 center = new Vector3();
  public Vector3 gravity = new Vector3(0, 0, -0.9f);
  public float jelly_force = 0.2f;
  public double resistance = 0.5f;
  public double floor = -3.0;
  public float max_magnitude = 0.5f;
  public bool has_floor = true;
  public bool firstGround = false;

  public Mesh mesh;
  public Node[] nodes;
  public Connection[] connections;

  private float time = 0.0f, next = 0.0f;

  void Start() {
    current = this;
    mesh = GetComponent<MeshFilter>().mesh;
    firstGround = false;

    Vector3 average = new Vector3();
    nodes = new Node[mesh.vertices.Length];
    for (int i = 0; i < mesh.vertices.Length; i++) {
      nodes[i] = new Node(this, mesh.vertices[i]);
      average += mesh.vertices[i];
    }
    average /= mesh.vertices.Length;

    HashSet<(int, int, float)> edges = new HashSet<(int, int, float)>();
    for (int ei = 0; ei < mesh.vertices.Length; ei++)
      for (int si = 0; si < ei; si++)
        edges.Add((si, ei, 1.0f));

    connections = new Connection[edges.Count];
    int edge_index = 0;
    foreach ((int, int, float) tp in edges)
      connections[edge_index++] = new Connection(this, tp.Item1, tp.Item2, tp.Item3);
  }

  void Update() {
    time += Time.deltaTime;
    if (time > next) {
//      Debug.Log("Timed poke");
      RandomPoke();
      next += UnityEngine.Random.Range(0.0f, 0.05f);
    }

    foreach (Connection c in connections) if (c != null) c.Update();
    foreach (Node nd in nodes) nd.Update();
    center = new Vector3();
    foreach (Node nd in nodes) center += nd.location;
    center /= (float)nodes.Length;
 
    Vector3[] nVec = new Vector3[nodes.Length];
    for (int i = 0; i < nodes.Length; i++) nVec[i] = nodes[i].location;

    mesh.vertices = nVec;
    mesh.RecalculateBounds();
  }

  void Poke(Vector3 point, Vector3 force) {
    if (force == null) force = new Vector3(
      UnityEngine.Random.Range(-0.03f, 0.03f), 0.0f, UnityEngine.Random.Range(-0.03f, 0.03f));
    int index = 0;
    float range = (point - nodes[index].location).magnitude;
    for (int i = 0; i < nodes.Length; i++)
      if ((point - nodes[0].location).magnitude < range)
        (range, index) = ((point - nodes[0].location).magnitude, i);
    nodes[index].speed += force;
  }

  void RandomPoke() {
    int i = rnd.Next(nodes.Length);
    nodes[i].speed += new Vector3(
      UnityEngine.Random.Range(-0.3f, 0.3f), 0.0f, UnityEngine.Random.Range(-0.3f, 0.3f));
  }

  public void RemoveFloor() { has_floor = false; }
  public void AddFloor() { has_floor = true; }

#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(WobblyPlane))]
  public class WobblyPlaneEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();
      WobblyPlane plane = (WobblyPlane)target;

      if (GUILayout.Button("Poke")) WobblyPlane.current.RandomPoke();

      if (GUILayout.Button("Drop")) WobblyPlane.current.RemoveFloor();
    }
  }
#endif
}
