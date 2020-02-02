using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ThoughtController : MonoBehaviour
{
    public Vector3 pos;
    public float velx;
    public float vely;
    public float health;
    BoxCollider2D col2D;
    private ToolSelection gmToolScript;
    private bool thoughtControlEnabled;
    public Vector2 screenBounds;
    public Vector2 screenOrigo;
    public EThought type;
    public bool beingScrubbed;
    public int slowdownMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        type = (EThought)Random.Range(1,6);
        SetThoughSprite(gameObject, type);
        thoughtControlEnabled = false;
        velx = Random.Range(-5.0f, 5.0f);
        vely = Random.Range(-5.0f, 5.0f);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        col2D = GetComponent<BoxCollider2D>();
        gmToolScript = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<ToolSelection>(); ;
        if (gmToolScript)
        {
            gmToolScript.AnnounceToolChanged += GmToolScript_AnnounceToolChanged; ;
        }
        else
        {
            Debug.LogWarning("ThoughtController could not find GameManager's ToolSelection script!");
        }
    }

        private void GmToolScript_AnnounceToolChanged(ETool toolEnum)
    {
        switch (toolEnum)
        {
            case ETool.None:
                thoughtControlEnabled = false;
                break;
            case ETool.Sponge:
                thoughtControlEnabled = true;
                break;
            default:
                thoughtControlEnabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (beingScrubbed){
            slowdownMultiplier = 10;
            health -= 5*Time.deltaTime;
        }else{
            slowdownMultiplier = 1;
        }
        transform.Translate(velx/slowdownMultiplier*Time.deltaTime, vely/slowdownMultiplier*Time.deltaTime, 0);
        pos = transform.position;
        if (pos.x + col2D.size.x/2 > screenBounds.x || pos.x - col2D.size.x/2< screenOrigo.x)
        {
            velx *= -1;
        }
        else if (pos.y + col2D.size.y/2 > screenBounds.y || pos.y - col2D.size.y/2 < screenOrigo.y)
        {
            vely *= -1;
        }
        if (health < 0){
            Destroy(gameObject);
        }
    }

    public void OnClick(){
        if (thoughtControlEnabled){
            beingScrubbed = true;
        }
        beingScrubbed = false;
    }
    public void OnRelease(){
        beingScrubbed = false;
    }

    private void SetThoughSprite(GameObject item, EThought thought){
        Sprite thoughtSprite = Resources.Load(thought.ToString("f"), typeof(Sprite)) as Sprite;
        Debug.Log(thoughtSprite);
        SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
        sprite.sprite = thoughtSprite;
    }
}
