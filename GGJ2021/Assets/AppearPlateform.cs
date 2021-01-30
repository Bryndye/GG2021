using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPlateform : MonoBehaviour
{
    [SerializeField] LayerMask layerMagicGround;

    private void Update()
    {
        CheckLayer();
    }

    private void CheckLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Magic"))
        {
            Debug.Log("Target Position: " + hit.collider.gameObject.name);
        } 
    }
}
