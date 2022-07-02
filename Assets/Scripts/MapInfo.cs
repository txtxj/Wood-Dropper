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
    public static List<GameObject> blockObjList;

    public static float speed = 20f;
    public static int animFlag = 0;

    private void Awake()
    {
        board = new int[20, 12];
        blockList = new List<Block>();
        blockObjList = new List<GameObject>();
        blockList.Add(null);
        blockObjList.Add(null);
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

    public static void Move(int index, int direction)
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
        p.left = k;
        for (int i = 0; i < p.length; i++)
        {
            board[p.layer, k + i] = p.id;
        }
        MoveAnimation(blockObjList[index], Vector3.right * dis, 1);
    }

    public static void MoveAnimation(GameObject obj, Vector3 direction, int type)
    {
        animFlag -= 1;
        Hashtable hash = new Hashtable();
        hash.Add("position", obj.transform.position + direction * 2);
        hash.Add("speed", speed);
        hash.Add("easeType", iTween.EaseType.linear);
        hash.Add("onComplete", "SignalFlag");
        hash.Add("onCompleteParams", type);
        iTween.MoveTo(obj, hash);
    }

    public static void Drop()
    {
        for (int i = 1; i < 20; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (board[i, j] != 0)
                {
                    int k = board[i, j];
                    int depth = i;
                    for (int u = i - 1; u >= 0 && depth == u + 1; u--)
                    {
                        depth = u;
                        for (int v = 0; v < blockList[k].length; v++)
                        {
                            if (board[u, j + v] != 0)
                            {
                                depth += 1;
                                break;
                            }
                        }
                    }
                    if (depth != i)
                    {
                        for (int u = 0; u < blockList[k].length; u++)
                        {
                            board[i, blockList[k].left + u] = 0;
                            board[depth, blockList[k].left + u] = k;
                        }
                        blockList[k].layer = depth;
                        MoveAnimation(blockObjList[k], Vector3.down * (i - depth), 2);
                    }
                    j += blockList[k].length - 1;
                }
            }
        }
    }

    public static void CheckLayer()
    {
        for (int i = 0; i < 20; i++)
        {
            int j;
            for (j = 0; j < 12; j++)
            {
                if (board[i, j] == 0)
                {
                    break;
                }
            }
            if (j == 12)
            {
                int k = 0;
                for (j = 0; j < 12; j++)
                {
                    if (board[i, j] != k)
                    {
                        k = board[i, j];
                        blockObjList[k].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        blockObjList[k].GetComponent<Rigidbody>().useGravity = true;
                        MoveAnimation(blockObjList[k], Vector3.back * 2f + new Vector3(Random.Range(0.4f, 0.8f), Random.Range(0.4f, 0.8f), Random.Range(0.4f, 0.8f)), j == 0 ? 0 : 1);
                    }
                    board[i, j] = 0;
                }
                return;
            }
        }
    }
}
