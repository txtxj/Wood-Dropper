using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    public int id;
    public void SignalFlag()
    {
        Debug.Log("Set!");
        MapInfo.animFlag = false;
    }
}
