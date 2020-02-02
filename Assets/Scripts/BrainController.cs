using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainController : MonoBehaviour {
  public GameObject brain_model = null;

  private GameObject brains = null;
  public float hide_distance = -5;

  void Start() {
        if (TrayAnimatorController.current != null) TrayAnimatorController.current.CloseTray();
    }

  void Update() {
    if (brains != null && WobblyPlane.current && WobblyPlane.current.center.z < hide_distance) {
      if (TrayAnimatorController.current != null) TrayAnimatorController.current.CloseTray();
      Destroy(brains);
      brains = null;
    }
  }

  public void NextBrains() {
    if (brains == null) {
      brains = Instantiate(brain_model);
      if (TrayAnimatorController.current != null) TrayAnimatorController.current.CloseTray();
    } else {
      WobblyPlane.current.RemoveFloor();
      if (TrayAnimatorController.current != null) TrayAnimatorController.current.OpenTray();
    }
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
