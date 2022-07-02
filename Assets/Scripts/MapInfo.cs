using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public static int[,] board;
    public class Block
    {
        public int left;
        public int layer;
        public int length;
        public int id;

        public Block(int _left, int _layer, int _length, int _id)
        {
            left = _left;
            layer = _layer;
            length = _length;
            id = _id;
        }
    }
    public static List<Block> blockList;

    public static float speed = 20f;
    public static bool animFlag = false;

    private void Awake()
    {
        board = new int[20, 12];
        blockList = new List<Block>();
        blockList.Add(new Block(0, 0, 0, 0));
    }

    public static bool Movable(int index, int direction)
    {
        Block p = blockList[index];
        if (direction == 0)
        {
            return p.left > 0 && board[p.layer, p.left - 1] == 0;
        }
        if (direction == 1)
        {
            return p.left + p.length < 12 && board[p.layer, p.left + p.length] == 0;
        }
        return false;
    }

    public static int Move(int index, int direction)
    {
        Block p = blockList[index];
        for (int i = 0; i < p.length; i++)
        {
            board[p.layer, p.left + i] = 0;
        }
        int k;
        if (direction == 0)
        {
            k = p.left;
            for (int i = k; i >= 0; i--)
            {
                if (board[p.layer, i] != 0)
                {
                    break;
                }
                k = i;
            }
        }
        else
        {
            k = p.left + p.length;
            for (int i = k; i < 12; i++)
            {
                if (board[p.layer, i] != 0)
                {
                    break;
                }
                k = i;
            }
            k -= p.length - 1;
        }
        int dis = k - p.left;
        Debug.Log(dis);
        p.left = k;
        for (int i = 0; i < p.length; i++)
        {
            board[p.layer, k + i] = p.id;
        }
        return dis;
    }

    public static void MoveAnimation(GameObject obj, Vector3 direction)
    {
        animFlag = true;
        Hashtable hash = new Hashtable();
        hash.Add("position", obj.transform.position + direction * 2);
        hash.Add("speed", speed);
        hash.Add("easeType", iTween.EaseType.linear);
        hash.Add("onComplete", "SignalFlag");
        iTween.MoveTo(obj, hash);
    }
}
