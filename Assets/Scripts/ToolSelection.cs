using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Replaces mouse sprite with selected tool.
/// </summary>

public class ToolSelection : MonoBehaviour
{
    public ETool selectedTool;
    public delegate void ToolSelectionEvent(ETool toolEnum);
    public event ToolSelectionEvent AnnounceToolChanged;

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

    

}
