using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private float coolDownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerMovment playerMovment;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovment = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) && coolDownTimer > attackCooldown /*&& playerMovment.canAttack()*/)
            Attack();

        coolDownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
        coolDownTimer = 0;
    }
}
