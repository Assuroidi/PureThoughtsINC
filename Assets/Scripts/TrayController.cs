using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayController : MonoBehaviour {
  private Animator animator = null;

  void Start() {
    animator = GetComponent<Animator>();
    animator.SetTrigger("open_tray");
  }

  void Update() {
  }
}
