using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu_Editor
{
    [RequireComponent(typeof(AudioSource))]
    public class IP_Base_Weapon : MonoBehaviour, IP_IWeapon
    {
        #region Variables
        [Header("Base Weapon Properties")]
        public Transform muzzlePos;
        public GameObject projectile;
        public int maxAmmoCount = 100;
        [Space(5)]
        public GameObject muzzleFlash;
        public AudioClip fireClip;

        protected AudioSource aSource;
        protected int currentAmmoCount = 0;
        #endregion




        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            currentAmmoCount = maxAmmoCount;
            aSource = GetComponent<AudioSource>();
        }
        #endregion



        #region Interface Methods
        public virtual void FireWeapon()
        {
            Fire();
        }

        protected void Fire()
        {
            if (currentAmmoCount != 0)
            {
                HandleProjectile();
                HandleAudio();
                HandleVFX();

                currentAmmoCount--;
                currentAmmoCount = Mathf.Clamp(currentAmmoCount, 0, maxAmmoCount);
            }
            else
            {
                Reload();
            }
        }

        public void Reload()
        {
            Debug.Log("Reloading Weapon!");
            currentAmmoCount = maxAmmoCount;
        }
        #endregion




        #region Custom Methods
        protected virtual void HandleProjectile()
        {
            if(projectile)
            {
                Instantiate(projectile, muzzlePos.position, Quaternion.LookRotation(muzzlePos.forward));
            }
        }

        protected virtual void HandleAudio()
        {
            if(aSource && fireClip)
            {
                aSource.PlayOneShot(fireClip);
            }
        }

        protected virtual void HandleVFX()
        {
            if(muzzleFlash)
            {

            }
        }
        #endregion
    }
}
