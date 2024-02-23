using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveManageData
{
    public int coinTotal = 0;
    public int maxLevelCurrent = 1;
    public int selectedSkin = 0;
    public int maxHP = 5;
    public int speed = 8;
    public int force = 10;
    public Vector3 scale = new Vector3(1,1,1);
    public List<int> checkSkin = new List<int>(1);
    public List<int> myNumItem = new List<int>(1);
}
