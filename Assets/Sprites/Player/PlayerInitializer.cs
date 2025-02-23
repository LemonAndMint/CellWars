using Player;
using UnityEngine;

namespace Player
{
    
    public class PlayerInitializer : MonoBehaviour
    {
        public PlayerInput playerInput;
        public Movement playerMovement;

        void Start()
        {

            try
            {
                
                playerInput.OnKeyInput += playerMovement.Move;

            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Data);
            }

        }

    }

}
