using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Direction
{
    
    public static class DirectionConverter{

        public static Vector2 DirectionToVector2(Direction direction){

            switch (direction)
            {
                case Direction.Forward:
                    return Vector2.up;
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                    return -1 * Vector2.left;
                case Direction.Back:
                    return -1 * Vector2.up;
                case Direction.None:
                    return Vector2.zero;
                default:
                    Debug.LogError("Something wrong in Direction Converter!");
                    return Vector2.zero;

            }

        }

        public static Vector2 DirectionsToVector2(List<Direction> directions){

            Vector2 tempVector = Vector2.zero;

            foreach (Direction direction in directions)
            {

                tempVector += DirectionToVector2(direction);

            }

            return tempVector;

        }

        /// <summary>
        /// Direction according to given transform.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Vector2 TransformDirectionToVector2(Transform transform, Direction direction = Direction.Forward){

            switch (direction)
            {
                case Direction.Forward:
                    return transform.forward;
                case Direction.Left:
                    new NotImplementedException();
                    break;
                case Direction.Right:
                    new NotImplementedException();
                    break;
                case Direction.Back:
                    new NotImplementedException();
                    break;
                case Direction.None:
                    new NotImplementedException();
                    break;
                default:
                    new NotImplementedException();
                    break;

            }

            return Vector2.zero;

        }

    }

    public enum Direction{
        Forward,
        Left,
        Right,
        Back,
        None
    }

}