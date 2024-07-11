using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public Transform myTrans;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myTrans.Rotate(new Vector3(0, 0.1f, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            myTrans.Rotate(new Vector3(0, -0.1f, 0));
        }
        if (Input.GetKey(KeyCode.R))
        {
            animator.SetTrigger("OnReload");
        }
        if (Input.GetKey(KeyCode.Return))
        {
            animator.SetTrigger("OnFire");
        }
    }
}
