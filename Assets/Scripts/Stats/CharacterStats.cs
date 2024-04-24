using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IEffectable
{
    private StatusEffectData data;
    private float currentEffectTime = 0.0f;
    private float nextTickTime = 0.0f;
    private GameObject effectParticles = null;

    public int maxHealth = 100;
    public int currentHealth {get; private set;}
    public Stat damage;
    public Stat armor;
    public Stat level;
    public Stat criticalChance;
    public bool enemy;

    public GameObject floatingTextPrefab;

    public event System.Action<int,int> OnHealthChanged;

    bool immune = false;

    public void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (data != null) {
            HandleEffect();
        }    
    }

    public void SetImmune(bool value) {
        immune = value;
    }

    public void TakeDamage(int damage, Ability ability, bool crit) {
        if (!immune) {
            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
        
            currentHealth -= damage;

            // Instantiate floating text
            if (floatingTextPrefab != null) {
                ShowFloatingText(damage.ToString(), false, crit);
            }

            Debug.Log(transform.name + " Takes " + damage + " damage");

            if (OnHealthChanged != null) {
                OnHealthChanged(maxHealth, currentHealth);
            }
        
            if (currentHealth <= 0) {
                Die();
            }
        }
    }

    public void Heal(int amount) {
        if (currentHealth <= maxHealth) {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (floatingTextPrefab != null) {
                ShowFloatingText(amount.ToString(), true, false);
            }

            if (OnHealthChanged != null) {
                OnHealthChanged(maxHealth, currentHealth);
            }
        }

    }

    public virtual void Die() {
        // Die
        // ovverides
        Debug.Log(transform.name + " died");
    }

    public void ResetHealth() {
        currentHealth = maxHealth;

        if (OnHealthChanged != null) {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public void ShowFloatingText(string text, bool heal, bool crit) {
        GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        floatingText.GetComponent<FloatingText>().SetDetails(text, crit, heal, enemy);
    }

    public void ApplyEffect(StatusEffectData data)
    {
        this.data = data;
        if (data.EffectParticles) {
            Instantiate(data.EffectParticles, transform);
        }
    }

    public void RemoveEffect()
    {
        this.data = null;
        currentEffectTime = 0;
        nextTickTime = 0;
        if (effectParticles != null) {
            Destroy(effectParticles);
        }
    }

    public void HandleEffect()
    {
        currentEffectTime += Time.deltaTime;
        if (currentEffectTime >= data.lifetime) {
            RemoveEffect();
        }

        if (data == null) {return;}
    
        switch (data.type) {
            case StatusEffectData.StatusEffectType.DamageOverTime:
            {
                if (data.damageOverTimeAmount != 0 && currentEffectTime > nextTickTime) {
                    nextTickTime += data.tickSpeed;
                    TakeDamage(data.damageOverTimeAmount, null, false);
                }
                break;
            }
            case StatusEffectData.StatusEffectType.HealOverTime:
            {
                if (data.damageOverTimeAmount != 0 && currentEffectTime > nextTickTime) {
                    nextTickTime += data.tickSpeed;
                    Heal(data.damageOverTimeAmount);
                }
                break;
            }
        }
    }
}
