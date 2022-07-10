using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public GameObject blockPrefab;
    
    [Tooltip("Should be 3:5")]
    public Vector2Int size = new Vector2Int(12, 20);

    [Header("Block movement option")]
    public float delay = 0.5f;
    public float speed = 20f;
    
    private int count = 1;
    private TextAsset levelText;
    private Transform blockController;

    private void Awake()
    {
        GameObject levelObj = GameObject.Find("LevelObject");
        if (levelObj == null)
        {
            levelText = Resources.Load<TextAsset>("Levels/level0");
        }
        else
        {
            levelText = Resources.Load<TextAsset>("Levels/level" + levelObj.GetComponent<LevelInfo>().id);
        }
        size = new Vector2Int(levelText.bytes[0], levelText.bytes[1]);

        GameObject obj = GameObject.Find("Obj");
        Transform board = obj.transform.GetChild(1);
        board.GetChild(1).GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", size);
        blockController = board.GetChild(2);
        blockController.localPosition = new Vector3(-20f + 20f / size.y, 0.5f, 12f);
        blockController.localScale = new Vector3(40f / size.y, 40f / size.y, 1f);

        MapInfo.delay = delay;
        MapInfo.speed = speed;
        MapInfo.size = size;
    }

    private void CreateBlock(int left, int layer, int length)
    {
        GameObject block = Instantiate(blockPrefab, blockController);
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

    private void RecoverBlock(int left, int layer, int length)
    {
        GameObject block = MapInfo.blockObjList[count];
        MapInfo.blockList[count].layer = layer;
        MapInfo.blockList[count].left = left;
        block.transform.localScale = new Vector3(length, 1f, 1f);
        block.transform.localPosition = new Vector3(length / 2f + left, layer, 0f);
        block.transform.localRotation = Quaternion.identity;
        block.tag = "Block";
        block.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        block.GetComponent<Rigidbody>().useGravity = false;
        for (int i = 0; i < length; i++)
        {
            MapInfo.board[layer, left + i] = count;
        }
        count += 1;
    }

    private void DecodeLevel(byte[] level, Action<int, int, int> blockHandler)
    {
        int layer = 0, left = 0;
        int len = level.Length;
        
        for (int i = 2; i < len; i++)
        {
            if (level[i] == 0)
            {
                layer += 1;
                left = 0;
            }
            else if (level[i] > 127)
            {
                left += 256 - level[i];
            }
            else
            {
                blockHandler(left, layer, level[i]);
                left += level[i];
            }
        }
    }

    private void Start()
    {
        DecodeLevel(levelText.bytes, CreateBlock);
        AudioController.PlayAudio(0);
    }

    public void Retry()
    {
        if (MapInfo.animFlag < 0)
        {
            return;
        }
        count = 1;
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                MapInfo.board[i, j] = 0;
            }
        }
        DecodeLevel(levelText.bytes, RecoverBlock);
        AudioController.PlayAudio(0);
    }
}
