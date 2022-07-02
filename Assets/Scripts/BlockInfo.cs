using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    public int id;
    public void SignalFlag(int type)
    {
        MapInfo.animFlag += 1;
        if (type == 1)
        {
            MapInfo.Drop();
        }
        else if (type == 2)
        {
            MapInfo.CheckLayer();
        }
    }
}
