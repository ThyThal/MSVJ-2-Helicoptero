using UnityEngine;

namespace Helicopter_Inputs
{
    public class Helicopter_Base_InputManager : MonoBehaviour
    {
        #region • Variables (2)
        protected float horizontal = 0f;
        protected float vertical = 0f;
        #endregion
        
        #region • Unity methods (Built-In) (1)
        void Update()
        {
            HandleInputs();
        }
        #endregion
        
        #region • Custom methods (1)
        protected virtual void HandleInputs()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        #endregion
    }
}