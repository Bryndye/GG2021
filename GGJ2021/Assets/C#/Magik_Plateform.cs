using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magik_Plateform : MonoBehaviour
{
    BoxCollider2D b;
    public SpriteRenderer[] sr;
    private void Awake() { 
        b = GetComponent<BoxCollider2D>();
        Desactive();
    }

    public void ActiveMe(float t)
    {
        if (b.isTrigger)
        {
            if (sr.Length > 0)
            {
                for (int i = 0; i < sr.Length; i++)
                {
                    sr[i].color = new Color32(255, 255, 255, 255);
                }
            } 
            b.enabled = true;
			b.isTrigger = false;
			Invoke(nameof(Desactive), t);
        }
    }

    private void Desactive()
    {
        if (sr.Length >0)
        {
            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = new Color32(255, 255, 255, 150);
            }
        }
        //b.enabled = false;
		b.isTrigger = true;
	}
}
