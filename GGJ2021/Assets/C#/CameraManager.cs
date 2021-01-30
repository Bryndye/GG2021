using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Transform Target;

    void Update() => SmoothFollow();

    void SmoothFollow()
    {
        if (Target != null)
        {
            Vector3 smooth = new Vector3(Target.position.x, Target.position.y, -10) - transform.position;
            transform.position += smooth / 100;
        }
    }

    private void Awake()
    {
        if (Instance != this)
            Destroy(this);
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

    private void FixedUpdate()
    {
       // AudioPlaying();
    }
}
