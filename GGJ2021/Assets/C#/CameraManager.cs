using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Transform Target;
    [SerializeField] float aheadAmount, aheadspeed, aheadAmounty, aheadAmountx;

    [Header("Audio Sources")]
    public AudioSource AS_dia;
    public AudioSource AS_whisper;
    public AudioSource AS_music;

    void FixedUpdate() => SmoothFollow();

    void SmoothFollow()
    {
        if (Target != null)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, Target.position.x * aheadAmount + aheadAmountx, aheadspeed * Time.deltaTime)
                , Mathf.Lerp(transform.localPosition.y, Target.position.y + aheadAmounty, aheadspeed * Time.deltaTime)
                , transform.localPosition.z);
        }
    }

    private void Awake()
    {
        //audioS = GetComponent<AudioSource>();
    }

    #region Sound
    private AudioSource audioS;
    private AudioClip PistStock;
    public void LaunchSound(AudioClip ac)
    {
        if (!audioS.isPlaying)
        {
            audioS.clip = ac;
            audioS.Play();
        }
        else
        {
            PistStock = ac;
        }
    }

    private void AudioPlaying()
    {
        if (!audioS.isPlaying)
        {
            if (PistStock)
            {
                audioS.clip = PistStock;
                PistStock = null;
                audioS.Play();
            }
            else
            {
                audioS.clip = null;
            }
        }
    }

    #endregion
}
