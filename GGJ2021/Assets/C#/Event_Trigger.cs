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
        Femme,
        ending
    }
    public TypeEvent Type;
    CanvasManager cm;
    Player_movement pm;
    [SerializeField] string nameEvent;
    public bool done = false;

    [Header("Dialogues")]
     public string[] dialogues;
     public AudioClip[] audioC;

    [Tooltip("Le temps de Run doit être inférieur au temps de cinématique")]
    [Header("Cinematique")]
    [Range(0, 30)] public float TimeRun;
    bool endRunning = false;
    [Range(0,30)] public float TimeCinematic;

    [Header("Femme")]
    public Animator Anim_Femme;

    private void Awake()
    {
        cm = CanvasManager.Instance;
        pm = Player_movement.Instance;
    }
    private void Update()
    {
        CheckVarCine();
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
                case TypeEvent.ending:
                    CallEnd();
                    break;
                default:
                    break;
            }
            done = true;
        }
    }
    private void CallEnd()
    {
        pm.InCinematic = true;
        pm.Anim_Player.SetTrigger("Reunion");
        cm.EndingCalled();
    }
    private void EventDialogue()
    {
        if (cm.dialogueHere && dialogues.Length > 0)
        {
            cm.StartDiaEffect(dialogues, audioC);
        }
    }

    #region Cine
    private void CheckVarCine()
    {
        if (Type == TypeEvent.Cinematic && done && pm.InCinematic)
        {
            pm.rb.velocity = new Vector2(pm.speed + 1, pm.rb.velocity.y);
        }
        if (Type == TypeEvent.Cinematic && done && endRunning)
        {
            pm.Anim_Player.SetBool("IsMoving", false);
            pm.Anim_Player.SetTrigger("ChangeMode");
            pm.rb.velocity = new Vector2(0, pm.rb.velocity.y);
        }
    }
    private void Cinematique()
    {
        pm.InCinematic = true;
        pm.Anim_Player.SetBool("IsMoving", true);

        //pm.Anim_Player.SetBool(); launch anim run
        pm.Anim_Player.SetTrigger("ChangeMode");
        Invoke(nameof(EndRun), TimeRun);
        Invoke(nameof(EndCinematic),  TimeCinematic);
    }
    private void EndRun()
    {
        endRunning = true;

        print("end run");
    }
    private void EndCinematic()
    {
        pm.InCinematic = false;
        //print("end cine");
        Destroy(gameObject);
    }
    #endregion

    private void Femme()
    {
        if (Anim_Femme != null)
        {
            Anim_Femme.SetTrigger("Disappear");
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
