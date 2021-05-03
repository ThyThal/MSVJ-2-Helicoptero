using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu_Editor
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class IP_Base_Projectile : MonoBehaviour
    {
        #region Variables
        [Header("Base Projectile Properties")]
        public float projectileSpeed = 200f;
        public float damagePower = 10f;
        public float timeoutTime = 10f;

        protected Rigidbody rb;
        protected SphereCollider col;

        protected float startTime = 0f;
        #endregion




        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();

            col.isTrigger = true;

            if(rb)
            {
                startTime = Time.time;
                FireProjectile();
            }
        }

        private void Update()
        {
            if(Time.time >= startTime + timeoutTime)
            {
                DestroyProjectile();
            }
        }
        #endregion





        #region Custom Methods
        public virtual void FireProjectile()
        {
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        }

        public void DestroyProjectile()
        {
            Destroy(this.gameObject);
        }
        #endregion
    }
}
