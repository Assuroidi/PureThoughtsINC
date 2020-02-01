using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DesiredThoughts : MonoBehaviour
{
    public List<EThought> requiredThoughts;
    public int lol;
    void Start(){
        int thoughtCount = UnityEngine.Random.Range(1,3);
        int i = 0;
        while (i < thoughtCount){
            requiredThoughts.Add((EThought)UnityEngine.Random.Range(1, Enum.GetValues(typeof(EThought)).Length));
            i++;
        }
    }
}