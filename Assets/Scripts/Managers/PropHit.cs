using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHit : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player") == false && collision.transform.CompareTag("Prop") == false && SoundManagerScript.Instance != null)
        {
            audioSource.PlayOneShot(SoundManagerScript.Instance.propHit);
        }

        if (collision.transform.CompareTag("Prop") && SoundManagerScript.Instance != null)
        {
            audioSource.PlayOneShot(SoundManagerScript.Instance.propHitProp);
        }
    }
}
