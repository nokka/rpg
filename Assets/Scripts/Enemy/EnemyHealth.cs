using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int expValue = 10;
    public GameObject healthPrefab;
    public Canvas canvas;

    private GameObject healthPanel;
    private Slider healthSlider;
    public float healthPanelOffset = 0.35f;

    private Animator animator;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private bool isDead;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Copy the prefab into our own instantiated health bar
        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();

        // Set our enemy name
        Text enemyName = healthPanel.GetComponentInChildren<Text>();
        enemyName.text = "BIG BOSS";

        currentHealth = startingHealth;
    }

    void Update()
    {
        UpdateHealthBarPosition();
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

    private void UpdateHealthBarPosition()
    {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = screenPos;
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
