using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Health : MonoBehaviour {

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private HealthEvent OnHeal;
    [SerializeField] private HealthEvent OnDamageTaken;
    [SerializeField] private UnityEvent OnHealthZero;

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

    public void Damage(float damage)
    {
        if (damage <= 0)
            return;

        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        OnDamageTaken.Invoke(GetNormalizedHealth());

        if (currentHealth == 0)
            OnHealthZero.Invoke();
    }

    public void Heal(float heal)
    {
        if (heal <= 0)
            return;

        currentHealth += heal;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHeal.Invoke(GetNormalizedHealth());
    }
}
