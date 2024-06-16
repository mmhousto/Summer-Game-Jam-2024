using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AnimationPlayerManager : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    private FirstPersonController controller;
    private Animator anim;
    private float speed = 4;
    private float MoveSpeed = 4f;
    private float SprintSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<FirstPersonController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = inputs.sprint? SprintSpeed : MoveSpeed;
        anim.SetFloat("Speed", speed);
        anim.SetFloat("Vertical", inputs.move.y);
        anim.SetFloat("Horizontal", inputs.move.x);
        anim.SetBool("Jumping", inputs.jump);
        anim.SetBool("Falling", !controller.Grounded);
    }

    public void PlayInteractAnimation()
    {
        anim.SetTrigger("Interact");
    }
}
