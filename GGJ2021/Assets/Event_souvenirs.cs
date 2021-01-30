using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_souvenirs : MonoBehaviour
{
    public int Index;
    public int IndexMax;
    [Header("Activate")]
    [SerializeField] GameObject[] someThings;

    [Header("ActivePorte")]
    public bool PorteEnd;
    [SerializeField] Sprite[] porteSprites;
    [SerializeField] SpriteRenderer sr;

    private void Awake()
    {
        for (int i = 0; i < someThings.Length; i++)
        {
            someThings[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magic"))
        {
            Index++;
            print(Index);
        }
    }

    private void Update()
    {
        if (Index >= IndexMax && someThings.Length > 0)
        {
            for (int i = 0; i < someThings.Length; i++)
            {
                someThings[i].SetActive(true);
            }
        }
        if (PorteEnd)
        {
            sr.sprite = porteSprites[Index];
        }
    }
}
