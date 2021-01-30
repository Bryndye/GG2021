using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Event_Trigger : MonoBehaviour
{
    public enum TypeEvent
    {
        Dialogue,
        Cinematic,
        Femme
    }
    public TypeEvent Type;
    CanvasManager cm;
    Player_movement pm;
    [SerializeField] string nameEvent;
    public bool done = false;

    [Header("Dialogues")]
     public string[] dialogues;
     public AudioClip[] audioC;

    [Header("Cinematique")]
    public float TimeRun;
    public float TimeCinematic;

    [Header("Femme")]
    public Animator Anim_Femme;

    private void Awake()
    {
        cm = CanvasManager.Instance;
        pm = Player_movement.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !done)
        {
            switch (Type)
            {
                case TypeEvent.Dialogue:
                    EventDialogue();
                    break;
                case TypeEvent.Cinematic:
                    Cinematique();
                    break;
                case TypeEvent.Femme:
                    Femme();
                    break;
                default:
                    break;
            }
            done = true;
        }
    }

    private void EventDialogue()
    {
       
        if (cm.dialogueHere && dialogues.Length > 0)
        {
            cm.StartDiaEffect(dialogues, audioC);
        }
    }

    private void Cinematique()
    {
        pm.InCinematic = true;
        Invoke(nameof(EndCinematic),  TimeCinematic);
    }
    private void EndCinematic()
    {
        pm.InCinematic = false;
    }

    private void Femme()
    {
        if (Anim_Femme != null)
        {
            //play anim disappear
        }
    }

    #region EditMoi
    private void OnDrawGizmos()
    {
        gameObject.name = "Evt " + nameEvent;
    }

    #endregion
}
