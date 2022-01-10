using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    public void OpenFence()
    {
        animator.SetBool("openFence", true);
    }
}
