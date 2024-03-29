﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : bird
{
    [SerializeField]
    public float _boostForce = 100;
    public bool _hasBoost = false;

   // Start is called before the first frame update
     public override void Start()
    {
        base.Start();
    }

    public void Boost()
    {
        if (State == BirdState.Thrown && !_hasBoost)
        {
            RigidBody.AddForce(RigidBody.velocity * _boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
