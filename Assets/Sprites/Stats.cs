using System;
using UnityEngine;

public class Stats : MonoBehaviour
{

    private string _id;
    public string ID{ get => _id; } //GUID
    public float Mass; //Different from rigidbody mass.
    public float MoveSpeed;

    private void _init(){

        _id = Guid.NewGuid().ToString();

        //Generate random Mass, MoveSpeed and MoveVector for the object.

    }

}



public class BoundStats
{
    public float MaxBreakingForce;

    public BoundStats(){

        //Generate random MaxBreakingForce for the bound and according to that change the sprite ccolor.
        //Length will be based on actual bond length so it wont be random.

    }

}