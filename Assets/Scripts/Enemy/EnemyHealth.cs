using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int expValue = 10;

    private Slider healthSlider;
    private Animator animator;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private bool isDead;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        healthSlider = GetComponentInChildren<Slider>();
    
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // remove obstacle by setting it to a trigger
        capsuleCollider.isTrigger = true;

        animator.SetTrigger("Dead");

        // Destroy game object after 2 sec
        Destroy(gameObject, 2f);
    }
}
