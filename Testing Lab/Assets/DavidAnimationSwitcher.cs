using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidAnimationSwitcher : MonoBehaviour
{
    private Animator animator;
    public bool isDoingJumpingJacks = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isDoingJumpinJacks", isDoingJumpingJacks);
    }
}
