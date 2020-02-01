using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    private bool gameOver;
    BudgetManager bmanager;
    public GameObject gameOverStuff;

    /// <summary>
    /// Enables game over text when budget is under zero.
    /// </summary>

    private void Start()
    {
        gameOver = false;
        bmanager = GetComponent<BudgetManager>();
        if (bmanager)
        {
            bmanager.AnnounceBudgetEnd += Bmanager_AnnounceBudgetEnd;
        }
        if (gameOverStuff)
        {
            gameOverStuff.SetActive(false);
        }
    }

    private void Bmanager_AnnounceBudgetEnd()
    {
        gameOver = true;
        if (gameOverStuff)
        {
            gameOverStuff.SetActive(true);
        }
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
