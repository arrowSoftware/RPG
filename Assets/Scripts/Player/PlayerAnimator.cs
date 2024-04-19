using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = 0.1f;
    Animator animator;
    PlayerControls playerControls;
    public float speedPercent;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerControls = GetComponent<PlayerControls>();
    }

    void Update()
    {
        speedPercent = playerControls.velocity.magnitude / playerControls.runSpeed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
