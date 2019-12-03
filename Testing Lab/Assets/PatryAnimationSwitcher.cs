using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatryAnimationSwitcher : MonoBehaviour
{
    private Animator animator;
    public bool isWalking = true;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isWalking", isWalking);
    }
}
