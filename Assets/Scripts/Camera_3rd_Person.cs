using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_3rd_Person : MonoBehaviour
{
    public Transform player;
    public Vector3 lookOffset;
    public float distance, cameraSpeed;
    public bool follow;

    private void LateUpdate()
    {
        Camera_Follow_Player(follow);
    }

    void Camera_Follow_Player(bool follow)
    {
        if (follow)
        {
            Vector3 lookPosition = player.position + lookOffset;
            this.transform.LookAt(lookPosition);

            if (Vector3.Distance(this.transform.position, lookPosition) > distance)
            {
                this.transform.Translate(0, 0, cameraSpeed * Time.deltaTime);
            }
        }
    }
}
