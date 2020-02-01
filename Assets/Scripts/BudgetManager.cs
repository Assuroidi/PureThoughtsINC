using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    public int startingFunds;
    [SerializeField]
    private int currentFunds;
    public float decreasingSpeed;
    public int decreasingAmount;
    public Events.SomethingHappened AnnounceBudgetEnd;

    // Start is called before the first frame update
    void Start()
    {
        if (startingFunds <= 0)
        {
            startingFunds = 50000;
        }
        currentFunds = startingFunds;
        if (decreasingAmount <= 0 || decreasingSpeed <= 0)
        {
            StartCoroutine(StartCountingDown());
        }
        else
        {
            StartCoroutine(StartCountingDown(decreasingAmount, decreasingSpeed));
        }
    }

    IEnumerator StartCountingDown(int amount = 5000, float speed = 5f)
    {
        while (currentFunds > 0)
        {
            currentFunds -= amount;
            yield return new WaitForSeconds(speed);
        }
        AnnounceBudgetEnd?.Invoke();
    }

    public void AddFunds(int amount)
    {
        currentFunds += amount;
    }

}
