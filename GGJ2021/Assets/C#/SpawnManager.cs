using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : Singleton<SpawnManager>
{
    Dead_zone dz;
    Player_movement pm;
    CanvasManager cm;
    public Transform SpawnPoint;
    bool done;

    private void Awake()
    {
        dz = Dead_zone.Instance;
        pm = Player_movement.Instance;
        cm = CanvasManager.Instance;
    }
    //private void 
    public void GetSpawn(Transform pos, bool die)
    {
        SpawnPoint = pos;
        if (die)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        pm.InCinematic = true;

        if (SpawnPoint != null && !done)
        {
            done = true;
            dz.can = false;
            cm.anim.SetTrigger("Dead");
            Invoke(nameof(SetPlayer), 5.5f);
        }
    }

    private void SetPlayer()
    {
        pm.transform.position = SpawnPoint.position;
        pm.transform.rotation = Quaternion.identity;
        pm.InCinematic = false;
        done = false;
        dz.posXMax = pm.transform.position.x;
        dz.can = true;
    }
}
