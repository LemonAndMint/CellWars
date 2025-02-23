using System.Collections.Generic;
using UnityEngine;
using Utils.Direction;

public class Movement : MonoBehaviour
{
    public Rigidbody2D targetRB;

    public void Move(List<Direction> directions){

        Vector2 moveVector = DirectionConverter.DirectionsToVector2(directions);

        targetRB.MovePosition(moveVector);
        
    }

}