using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    public AudioClip[] dirt;
    public AudioClip[] stone;
    public AudioClip[] grass;
    public AudioClip[] wood;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        switch (other.tag)
        {
            case "Dirt":
                int randDirt = Random.Range(0, dirt.Length);
                audioSource.PlayOneShot(dirt[randDirt]);
                Debug.Log(dirt[randDirt]);
                break;
            case "Grass":
                int randGrass = Random.Range(0, grass.Length);
                audioSource.PlayOneShot(grass[randGrass]);
                break;
            case "Stone":
                int randStone = Random.Range(0, stone.Length);
                audioSource.PlayOneShot(stone[randStone]);
                break;
            case "Wood":
                int randWood = Random.Range(0, wood.Length);
                audioSource.PlayOneShot(wood[randWood]);
                break;
            default:
                audioSource.PlayOneShot(dirt[6], 0.5f);
                break;
        }
    }
}
