﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EmailUnfold : MonoBehaviour {

    public Animator EmailAnimator;
    [Space(5)]
    public KeyCode FoldDebugKey = KeyCode.F;
    public string UnfoldEmailTrigger = "";
    public string FoldEmailTrigger = "";

    private bool Folded = true;

    void Start () {
        EmailAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(FoldDebugKey))
        {
            ToggleFolding();
        }
    }

    public void ToggleFolding()
    {
        if(Folded)
        {
            Unfold();
        }
        else
        {
            Fold();
        }
    }

    public void Unfold()
    {
        Debug.Log("Unfolding e-mail...");
        Folded = false;
        EmailAnimator.SetBool("Folded", !Folded);
    }

    public void Fold()
    {
        Debug.Log("Folding back e-mail...");
        Folded = true;
        EmailAnimator.SetBool("Folded", !Folded);
    }

}
