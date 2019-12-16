using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    public Transform player_Pos;
    public Animator animation_controller;
    public float field_Of_Attack;
    public AudioSource[] audio_Source_FX;
    public AudioClip[] audio_Clip_FX;

    Game_Manager game_Manager;

    private NavMeshAgent monster;
    private SphereCollider col;
    private int monster_HP = 3;

    void Start()
    {
        monster = this.GetComponent<NavMeshAgent>();
        col = this.GetComponent<SphereCollider>();
        game_Manager = FindObjectOfType<Game_Manager>();
    }

    void Update()
    {
        Enemy_Field_View();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (monster.velocity != Vector3.zero)
            {
                animation_controller.SetBool("Run", true);
            }
            else
                animation_controller.SetBool("Run", true);

            if (!animation_controller.GetCurrentAnimatorStateInfo(0).IsName("ATTACK_1"))
            {
                monster.SetDestination(player_Pos.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Monster_Alerted_To_Player();
        }
    }

    void Enemy_Field_View()
    {
        Vector3 direction = player_Pos.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < field_Of_Attack * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 4f, 1 << 8))
            {
                animation_controller.SetBool("Attack_1", true);
            }
            else
            {
                animation_controller.SetBool("Attack_1", false);
            }
        }
        else
        {
            animation_controller.SetBool("Attack_1", false);
        }
    }

    void Monster_Alerted_To_Player()
    {
        audio_Source_FX[0].clip = audio_Clip_FX[0];
        audio_Source_FX[0].Play();
    }

    public void Monster_Health()
    {
        audio_Source_FX[1].clip = audio_Clip_FX[1];
        audio_Source_FX[1].Play();
        monster_HP--;
        if (monster_HP == 0)
        {
            //Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + transform.up, transform.forward * 4f);
    }
}
