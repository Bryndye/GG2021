using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool uDie = false;
    public Transform Spawnpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && uDie)
        {
            SpawnManager sm = SpawnManager.Instance;
            sm.GetSpawn(Spawnpoint, uDie);
        }
    }
}
