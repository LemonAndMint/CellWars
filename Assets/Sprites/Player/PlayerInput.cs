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
        public delegate void MouseBindButtonEvent(GameObject go);
        public delegate void MouseUnbindButtonEvent();

        public event KeyboardEvent OnKeyInput;
        public event MouseEvent OnMouseInput;
        public event MouseBindButtonEvent OnBindInput;
        public event MouseUnbindButtonEvent OnUnbindInput;



        private List<Direction> directions = new List<Direction>();


        void LateUpdate()
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

            if(mouseXAxis != 0){

                OnMouseInput?.Invoke(Input.mousePosition);

            }

            if(Input.GetKeyDown(KeyCode.Mouse0)){

                GameObject targetGO = GetGO();

                if(targetGO != null){

                    OnBindInput?.Invoke(targetGO);

                }


            }

            if(Input.GetKeyDown(KeyCode.Mouse1)){

                OnUnbindInput?.Invoke();

            }

        }

        public GameObject GetGO(){

            //IN ORDER IT TO WORK OPEN THE RIGIDBODY SIMULATION OR ADD RIGIDBODY!
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 

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
