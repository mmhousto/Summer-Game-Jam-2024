using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrottoEntrance : MonoBehaviour
{
    public MeshRenderer[] grotto;
    public SkinnedMeshRenderer npc;

    private void Start()
    {
        EnableDisableGrotto(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableDisableGrotto(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableDisableGrotto(false);
        }
    }

    private void EnableDisableGrotto(bool enable)
    {
        foreach(MeshRenderer renderer in grotto)
        {
            renderer.enabled = enable;
        }
        npc.enabled = enable;
    }
}
