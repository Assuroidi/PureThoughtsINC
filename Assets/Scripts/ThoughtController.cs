using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ThoughtController : MonoBehaviour
{
    public Vector3 pos;
    public float velx;
    public float vely;
    public string type;
    BoxCollider2D col2D;

    public Vector2 screenBounds;
    public Vector2 screenOrigo;
    // Start is called before the first frame update
    void Start()
    {
        velx = Random.Range(-5.0f, 5.0f);
        vely = Random.Range(-5.0f, 5.0f);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        col2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velx*Time.deltaTime, vely*Time.deltaTime, 0);
        pos = transform.position;
        if (pos.x + col2D.size.x/2 > screenBounds.x || pos.x - col2D.size.x/2< screenOrigo.x)
        {
            velx *= -1;
        }
        else if (pos.y + col2D.size.y/2 > screenBounds.y || pos.y - col2D.size.y/2 < screenOrigo.y)
        {
            vely *= -1;
        }
    }
}
