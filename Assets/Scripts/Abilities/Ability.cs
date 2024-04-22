using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float maxRange;
    public Sprite icon;

    public virtual void Activate(Transform player, Transform target) {}
}
