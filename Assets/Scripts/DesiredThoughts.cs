using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DesiredThoughts : MonoBehaviour
{
    public List<EThought> requiredThoughts;
    public int lol;
    public GameObject[] thoughts;
    public GameObject gm;
    void Start(){
        gm = GameObject.FindWithTag("GameManager");
        int thoughtCount = UnityEngine.Random.Range(1,3);
        int i = 0;
        while (i < thoughtCount){
            EThought thought = (EThought)UnityEngine.Random.Range(1, Enum.GetValues(typeof(EThought)).Length);
            GameObject item = gameObject.transform.Find("Item"+i).gameObject;
            requiredThoughts.Add(thought);
            SetDesiredThoughSprite(item, thought);
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
        item.GetComponent<SpriteRenderer>().sprite = thoughtSprite;
    }
}