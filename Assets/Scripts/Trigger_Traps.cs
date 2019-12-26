using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Traps : MonoBehaviour
{
    public bool spook;
    public Transform particle_Pos;
    public float speed;

    private void Update()
    {
        if (spook)
        {
            particle_Pos.position = Vector3.Lerp(particle_Pos.position, new Vector3(-13.31f, 2.86f, -2.97f), speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spook  = true;
        }
    }
}
