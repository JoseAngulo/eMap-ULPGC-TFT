using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationSwitcher : MonoBehaviour
{
    private Animator animator;
    public bool isDoingSitUps = false;
    public bool isDoingPushUps = false;
   
    private void Start()
    {

        animator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        

       //if (isDoingSitUps) { isDoingPushUps = false; }
       //if (isDoingPushUps) { isDoingSitUps = false; }

       animator.SetBool("isDoingSitUps", isDoingSitUps);
       animator.SetBool("isDoingPushUps", isDoingPushUps);

    }
}
