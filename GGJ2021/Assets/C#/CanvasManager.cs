using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    //[SerializeField] Button bt_continue;
    CameraManager cm;
    public Animator anim;

    [Header("Dialogues")]
    public Text dialogueHere;
    private bool skip;
    private int index =0;
    public string[] sentences;
    [SerializeField] string[] sentencesStock;
    public AudioClip[] audioc;
    [SerializeField] AudioClip[] acStock;
    private bool isRuntime;

    [Header("Fond")]
    public Image DarkFond;
    public float TimeTransition;
    void Awake()
    {
        if (Instance != this)
            Destroy(this);

        cm = GetComponent<CameraManager>();
        anim = GetComponent<Animator>();
        BandeDisAppear();
    }

    #region visual
    private void UpdateDark()
    {
        //convert float to byte color.r = (byte) ((int) (Mathf.Lerp(appear.a, btt, TimeTransition)) % 256)

    }
    public void BandeAppear()
    {
        anim.SetTrigger("Appear");
    }
    public void BandeDisAppear()
    {
        anim.SetTrigger("Disappear");
    }
    #endregion

    #region Daliogue
    IEnumerator Type()
    {
        LaunchAudio();
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueHere.text += letter;
            yield return new WaitForSeconds(latence);
        }
    }
    private void LaunchAudio()
    {
        if (audioc.Length > 0 && index < audioc.Length)
        {
            cm.AS_dia.clip = audioc[index];
            cm.AS_dia.Play();
            print("audio launch");
        }
    }

    public void StartDiaEffect(string[] dia, AudioClip[] ac)
    {
        if (!isRuntime)
        {
            sentences = dia;
            audioc = ac;
            isRuntime = true;
            index = 0;
            StartCoroutine(Type());
        }
        else
        {
            sentencesStock = dia;
            acStock = ac;
        }
    }

    public void NextDialogue()
    {
        if (index < sentences.Length - 1 && sentences != null)
        {
            index++;
            dialogueHere.text = null;
            StartCoroutine(Type());
            //Debug.Log("Next");
        }
        else
        {
            sentences = null;
            dialogueHere.text = null;
            isRuntime = false;
        }
        if (acStock != null && acStock.Length > 0)
        {
            audioc = acStock;
            acStock = null;
        }
        if (sentencesStock != null && sentencesStock.Length > 0 && sentences ==  null)
        {
            isRuntime = true;
            index = 0;
            sentences = sentencesStock;
            sentencesStock = null;
            dialogueHere.text = null;
            StartCoroutine(Type());
            //Debug.Log("next dialogues");
        }
        skip = false;
    }

    [SerializeField] float latence = 0.1f;

    #endregion

    void Update()
    {
        if (isRuntime && dialogueHere.text == sentences[index] && !skip)
        {
            skip = true;
            Invoke(nameof(NextDialogue), 2f);
        }
        UpdateDark();
    }
}
