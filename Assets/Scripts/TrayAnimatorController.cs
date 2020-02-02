using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayAnimatorController : MonoBehaviour
{
    public Animator anim;
    public bool trayOpen;
    public bool nextBrains;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        trayOpen = false;
        nextBrains = Input.GetMouseButton(0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed left click.");
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log("Pressed right click.");
        }

        if (Input.GetMouseButton(2))
        {
            Debug.Log("Pressed middle click.");
        }
        */
        
        if (nextBrains) //next brains button is pressed
        {
            anim.SetTrigger("Tray_auki");
            Debug.Log("Pressed next brains.");
            trayOpen = true;
        }
        else if (trayOpen = true && nextBrains) //and next brains button is pressed
        {
            anim.SetTrigger("Tray_kii");
        }
        else
        {
            anim.SetBool("Next_Brains", nextBrains);
        }

        /*
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                animator.SetTrigger("Die");
            }
        }
        */
    }
}
