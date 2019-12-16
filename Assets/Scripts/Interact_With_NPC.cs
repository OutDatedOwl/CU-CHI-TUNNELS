using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_With_NPC : MonoBehaviour
{
    public Animator npc_Animation;
    bool player_Inside_Talk_Zone, NPC_Speaking;
    [SerializeField]
    AudioClip[] speech_Line_Array;
    public AudioSource source;

    void Update()
    {
        NPC_Interaction();
    }

    void NPC_Interaction()
    {
        if (player_Inside_Talk_Zone)
        {
            if (Input.GetKeyDown(KeyCode.R) && !NPC_Speaking)
            {
                NPC_Speaking = true;
                StartCoroutine(Play_Next_Line());
            }
        }
    }

    IEnumerator Play_Next_Line()
    {
        npc_Animation.SetBool("Line_1", true);
        source.clip = speech_Line_Array[0];
        source.Play();
        yield return new WaitForSeconds(speech_Line_Array[0].length + 0.5f);
        npc_Animation.SetBool("Line_2", true);
        source.clip = speech_Line_Array[1];
        source.Play();
        yield return new WaitForSeconds(speech_Line_Array[1].length + 0.5f);
        npc_Animation.SetBool("Line_3", true);
        source.clip = speech_Line_Array[2];
        source.Play();
        yield return new WaitForSeconds(speech_Line_Array[2].length + 0.5f);
        npc_Animation.SetBool("Line_3", false);
        source.clip = speech_Line_Array[3];
        source.Play();
        yield return new WaitForSeconds(speech_Line_Array[3].length + 0.5f);
        npc_Animation.SetBool("Line_2", false);
        source.clip = speech_Line_Array[4];
        source.Play();
        yield return new WaitForSeconds(speech_Line_Array[4].length + 0.5f);
        npc_Animation.SetBool("Line_1", false);
        source.clip = speech_Line_Array[5];
        source.Play();
        NPC_Speaking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_Inside_Talk_Zone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player_Inside_Talk_Zone = false;
        }
    }
}
