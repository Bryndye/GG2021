using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_souvenirs : MonoBehaviour
{
    public int Index;
    public int IndexMax;
    [SerializeField] Transform spawnSouv;

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

    public void SendSouvenir(Player_movement pm)
    {
        Index++;
        if (PorteEnd)
        {
            sr.GetComponent<Animator>().SetTrigger("Door");
        }
        Instantiate(Resources.Load<GameObject>("Particle_souvenir"), spawnSouv.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Index >= IndexMax && someThings.Length > 0)
        {
            for (int i = 0; i < someThings.Length; i++)
            {
                someThings[i].SetActive(true);
            }
            if (PorteEnd)
            {
                sr.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
        /*
        if (PorteEnd)
        {
            sr.sprite = porteSprites[Index];
        }*/
    }
}
