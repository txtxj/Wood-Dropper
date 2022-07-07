using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelButtonPrefab;

    public int levelCount = 16;

    private ButtonManager btManager;

    private void Start()
    {
        int row = 0;
        int column = 0;
        btManager = GameObject.Find("EventSystem").GetComponent<ButtonManager>();
        for (int id = 0; id < levelCount; id++)
        {
            if (Resources.Load<TextAsset>("Levels/level" + id) == null)
            {
                return;
            }
            GameObject bt = Instantiate(levelButtonPrefab, transform);
            bt.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-270f + 180f * column, 300f - 180f * row, 0f);
            bt.GetComponentInChildren<Text>().text = id.ToString();
            bt.name = id.ToString();
            bt.GetComponent<Button>().onClick.AddListener(delegate
            {
                btManager.LoadLevel(bt.name);
            });
            column += 1;
            row += column / 4;
            column %= 4;
        }
    }
}
