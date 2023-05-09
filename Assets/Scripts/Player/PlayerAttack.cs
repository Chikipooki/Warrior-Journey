using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header ("Attack Preferences")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private AudioClip attackSound;

    [Header("Player Layer")]
    [SerializeField] private LayerMask enemyLayer;
    private float coolDownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerMovement playerMovement;
    public Transform attackPoint;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        coolDownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        SoundManager.instance.PlaySound(attackSound);
        anim.SetTrigger("attack");
        coolDownTimer = 0;

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
