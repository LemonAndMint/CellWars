using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Direction;

namespace Player
{
    
    public class PlayerInput : MonoBehaviour
    {
        public delegate void KeyboardEvent(List<Direction> directions);
        public delegate void MouseEvent(Vector3 mousePos);
        public delegate void MouseButtonEvent(GameObject go);

        public event KeyboardEvent OnKeyInput;
        public event MouseEvent OnMouseInput;

        public event MouseButtonEvent OnBindInput;
        public event MouseButtonEvent OnUnbindInput;



        private List<Direction> directions = new List<Direction>();


        void FixedUpdate()
        {
            
            //KEYBOARD ++
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
            //KEYBOARD --


            float mouseXAxis = Input.GetAxis("Mouse X");

            if(Input.GetKeyDown(KeyCode.Space)){

                OnMouseInput?.Invoke(Input.mousePosition);

            }

            
        }

        void Update() {

            if(Input.GetKeyDown(KeyCode.Mouse0)){

                GameObject targetGO = GetGO();

                if(targetGO != null){

                    OnBindInput?.Invoke(targetGO);

                }


            }

            if(Input.GetKeyDown(KeyCode.Mouse1)){

                GameObject targetGO = GetGO();

                OnUnbindInput?.Invoke(targetGO);

            }

            
        }

        public GameObject GetGO(){

            //IN ORDER IT TO WORK OPEN THE RIGIDBODY SIMULATION OR ADD RIGIDBODY!
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); //ADD LAYER MASK FOR EASY COMPUTATION FOR NETWORK!

            if (hit.collider != null)
            {

                if(hit.transform != null && hit.transform.gameObject.TryGetComponent(out CellStats stats)){

                    return hit.transform.gameObject;

                }

            }

            return null;
        }

    }

}
