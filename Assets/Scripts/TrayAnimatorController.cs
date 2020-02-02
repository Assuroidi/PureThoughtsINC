using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayAnimatorController : MonoBehaviour {
  public static TrayAnimatorController current = null;

  public Animator anim;
  public bool trayOpen;
  public bool nextBrains;

  void Start() {
    current = this;
    anim = GetComponent<Animator>();
    trayOpen = false;
    nextBrains = false;
  }

  void Update() {
//    anim.SetBool("Next_Brains", nextBrains);
  }

  public void OpenTray() {
    anim.SetTrigger("Tray_auki");
    Debug.Log("Tray opened");
    trayOpen = true;
  }

  public void CloseTray() {
    anim.SetTrigger("Tray_kii");
    Debug.Log("Tray closed");
    trayOpen = false;
  }
}
