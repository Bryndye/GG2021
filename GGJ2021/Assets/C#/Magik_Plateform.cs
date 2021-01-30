using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magik_Plateform : MonoBehaviour
{
    BoxCollider2D b;
    SpriteRenderer sr;
    private void Awake() { 
        b = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        Desactive();
    }

    public void ActiveMe(float t)
    {
        if (b.isTrigger)
        {
            sr.color = new Color32(255, 255, 255, 255);
            b.isTrigger = false;
            Invoke(nameof(Desactive), t);
        }
    }

    private void Desactive()
    {
        sr.color = new Color32(255,255,255, 150);
        b.isTrigger = true;
    }
}
