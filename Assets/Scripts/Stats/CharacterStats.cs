using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {get; private set;}
    public Stat damage;
    public Stat armor;
    public Stat level;
    public bool enemy;
    public GameObject floatingTextPrefab;
    public GameObject playerFloatingTextPrefeb;

    public event System.Action<int,int> OnHealthChanged;

    bool immune = false;

    public void Awake() {
        currentHealth = maxHealth;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(damage.GetValue());
        }
    }

    public void SetImmune(bool value) {
        immune = value;
    }

    public void TakeDamage(int damage) {
        if (!immune) {
            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
        
            currentHealth -= damage;

            // Instantiate floating text
            if (floatingTextPrefab != null) {
                ShowFlotingText(damage.ToString(), false);
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
                ShowFlotingText(amount.ToString(), true);
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

    public void ShowFlotingText(string text, bool heal) {
        Vector3 position = transform.position;
        GameObject floatingText;

        floatingText = Instantiate(floatingTextPrefab, position, Quaternion.identity, transform);
        floatingText.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().SetText(text);

        if (enemy) {
            floatingText.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        } else {
            if (heal) {
                floatingText.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().color = Color.green;
            } else {
                floatingText.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().color = Color.red;
            }
        }  
    }
}
