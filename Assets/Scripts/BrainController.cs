using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainController : MonoBehaviour {
  public GameObject brain_model = null;

  private GameObject brains = null;
  public float hide_distance = -5;

  void Start() {}

  void Update() {
    if (brains != null && WobblyPlane.current.center.z < hide_distance) {
      Destroy(brains);
      brains = null;
    }
  }

  void NextBrains() {
    if (brains == null) brains = Instantiate(brain_model);
    else WobblyPlane.current.RemoveFloor();
  }

#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(BrainController))]
  public class BrainControllerEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();
      BrainController controller = (BrainController)target;

      if (GUILayout.Button("Next brains")) controller.NextBrains();
    }
  }
#endif
}
