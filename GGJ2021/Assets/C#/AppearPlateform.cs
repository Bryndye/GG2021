﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPlateform : MonoBehaviour
{
    CanvasManager cm;
    [SerializeField] Transform light_mouse;

    [SerializeField] float timeActivate = 2;

    private void Awake()
    {
        cm = CanvasManager.Instance;
    }
    private void Update()
    {
        if (cm == null)
        {
            cm = CanvasManager.Instance;
        }
        CheckLayer();
        light_mouse.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void CheckLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Magic"))
        {
            Magik_Plateform mp = hit.collider.GetComponent<Magik_Plateform>();
            if (mp.activated)
            {
                if (cm.ClickOniT != null)
                {
                    cm.ClickOniT.SetActive(false);
                }
                light_mouse.gameObject.SetActive(false);
            }
            else
            {
                if (cm.ClickOniT != null)
                {
                    cm.ClickOniT.SetActive(true);
                }
                light_mouse.gameObject.SetActive(true);
            }
            //Debug.Log("Target Position: " + hit.collider.gameObject.name);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mp.ActiveMe(timeActivate);
            }
        }
        else
        {
            if (cm.ClickOniT != null)
            {
                cm.ClickOniT.SetActive(false);
            }
            light_mouse.gameObject.SetActive(false);
        }
    }
}
