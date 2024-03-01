using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveManageData
{
    public int coinTotal = 0, maxLevelCurrent = 1, selectedSkin = 0, maxHP = 5;

    public float speed = 8f, force = 10f;

    public Vector3 scale = new Vector3(1,1,1);

    public List<int> checkSkin = new List<int>(1), myNumItem = new List<int>(1);
}
