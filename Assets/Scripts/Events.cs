using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void SomethingHappened();
    public delegate void ToolSelectionEvent(ETool toolEnum);
}
