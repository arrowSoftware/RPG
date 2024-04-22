using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1.0f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.6f;
    
    public event System.Action OnAttack;

    private CharacterStats myStats;

    void Start() {
        myStats = GetComponent<CharacterStats>();
    }

    void Update() {
        attackCooldown -= Time.deltaTime;
    }

    public void AttackTarget() {}

    public void Attack(CharacterStats targetStats) {
        if (attackCooldown <= 0) {
           Debug.Log("Attack");
           targetStats.TakeDamage(myStats.damage.GetValue());
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null) {
                OnAttack();
            }
            attackCooldown = 1.0f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
    }
}
