using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    //private new Rigidbody2D rigidbody2D;
    private UIManager uiManager;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    private Animator anim;
    private bool dead;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("iFrimes")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiManager = FindObjectOfType<UIManager>();
        //rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("Grounded", true);
                anim.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
                //rigidbody2D.velocity = Vector2.zero;
                //uiManager.GameOver();
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components)
            component.enabled = true;
        transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * 5, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
