using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Direction;

namespace Player
{
    
    public class PlayerInput : MonoBehaviour
    {
        public delegate void KeyboardEvent(List<Direction> directions);
        public event KeyboardEvent OnKeyInput;

        private List<Direction> directions = new List<Direction>();


        void LateUpdate()
        {
            
            directions.Clear();

            if(Input.GetKey(KeyCode.W)){

                directions.Add(Direction.Forward);

            }

            if(Input.GetKey(KeyCode.A)){

                directions.Add(Direction.Left);

            }

            if(Input.GetKey(KeyCode.D)){

                directions.Add(Direction.Right);

            }

            if(Input.GetKey(KeyCode.S)){

                directions.Add(Direction.Back);

            }

            if(directions.Count > 0 && directions != null){

                OnKeyInput?.Invoke(directions);

            }

        }

    }

}
