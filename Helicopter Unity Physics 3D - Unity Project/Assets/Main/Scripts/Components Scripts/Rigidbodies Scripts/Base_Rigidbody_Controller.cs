using UnityEngine;

namespace Helicopter_Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class Base_Rigidbody_Controller : MonoBehaviour
    {
        #region • Variables
        [Header("Base Physics Properties")]
        public float weightInKgs = 10f;
        public Transform centerOfGravity;
        
        protected Rigidbody rigidbody;
        #endregion
        
        #region • Unity methods (Built-In) (3)
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void Start()
        {
            if (rigidbody)
            {
                rigidbody.mass = weightInKgs;
            }
        }

        void FixedUpdate()
        {
            if (rigidbody)
            {
                HandlePhysics();
            }
        }
        #endregion
        
        #region • Custom methods (1)
        protected virtual void HandlePhysics() { }
        #endregion
    }
}