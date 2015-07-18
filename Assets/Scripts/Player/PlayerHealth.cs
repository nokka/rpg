using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public GameObject healthPrefab;
    public Canvas canvas;

    private GameObject healthPanel;
    private Slider healthSlider;
    public float healthPanelOffset = 0.35f;
    private Animator animator;
    private PlayerMovement playerMovement;
    private bool isDead;
    private bool damaged;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;

        // Copy the prefab into our own instantiated health bar
        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();

        // Set our name
        Text name = healthPanel.GetComponentInChildren<Text>();
        name.text = "Player";
    }

    void Update()
    {

        UpdateHealthBarPosition();

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
        healthSlider.value = currentHealth / (float)startingHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void UpdateHealthBarPosition()
    {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = screenPos;
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");

        // disable movement when dead
        playerMovement.enabled = false;
    }
}
