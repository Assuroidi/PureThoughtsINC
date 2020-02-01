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
    void Start(){
        int thoughtCount = UnityEngine.Random.Range(1,3);
        int i = 0;
        while (i < thoughtCount){
            requiredThoughts.Add((EThought)UnityEngine.Random.Range(1, Enum.GetValues(typeof(EThought)).Length));
            i++;
        }
    }

    public void CompleteOrder(){
        thoughts = GameObject.FindGameObjectsWithTag("thought");
        foreach (GameObject thought in thoughts){
            CheckDesiredThoughtMatch(GetComponent<ThoughtController>().type);
        }
    }

    private void CheckDesiredThoughtMatch(EThought thought){
        if(requiredThoughts.Contains(thought)){
            BudgetManager.AddFunds(100);
        }
        else{
            BudgetManager.AddFunds(-200);
        }
    }
}