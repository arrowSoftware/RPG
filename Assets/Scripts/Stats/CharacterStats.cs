using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {get; private set;}
    public Stat damage;
    public Stat armor;

    public event System.Action<int,int> OnHealthChanged;

    public void Awake() {
        currentHealth = maxHealth;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(damage.GetValue());
        }
    }

    public void TakeDamage(int damage) {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
    
        currentHealth -= damage;
        Debug.Log(transform.name + " Takes " + damage + " damage");

        if (OnHealthChanged != null) {
            OnHealthChanged(maxHealth, currentHealth);
        }
    
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        // Die
        // ovverides
        Debug.Log(transform.name + " died");
    }
}
