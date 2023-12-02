using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ToDo : MonoBehaviour
{
    public string title;
    public string description;
    public DateTime endDate;
    public int difficulty;
}
