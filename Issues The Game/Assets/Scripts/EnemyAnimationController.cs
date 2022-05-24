using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private RangedAIPatrol range;
    private string currentState;

    // private WeaponPickup wp;

    // Sprite color
    // private SpriteRenderer sprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
        range = GetComponent<RangedAIPatrol>();
    }

    public void ReloadState(bool reload)
    {
        animator.SetBool("Reloading", reload);
    }

    public void MoveState(bool move)
    {
        animator.SetBool("Moving", move);
    }

    public void AtkState()
    {
        animator.ResetTrigger("Atk");
        animator.SetTrigger("Atk");
    }
}
