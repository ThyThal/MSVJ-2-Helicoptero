using Helicopter_Inputs;

namespace Helicopter_Components
{
    public interface IHelicopter_Rotor
    {
        #region • Custom methods (1)
        void UpdateRotor(float dps, Helicopter_Main_Input_Manager mainInputManager);
        #endregion  
    }
}