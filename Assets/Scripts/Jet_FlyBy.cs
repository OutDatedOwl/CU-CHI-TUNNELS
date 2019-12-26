using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet_FlyBy : MonoBehaviour
{
    public Transform end_Position;
    public float speed;

    private AudioSource audio_Source;
    private bool jet_Fly;

    void Start()
    {
        audio_Source = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (jet_Fly)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, end_Position.position, Time.deltaTime * speed);
        }
    }

    public IEnumerator Jet_Sound()
    {
        audio_Source.Play();
        yield return new WaitForSeconds(21.5f);
        jet_Fly = true;
    }
}
