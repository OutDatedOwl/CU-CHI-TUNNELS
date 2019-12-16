using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed, turnSpeed, acc, gun_Shoot_Range;
    public Animator anim;

    bool currently_Shooting, loaded_Gun;
    float forward_Move_Anim;
    [SerializeField]
    int gun_Shot_Count = 0;    
    Vector3 velocity;
    Vector3 moveDirection, turnDirection;
    CharacterController controller;
    AudioSource audioSource;

    Enemy_Movement monster;

    [SerializeField]
    AudioClip[] audio_Sound_Array;
    RaycastHit hit;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        controller = this.GetComponent<CharacterController>();
        loaded_Gun = true;
    }


    void Update()
    {
        Move_Input();
        Interact();
        Animate_Player();
    }

    void Move_Input()
    {
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        turnDirection = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        moveDirection.Normalize();
        turnDirection.Normalize();
        controller.transform.Rotate(turnDirection * turnSpeed);
        velocity = Vector3.Lerp(velocity, moveDirection.z * transform.forward * moveSpeed, acc * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!anim.GetBool("Aim"))
            {
                anim.SetBool("Aim", true);
            }
            else
                anim.SetBool("Aim", false);
        }

        if (anim.GetBool("Aim") && Input.GetKeyDown(KeyCode.Space) && loaded_Gun && !currently_Shooting)
        {
            currently_Shooting = true;
            anim.SetBool("Shoot", true);
            Gun_Raycast_Shoot();
            gun_Shot_Count++;
            Gun_Reload_Sound();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Equip_Pistol", true);
        }
    }

    void Reset_Shoot_Gun_Pos()
    {
        anim.SetBool("Shoot", false);
    }

    void Pistol_Gunshot_Sound()
    {
        audioSource.clip = audio_Sound_Array[0];
        audioSource.Play();
        StartCoroutine(Ready_Next_Shot());
    }

    void Gun_Raycast_Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, gun_Shoot_Range))
        {
            if (hit.collider.tag == "Enemy")
            {
                monster = hit.transform.GetComponent<Enemy_Movement>();
                monster.Monster_Health();
            }
        }
    }

    void Gun_Reload_Sound()
    {
        if (gun_Shot_Count == 5)
        {
            StartCoroutine(Reload_Gun_Time());
        }
    }

    void Animate_Player()
    {
        forward_Move_Anim = Input.GetAxis("Vertical");

        anim.SetFloat("Move_Forward", forward_Move_Anim);
    }

    IEnumerator Reload_Gun_Time()
    {
        yield return new WaitForSeconds(audio_Sound_Array[0].length);
        loaded_Gun = false;
        audioSource.clip = audio_Sound_Array[1];
        audioSource.Play();
        yield return new WaitForSeconds(audio_Sound_Array[1].length);
        gun_Shot_Count = 0;
        loaded_Gun = true;
    }

    IEnumerator Ready_Next_Shot()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        currently_Shooting = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * gun_Shoot_Range);
    }
}
