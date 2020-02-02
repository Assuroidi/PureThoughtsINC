using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Functions as a count down timer.
/// </summary>

public class BudgetManager : MonoBehaviour
{
    public int startingFunds;
    [SerializeField]
    private int currentFunds;
    public float decreasingInterval;
    public int decreasingAmount;
    public event Events.SomethingHappened AnnounceBudgetEnd;
    public Text budgetText;

    public void StartBudgeting()
    {
        if (startingFunds <= 0)
        {
            startingFunds = 50000;
        }
        currentFunds = startingFunds;
        if (decreasingAmount == 0)
        {
            decreasingAmount = 5000;
        }
        if (decreasingInterval == 0)
        {
            decreasingInterval = 5f;
        }
        StartCoroutine(StartCountingDown(decreasingAmount, decreasingInterval));
        UpdateBudgetText();
    }

    IEnumerator StartCountingDown(int amount = 5000, float speed = 5f)
    {
        while (currentFunds > 0)
        {
            yield return new WaitForSeconds(speed);
            currentFunds -= amount;
            UpdateBudgetText();
        }
        AnnounceBudgetEnd?.Invoke();
    }

    private void UpdateBudgetText()
    {
        if (budgetText)
        {
            budgetText.text = "Budget: " + currentFunds;
        }
    }

    public void AddFunds(int amount)
    {
        currentFunds += amount;
    }

}
