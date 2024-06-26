using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Ability", menuName = "Abilites/Melee")]
public class Melee : Ability
{
    CharacterStats playerStats;
    CharacterStats targetStats;
    bool crit = false;
    public StatusEffectData statusEffect;

    public override void Activate(Transform player, Transform target) {
        Debug.Log(player.name + " uses Melee on " + target.name);

        playerStats = player.gameObject.GetComponent<PlayerStats>();
        targetStats = target.gameObject.GetComponent<EnemyStats>();

        float distance = Vector3.Distance(player.position, target.position);

        // If within range, attack
        if (distance <= maxRange) {
            target.gameObject.GetComponent<EnemyController>().Aggro(player);
            int damage = playerStats.damage.GetValue();
            int critRoll = Random.Range(0, 100);
            if (critRoll <= playerStats.criticalChance.GetValue()) {
                damage *= 2;
                crit = true;                
            }
            var effectable = target.GetComponent<IEffectable>();
            if (effectable != null) {
                effectable.ApplyEffect(statusEffect);
            }
            targetStats.TakeDamage(damage, this, crit);
            // reset crit
            crit = false;
        } else {
            GameManager.instance.SetWarning();
        }

        // todo https://discussions.unity.com/t/how-can-i-use-coroutines-in-scriptableobject/45402/2
    }
}
