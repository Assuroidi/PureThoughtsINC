﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Click this to select tool.
/// </summary>

public class ToolHolder : MonoBehaviour
{
    public ETool originalTool;
    [SerializeField]
    private ETool currentTool;
    private SpriteRenderer rend;
    public Color HasToolColor;
    public Color EmptyColor;
    private ToolSelection gmToolScript;
    public Texture2D mouseToolSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentTool = originalTool;
        rend = gameObject.GetComponent<SpriteRenderer>();
        gmToolScript = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<ToolSelection>();
        if (gmToolScript)
        {
            gmToolScript.AnnounceToolChanged += GmToolScript_AnnounceToolChanged;
        }
        else
        {
            Debug.LogWarning("ToolHolder could not find GameManager's ToolSelection script!");
        }
    }

    private void OnDestroy()
    {
        if (gmToolScript)
        {
            gmToolScript.AnnounceToolChanged -= GmToolScript_AnnounceToolChanged;
        }
    }

    /// <summary>
    /// This should reset the tool, when a different tool is selected while the original tool is already selected (thus current tool being none).
    /// </summary>
    /// <param name="toolEnum"></param>

    private void GmToolScript_AnnounceToolChanged(ETool toolEnum)
    {
        if (currentTool == ETool.None && toolEnum != originalTool)
        {
            ToolHelper(originalTool, HasToolColor);
        }
    }

    public void ToggleTool()
    {
        if (currentTool == ETool.None)
        {
            ToolHelper(originalTool, HasToolColor);
            gmToolScript.SelectTool(ETool.None);
            gmToolScript.ChangeSprite();
        }
        else
        {
            ToolHelper(ETool.None, EmptyColor);
            gmToolScript.SelectTool(originalTool);
            gmToolScript.ChangeSprite(mouseToolSprite);
        }
    }

    /// <summary>
    /// Changes are made only locally! Call gmToolScript separately when needed!
    /// </summary>
    /// <param name="tool"></param>
    /// <param name="color"></param>

    private void ToolHelper(ETool tool, Color color)
    {
        currentTool = tool;
        rend.color = color;
    }
}
