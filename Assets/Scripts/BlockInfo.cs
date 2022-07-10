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
            AudioController.PlayAudio(1);
            MapInfo.Drop();
        }
        else if (type == 2)
        {
            AudioController.PlayAudio(1);
            MapInfo.CheckLayer();
        }
        else if (type == 3)
        {
            AudioController.PlayAudio(0);
            MapInfo.Drop();
        }
    }
}
