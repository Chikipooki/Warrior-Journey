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
    private PlayerMovment playerMovment;
    public Transform attackPoint;
    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovment = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && coolDownTimer > attackCooldown)
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
            Debug.Log("1 damage");
        }


        //if (hit.collider != null)
        //    playerHealth = hit.collider.GetComponent<Health>();

        //return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
