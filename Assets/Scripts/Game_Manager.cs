using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public AudioClip[] sound_FX_Array;
    public AudioSource[] audio_FX_Source;
    public float bomb_Shake_Timer;

    Fade_Out_Title fadeOutScript;
    Jet_FlyBy jet_Boolean;
    Camera_Shake camera_1;

    void Start()
    {
        //camera_1 = FindObjectOfType<Camera_Shake>();

        //audio_FX_Source[0].clip = sound_FX_Array[2];
        //audio_FX_Source[0].Play();
        //audio_FX_Source[1].clip = sound_FX_Array[3];
        //audio_FX_Source[1].Play();        

        audio_FX_Source[0].clip = sound_FX_Array[0];
        audio_FX_Source[0].Play();

        jet_Boolean = FindObjectOfType<Jet_FlyBy>();
        fadeOutScript = FindObjectOfType<Fade_Out_Title>();
        //StartCoroutine(Bomb_Drop());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(jet_Boolean.Jet_Sound());
            StartCoroutine(fadeOutScript.Fade_Out_Routine());
        }
        //bomb_Shake_Timer += Time.deltaTime;
        //if (bomb_Shake_Timer >= sound_FX_Array[2].length)
        //{
        //    camera_1.shouldShake = true;
        //    bomb_Shake_Timer = 0;
        //}
    }

    IEnumerator In_Time()
    {
        yield return new WaitForSeconds(1f);
        camera_1.power = 0.2f;
        camera_1.duration = 0.05f;
        camera_1.slowDownAmount = 0.7f;
        camera_1.shouldShake = true;
        bomb_Shake_Timer = 0;
    }

    IEnumerator Bomb_Drop()
    {
        //yield return new WaitForSeconds(1f);
        audio_FX_Source[0].clip = sound_FX_Array[2];
        audio_FX_Source[0].Play();

        yield return new WaitForSeconds(sound_FX_Array[2].length - 1.5f);
        audio_FX_Source[1].clip = sound_FX_Array[3];
        audio_FX_Source[1].Play();
    }
}
