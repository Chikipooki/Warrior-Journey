using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;


    private Animator anim;
    public PlayerController controller;
    public Joystick joystick;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stepSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            SoundManager.instance.PlaySound(jumpSound);
            anim.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        anim.SetBool("IsCrouching", isCrouching);
    }
}
