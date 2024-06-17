// Morgan Houston
using StarterAssets;
using UnityEngine;

public class AnimationPlayerManager : MonoBehaviour
{
    #region Fields

    private StarterAssetsInputs inputs;
    private FirstPersonController controller;
    private Animator anim;
    private bool moving = false;
    private float speed = 4;
    private float MoveSpeed = 4f;
    private float SprintSpeed = 6f;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<FirstPersonController>();
        if(GetComponentInChildren<Animator>() != null)
            anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = inputs.sprint ? SprintSpeed : MoveSpeed;
        moving = (inputs.move != Vector2.zero) ? true : false;

        if (anim != null)
        {
            anim.SetFloat("Speed", speed);
            anim.SetFloat("Vertical", inputs.move.y);
            anim.SetFloat("Horizontal", inputs.move.x);
            anim.SetBool("Jumping", inputs.jump);
            anim.SetBool("Falling", !controller.Grounded);
            anim.SetBool("Moving", moving);
        }
        
    }

    #endregion

    #region Methods

    public void PlayInteractAnimation()
    {
        if (anim != null)
            anim.SetTrigger("Interact");
    }

    #endregion
}
