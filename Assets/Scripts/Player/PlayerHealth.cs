using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    private Slider healthSlider;
    private Animator animator;
    private PlayerMovement playerMovement;
    private bool isDead;
    private bool damaged;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        healthSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (damaged)
        {
            // TODO: play damaged animation
        }

        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");

        // disable movement when dead
        playerMovement.enabled = false;
    }
}
