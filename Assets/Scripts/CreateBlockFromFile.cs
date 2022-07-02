using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CreateBlockFromFile : MonoBehaviour
{
    public GameObject blockPrefab;
    private int count = 1;

    private void CreateBlock(int left, int layer, int length)
    {
        GameObject block = Instantiate(blockPrefab, transform);
        block.transform.localScale = new Vector3(length, 1f, 1f);
        block.transform.localPosition = new Vector3(length / 2f + left, layer, 0f);
        MapInfo.blockList.Add(new MapInfo.Block(left, layer, length, count));
        MapInfo.blockObjList.Add(block);
        block.GetComponent<BlockInfo>().id = count;
        for (int i = 0; i < length; i++)
        {
            MapInfo.board[layer, left + i] = count;
        }
        count += 1;
    }

    private void DecodeLevel(byte[] level)
    {
        int layer = 0, left = 0;
        foreach (byte length in level)
        {
            if (length == 0)
            {
                layer += 1;
                left = 0;
            }
            else if (length > 127)
            {
                left += 256 - length;
            }
            else
            {
                CreateBlock(left, layer, length);
                left += length;
            }
        }
    }

    private void Start()
    {
        TextAsset levelText = Resources.Load<TextAsset>("Levels/level0");
        DecodeLevel(levelText.bytes);
    }
}
