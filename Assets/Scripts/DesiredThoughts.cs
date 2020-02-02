using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DesiredThoughts : MonoBehaviour
{
    public List<EThought> requiredThoughts;
    public GameObject[] thoughts;
    public GameObject gm;
    private GameObject item;
    private EThought t;
    void Start(){
        gm = GameObject.FindWithTag("GameManager");
        int thoughtCount = UnityEngine.Random.Range(1,4);
        int i = 0;
        while (i < thoughtCount){
            t = (EThought)UnityEngine.Random.Range(1, Enum.GetValues(typeof(EThought)).Length);
            if (!requiredThoughts.Contains(t)){
                requiredThoughts.Add(t);
            }else{
                continue;
            }
            item = transform.Find("Item"+i).gameObject;
            SetDesiredThoughSprite(item, t);
            i++;
        }
    }

    public void CompleteOrder(){
        thoughts = GameObject.FindGameObjectsWithTag("thought");
        if (thoughts.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'thought'");
        }
        else
        {
            foreach (GameObject thought in thoughts){
                CheckDesiredThoughtMatch(thought.GetComponent<ThoughtController>().type);
            }
        }
    }

    private void CheckDesiredThoughtMatch(EThought thought){
        if(requiredThoughts.Contains(thought)){
            gm.GetComponent<BudgetManager>().AddFunds(100);
            Debug.Log("löyty!");
        }
        else{
            gm.GetComponent<BudgetManager>().AddFunds(-200);
            Debug.Log("ei löytyny!");
        }
    }
    private void SetDesiredThoughSprite(GameObject item, EThought thought){
        Sprite thoughtSprite = Resources.Load(thought.ToString("f"), typeof(Sprite)) as Sprite;
        Debug.Log(thoughtSprite);
        SpriteRenderer sprite = item.GetComponent<SpriteRenderer>();
        sprite.sprite = thoughtSprite;
    }
}