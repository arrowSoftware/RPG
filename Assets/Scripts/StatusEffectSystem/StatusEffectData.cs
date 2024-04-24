using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=J2CFVjqEHpU

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffectData : ScriptableObject
{
    public enum StatusEffectType {
        HealOverTime,
        DamageOverTime
    }
    public new string name;
    public Sprite icon;
    public StatusEffectType type;
    public int damageOverTimeAmount;
    public float tickSpeed;
    public float lifetime;

    public GameObject EffectParticles;
}
