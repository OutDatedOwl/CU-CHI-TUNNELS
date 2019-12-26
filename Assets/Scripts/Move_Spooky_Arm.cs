using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Spooky_Arm : MonoBehaviour
{
    Trigger_Traps trigger_Set;
    Vector3 end_Pos;
    [SerializeField]
    Transform start_Pos;
    [SerializeField]
    float speed;

    void Start()
    {
        trigger_Set = GameObject.FindObjectOfType<Trigger_Traps>();
        end_Pos = this.transform.position;
        this.transform.position = start_Pos.position;
    }

    void Update()
    {
        if (trigger_Set.spook)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, end_Pos, speed * Time.deltaTime);
        }
    }

}
