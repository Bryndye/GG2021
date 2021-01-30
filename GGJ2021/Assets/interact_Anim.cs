using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact_Anim : MonoBehaviour
{
    Player_movement pm;

    private void Awake() => pm = Player_movement.Instance;

    public void CallInteract()
    {
        pm.CallEvent();
    }

    public void EndAnim()
    {
        pm.Deposer();
    }
}
