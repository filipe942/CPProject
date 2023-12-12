using DungeonKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBarLvL : MonoBehaviour
{
    [SerializeField] Text TextLvl;
    void Start()
    {
        TextLvl.text = PlayerStats.GetInstance().Level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TextLvl.text = PlayerStats.GetInstance().Level.ToString();
    }
}
