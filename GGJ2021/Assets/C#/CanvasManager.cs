﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    //[SerializeField] Button bt_continue;
    CameraManager cm;
    Animator anim;

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
    private byte alphaC;
    public float TimeTransition;
    void Awake()
    {
        if (Instance != this)
            Destroy(this);
        cm = GetComponent<CameraManager>();
        //anim = GetComponent<Animator>();
        //anim.SetTrigger("Disappear");
    }

    #region visual
    private void UpdateDark()
    {
        //DarkFond.color = new Color32(255,255, 255, Mathf.Lerp(DarkFond.color.a, alphaC, TimeTransition));
        //Mathf.Lerp(transform.localPosition.x, Target.position.x * aheadAmount + aheadAmountx, aheadspeed * Time.deltaTime)
    }
    public void BandeAppear()
    {

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
