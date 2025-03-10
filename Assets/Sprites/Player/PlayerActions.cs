using System.Collections.Generic;
using Network;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Direction;

namespace Player
{
    
    public class PlayerActions : MonoBehaviour
    {
        public PlayerInput playerInput;
        public PlayerMovement playerMovement;
        public BoundManager boundManager;

        [Space(5f)]
        public GameObject playerGO;
        public PlayerStats playerCellStats; 

        [Space(5f)]
        public CellNetwork<PlayerNode, PlayerStats> playerCellNetwork;
        public float MaximumAllowedBoundLength;

        void Start()
        {

            try
            {

                _initComponents();
                
                playerInput.OnKeyInput += Move;
                playerInput.OnMouseInput += playerMovement.Rotate; 
                playerInput.OnBindInput += Bound;
                playerInput.OnUnbindInput += Unbound;

                playerCellNetwork = CellNetworkCreater.CreateNetwork(playerCellStats, playerGO, MaximumAllowedBoundLength, boundManager);


            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Data);
            }


        }

        public void Move(List<Direction> directions){

            playerMovement.Move(directions, playerCellStats.MoveSpeed);

        }

        public void Bound(GameObject goToBeBound){

            //Get the nearest cell bounded with the system. Add the to be bound cell to the nearest cell.

            if(goToBeBound.TryGetComponent(out CellStats stats)){

                GameObject nearestCellGO = playerCellNetwork.Add(stats, goToBeBound);

                if(nearestCellGO != null){
                    
                    playerCellStats.AddPenalty(stats.MoveVector, stats.MoveSpeed);

                }

            }

        }

        public void Unbound(GameObject goToBeUnbound){

            if(goToBeUnbound != null && goToBeUnbound.TryGetComponent(out CellStats stats)){

                if(playerCellNetwork.Remove(goToBeUnbound)){

                    playerCellStats.RemovePenalty(stats.MoveVector, stats.MoveSpeed);

                }

            }

        }

        private void _initComponents(){

            _initComponent(ref  playerInput);
            _initComponent(ref  playerMovement);
            _initComponent(ref  boundManager);

        }

        private void _initComponent<T>(ref T component) where T : MonoBehaviour{

            if(component == null){

                if(TryGetComponent(out T getComponent)){

                    component = getComponent;

                }

            }

        }

    }

}
