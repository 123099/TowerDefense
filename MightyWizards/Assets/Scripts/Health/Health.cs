using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Health : MonoBehaviour {

    [Tooltip("The maximum health this object can have")]
    [SerializeField] private float maxHealth;
    [Tooltip("Set to true if you want this object to not be able to take damage")]
    [SerializeField] private bool invulnerable;

    [Tooltip("Invoked when the component is healed. Returns the amount of healing done")]
    [SerializeField] private HealthEvent OnHeal;
    [Tooltip("Invoked when the component is damaged. Returns the amount of damage done")]
    [SerializeField] private HealthEvent OnDamageTaken;
    [Tooltip("Invoked when the component's health changes in any way. Returns the current normalized health")]
    [SerializeField] private HealthEvent OnHealthChange;
    [Tooltip("Invoked when the component's health hits 0")]
    [SerializeField] private UnityEvent OnHealthZero;

    private float currentHealth;

	void Start () {
        Heal(maxHealth);
	}

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Heal(2);
        if (Input.GetKeyDown(KeyCode.F))
            Damage(3);
    }

    public float GetHealth ()
    {
        return currentHealth;
    }

    public float GetNormalizedHealth ()
    {
        if (maxHealth == 0)
            return 0;
        return currentHealth / maxHealth;
    }

    public bool IsAlive ()
    {
        return currentHealth > 0;
    }

    public void Damage(float damage)
    {
        if (damage <= 0)
            return;

        if (!invulnerable)
            SetHealth(currentHealth - damage);

        if (currentHealth < 0)
            currentHealth = 0;

        OnDamageTaken.Invoke(damage);

        if (currentHealth == 0)
            OnHealthZero.Invoke();
    }

    public void Heal(float heal)
    {
        if (heal <= 0)
            return;

        SetHealth(currentHealth + heal);

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHeal.Invoke(heal);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        OnHealthChange.Invoke(GetNormalizedHealth());
    }
}
