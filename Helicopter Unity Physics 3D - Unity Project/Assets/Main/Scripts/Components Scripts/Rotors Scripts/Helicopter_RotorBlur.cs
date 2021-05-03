using Helicopter_Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace Helicopter_Components
{
    public class Helicopter_RotorBlur : MonoBehaviour, IHelicopter_Rotor
    {
        #region • Variables (5)
        [Header("Rotor Blur Properties")]
        public float maxDps = 1000f;
        public GameObject blurQuad;
        public Material blurMaterial;

        [Header("Blades List")]
        public List<GameObject> blades = new List<GameObject>();
        
        [Header("Blur Textures List")]
        public List<Texture2D> blurTextures = new List<Texture2D>();
        #endregion
        
        #region • Custom interface Methods (1)
        public void UpdateRotor(float dps, Helicopter_Main_Input_Manager mainInputManager)
        {
            //Debug.Log("Blurring Main Rotor");
            float normalizedDPS = Mathf.InverseLerp(0f, maxDps, dps);
            int blurTexID = Mathf.FloorToInt(normalizedDPS * blurTextures.Count - 1);
            blurTexID = Mathf.Clamp(blurTexID, 0, blurTextures.Count - 1);

            //check to see if we have Blur Textures and A blur Mat
            if (blurMaterial && blurTextures.Count > 0)
            {
                blurMaterial.SetTexture("_MainTex", blurTextures[blurTexID]);
            }

            //Handle the Geo Blades Visibility
            if (blurTexID > 2 && blades.Count > 0)
            {
                HandleBladeActivation(false);
            }
            
            else
            {
                HandleBladeActivation(true);
            }

            //Handle the Blur Geo Visibility
            if (blurTexID > 1)
            {
                HandleBlurActivation(true);
            }
            
            else
            {
                HandleBlurActivation(false);
            }
        }
        #endregion

        #region • Custom methods (2)
        void HandleBladeActivation(bool isActive)
        {
            foreach (GameObject blade in blades)
            {
                if(blade.activeSelf != isActive)
                {
                    blade.SetActive(isActive);
                }
            }
        }

        void HandleBlurActivation(bool isActive)
        {
            if(blurQuad && blurQuad.activeSelf != isActive)
            {
                blurQuad.SetActive(isActive);
            }
        }
        #endregion
    }
}