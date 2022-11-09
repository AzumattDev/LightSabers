/*using UnityEngine;

namespace LightSabers.Scripts
{

    public class SaberTrail : MonoBehaviour
    {
        /// <summary>
        /// Assign a trail template to this field to create the sword trail during the sword swing.
        /// </summary>
        [SerializeField] public GameObject vfxTrail;

        /// <summary>
        /// Custom spawn point. If not set, the container to which this script is attached will be used as spawn point for the vfx trail.
        /// </summary>
        [SerializeField] public GameObject customSpawnPoint;

        /// <summary>
        /// When activated, the trail is automatically activated when a movement is made, if it was previously automatically deactivated.
        /// </summary>
        [SerializeField] public bool autoActivateTrail;

        private GameObject _currentTrailObject;
        private Vector3 _lastWorldPosition;
        public SaberTrailPsHandler SaberTrailPsHelper { get; private set; }

        private void Start()
        {
            InitSaberTrailHelper();
            ActivateTrail();
        }

        private void InitSaberTrailHelper()
        {
            SaberTrailPsHelper = gameObject.GetComponentInChildren<SaberTrailPsHandler>();
        }

        /// <summary>
        /// Used to activate the trail.
        /// </summary>
        public void ActivateTrail()
        {
            var spawnPoint = !customSpawnPoint ? gameObject : customSpawnPoint;
            if (!_currentTrailObject && vfxTrail)
            {
                _currentTrailObject =
                    Instantiate(vfxTrail, spawnPoint.transform.position, Quaternion.identity); //TODO pooling
                _currentTrailObject.transform.SetParent(transform);
            }

            if (_currentTrailObject)
                InitSaberTrailHelper();

            _currentTrailObject.SetActive(true);
        }

        public void FixedUpdate()
        {
            //Cancel if already activated
            if (_currentTrailObject)
                return;

            //Cancel if feature is deactivated
            if (!autoActivateTrail)
                return;

            var currentWorldPos = gameObject.transform.position;
            var difference = Mathf.Abs(Vector3.Distance(currentWorldPos, _lastWorldPosition));
            if (_lastWorldPosition == Vector3.zero || difference > 0.01f)
            {
                ActivateTrail();
                _lastWorldPosition = currentWorldPos;
            }
        }

        public void DeactivateTrail()
        {
            if (_currentTrailObject)
                _currentTrailObject.SetActive(false);
        }

        public void UpdateColor(Color color)
        {
            if (ReferenceEquals(SaberTrailPsHelper, null))
                return;
            SaberTrailPsHelper.UpdateColor(color);
        }
    }
}*/