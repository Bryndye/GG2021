﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_zone : MonoBehaviour
{
    CanvasManager cm;
    Player_movement pm;

    [SerializeField] float rangeBetween = 10;
    [SerializeField] float posXMin;
    [SerializeField] float posXMax;

    [Header("Chrono")]
    public bool IndeadZone;
    float timer;
    [SerializeField] float timeMax;
    bool done, doneA;
    private void Awake()
    {
        cm = CanvasManager.Instance;
        pm = Player_movement.Instance;
        posXMax = transform.position.x;
    }

    private void Update()
    {
        CheckPos();

        if (IndeadZone)
        {
            timer += Time.deltaTime;
            if (timer >= timeMax)
            {
                print("the end");
                pm.InCinematic = true;
            }
        }
    }

    private void CheckPos()
    {
        if (posXMax <= transform.position.x)
        {
            posXMax = transform.position.x;
            posXMin = transform.position.x - rangeBetween;
        }
        if (transform.position.x < posXMin)
        {
            done = false;
            print("c le roussi");
            IndeadZone = true;
            if (!doneA)
            {
                doneA = true;
                cm.BandeAppear();
            }
        }
        else if(!done)
        {
            done = true;
            doneA = false;
            cm.BandeDisAppear();
            IndeadZone = false;
        }
    }
}
