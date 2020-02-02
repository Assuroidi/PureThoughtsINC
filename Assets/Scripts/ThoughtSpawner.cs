using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtSpawner : MonoBehaviour
{

    public GameObject thought;
    private ToolSelection gmToolScript;
    private bool thoughtSpawningEnabled;
    // Start is called before the first frame update
    void Start()
    {
        thoughtSpawningEnabled = false;
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

    // Update is called once per frame
    void Update()
    {

    }

    private void GmToolScript_AnnounceToolChanged(ETool toolEnum)
    {
        switch (toolEnum)
        {
            case ETool.None:
                thoughtSpawningEnabled = false;
                break;
            case ETool.LobotomyPick:
                thoughtSpawningEnabled = true;
                break;
            default:
                thoughtSpawningEnabled = false;
                break;
        }
    }

    public void OnClick(){
        //Instantiate(thought);
        if (thoughtSpawningEnabled){
            int thoughtCount = Random.Range(1,3);
            for (int i = 0; i < thoughtCount; i++){
                Instantiate(thought);
            }
            Debug.Log("What happen?");
        }
    }
}
