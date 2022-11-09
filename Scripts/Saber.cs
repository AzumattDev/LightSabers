/*using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LightSabers.Scripts
{
    public class Saber : MonoBehaviour
    {
        #region Private Members

        #region Serializable

        [Tooltip("Boolean value which indicates whether the lightsaber is active or inactive.")] [SerializeField]
        private bool saberActive = true;

        private bool _lastSaberActiveStatus;

        [Tooltip("The color of the lightsaber")] [SerializeField]
        private Color bladeColor = Color.red;
        [Tooltip("The color of the lightsaber")] [SerializeField]
        private Color emissionBladeColor = Color.red;

        private bool _lastTrailsActivated;

        [Tooltip("Used to activate / deactivate blade trail.")] [SerializeField]
        private bool trailsActivated = true;

        #endregion

        #region Public Serializable Members

        [Tooltip(
            "The speed at which the blade is retracted or extended when the lightsaber is activated or deactivated.")]
        public float BladeExtendSpeed = 0.3f;
        
        [Tooltip(
            "List of blades which hang as child in the object.")]
        public List<SaberBlade> _blades;

        #endregion

        /#1#// <summary>
        /// List of blades which hang as child in the object.
        /// </summary>
        private List<SaberBlade> _blades;#1#

        private Color _lastColor;

        #endregion

        #region Properties

        /// <summary>
        /// Allows you to change the color of the lightsaber.
        /// If the color is changed, the lightsaber is updated.
        /// </summary>
        public Color BladeColor
        {
            get => bladeColor;
            set
            {
                bladeColor = _lastColor = value;
                UpdateLightSaber();
            }
        }

        /// <summary>
        /// Enables or disables the lightsaber.
        /// If the status is changed, the
        /// lightsaber is updated.
        /// </summary>
        public bool SaberActive
        {
            get => saberActive;
            set
            {
                if (saberActive.Equals(value)
                    && _lastSaberActiveStatus == saberActive)
                    return;

                _lastSaberActiveStatus = saberActive = value;
                UpdateLightSaber();
            }
        }

        /// <summary>
        /// Used to activate / deactivate trails on runtime.
        /// </summary>
        public bool TrailsActive
        {
            get => trailsActivated;
            set
            {
                if (trailsActivated == value)
                    return;
                _lastTrailsActivated = trailsActivated = value;
                _blades.ForEach(x =>
                {
                    x.SetFuncTrailFeatureActivated(() => trailsActivated);
                    x.ActivateTrail(trailsActivated);
                });
            }
        }

        #endregion

        #region Setup

        // Use this for initialization
        private void Awake()
        {
            _lastUpdateTime = Time.time;

            if (FindSetupBlades())
                return;

            Setup();
        }

        private float _lastUpdateTime;

        private void LateUpdate()
        {
            var currentUpdateTime = Time.time;
            if ((currentUpdateTime - _lastUpdateTime) * 1000 >= 500)
                return;
            _lastUpdateTime = currentUpdateTime;

            if (_lastSaberActiveStatus != saberActive)
                SaberActive = saberActive;

            if (_lastColor != bladeColor)
                BladeColor = bladeColor;

            if (_lastTrailsActivated != trailsActivated)
                TrailsActive = trailsActivated;
        }

        /// <summary>
        /// Find and setup light saber children blades
        /// </summary>
        /// <returns></returns>
        private bool FindSetupBlades()
        {
            //_blades = gameObject.GetComponentsInChildren<SaberBlade>().ToList();
            if (_blades.Any())
                return false;

            Debug.LogWarning("No light saber blades found. " +
                             $"Please add some blade children by adding a {nameof(SaberBlade)} script." +
                             $"The light saber Must have at least 1 blade.");
            return true;
        }

        /// <summary>
        /// Setup for the light saber
        /// </summary>
        private void Setup()
        {
            //Execute setup for the blades of the light saber.
            _blades.ForEach(x => x.Setup(BladeExtendSpeed, SaberActive));

            UpdateLightSaber();
        }

        #endregion

        #region Updates

        /// <summary>
        /// Updates the color of the blades.
        /// </summary>
        private void UpdateColor()
        {
            bladeColor.a = Mathf.Clamp(bladeColor.a, 0.1f, 1f);

            //Update each blade
            _blades?.ForEach(x => x.Color = bladeColor);
        }

        /// <summary>
        /// Updates each blade.
        /// It updates the active status, the light and the saber size.
        /// </summary>
        private void UpdateBlades()
        {
            _blades?.ForEach(SaberBlade =>
            {
                SaberBlade.BladeActive = SaberActive;
                SaberBlade.UpdateLighting();
                SaberBlade.UpdateSaberSize();
            });
        }

        /// <summary>
        /// This method updates the color of the lightsaber and its blades.
        /// </summary>
        public void UpdateLightSaber()
        {
            // Setup the color of the light saber.
            UpdateColor();

            // initially update blade length, so that it isn't set to what we have in unity's visual editor
            UpdateBlades();
        }

        #endregion

        #region Toggle

        /// <summary>
        /// Toggle for activating deactivating the lightsaber.
        /// </summary>
        public void ToggleActive()
        {
            SaberActive = !SaberActive;
        }

        /// <summary>
        /// Toggle for activating deactivating the lightsaber trails.
        /// </summary>
        public void ToggleActiveTrails()
        {
            TrailsActive = !TrailsActive;
        }

        #endregion
    }
}*/