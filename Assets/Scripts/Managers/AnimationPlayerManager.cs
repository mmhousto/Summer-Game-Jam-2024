using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerManager : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Vertical", inputs.move.y);
        anim.SetFloat("Horizontal", inputs.move.x);
    }
}
