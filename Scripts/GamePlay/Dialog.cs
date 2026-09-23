using System.Collections.Generic;
using UnityEngine;

// Lines of text shown one at a time by the DialogManager
[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;

    public List<string> Lines => lines;
}
