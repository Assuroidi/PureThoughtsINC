using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Replaces mouse sprite with selected tool.
/// </summary>

public class ToolSelection : MonoBehaviour
{
    public ETool selectedTool;
    public event Events.ToolSelectionEvent AnnounceToolChanged;

    // Start is called before the first frame update
    void Start()
    {
        SelectTool(ETool.None);

    }

    public void SelectTool(ETool tool)
    {
        if (tool == ETool.Error)
        {
            Debug.LogWarning("Invalid tool selection!");
            return;
        }
        selectedTool = tool;
        AnnounceToolChanged?.Invoke(tool);
    }

    /// <summary>
    /// Sets the given sprite as the mouse cursor.
    /// </summary>
    /// <param name="texture"></param>
    internal void ChangeSprite(Texture2D texture = null)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }
}
