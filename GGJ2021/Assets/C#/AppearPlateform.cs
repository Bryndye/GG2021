using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPlateform : MonoBehaviour
{
    [SerializeField] LayerMask layerMagicGround;
    [SerializeField] Transform light_mouse;

    [SerializeField] float timeActivate = 2;
    private void Update()
    {
        CheckLayer();
        light_mouse.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void CheckLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Magic"))
        {
            light_mouse.gameObject.SetActive(true);
            //Debug.Log("Target Position: " + hit.collider.gameObject.name);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hit.collider.GetComponent<Magik_Plateform>().ActiveMe(timeActivate);
            }

        }
        else
        {
            light_mouse.gameObject.SetActive(false);
        }
    }
}
