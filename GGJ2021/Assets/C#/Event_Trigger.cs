using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Event_Trigger : MonoBehaviour
{
    //CanvasManager cm;
    CameraManager camM;
    [SerializeField] string nameEvent;
    private bool done = false;

    [Header("Dialogues")]
     public string[] dialogues;
     public AudioClip audioC;

    private void Awake()
    {
        camM = CameraManager.Instance;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            EventAudio();
            EventDialogue();

            done = true;
        }
    }

    private void EventAudio()
    {
        if (audioC)
        {
            //Debug.Log(camM + " Manager Cam  " + audioC);
            camM.LaunchSound(audioC);
        }
    }

    private void EventDialogue()
    {
        /*
        if (cm.dialogueHere && dialogues.Length > 0)
        {
            cm.StartDiaEffect(dialogues);
        }*/
    }

    #region EditMoi
    private void OnDrawGizmos()
    {
        gameObject.name = "Evt " + nameEvent;
    }

    #endregion
}
