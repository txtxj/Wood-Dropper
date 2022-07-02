using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClickAndMove : MonoBehaviour
{
	private bool isClick;
	private int curindex = 0;
	private GameObject curObj;
	private Vector3 oriMousePos;
	private Camera mainCamera;
	
	public float threshold = 5f;

	private void Start()
	{
		mainCamera = Camera.main;
		isClick = false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				curObj = hit.transform.gameObject;
				curindex = curObj.GetComponent<BlockInfo>().id;
				oriMousePos = Input.mousePosition;
			}
			isClick = true;
		}
		if (Input.GetMouseButtonUp(0) || MapInfo.animFlag)
		{
			isClick = false;
		}
		if (isClick)
		{
			if (curindex != 0)
			{
				Vector3 curMousePos = Input.mousePosition;
				Vector3 mouseOffset = curMousePos - oriMousePos;
				int direction = mouseOffset.x > 0 ? 1 : 0;
				if (Mathf.Abs(mouseOffset.x) > threshold && MapInfo.Movable(curindex, direction))
				{
					int dis = MapInfo.Move(curindex, direction);
					MapInfo.MoveAnimation(curObj, new Vector3(dis, 0f, 0f));
					isClick = false;
				}
			}
		}
	}
}
