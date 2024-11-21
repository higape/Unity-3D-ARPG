using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator Animator { get; set; }

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        Debug.Log(Animator.deltaPosition);
        transform.position += Animator.deltaPosition;
    }
}
