using UnityEngine;
using UnityEngine.UI;

namespace StonesGaming
{
    public class EngineMonitor : MonoBehaviour
    {
        public Text fallText;
        public Text engineStateText;
        public Text prevEngineStateText;
        public Text collidingAgainst;
        public Text ladderZone;
        public Text restrictedArea;
        public PlatformerEngine engineToWatch;

        private PlatformerEngine.EngineState _savedEngineState;

        private PlatformerEngine.EngineState _engineState
        {
            set
            {
                if (_savedEngineState != value)
                {
                    prevEngineStateText.text = string.Format("Prev Engine State: {0}", _savedEngineState);
                    engineStateText.text = string.Format("Engine State: {0}", value);
                }
                _savedEngineState = value;
            }
        }

        void Start()
        {
            engineToWatch.onLanded += OnFallFinished;
            fallText.color = Color.white;
        }

        public void OnFallFinished()
        {
            fallText.text = string.Format("Fall Distance: {0:F}", engineToWatch.amountFallen);
            fallText.color = Color.green;
            _justFellTimer = 0.5f;
        }

        float _justFellTimer;

        void Update()
        {
            if (_justFellTimer > 0)
            {
                _justFellTimer -= Time.deltaTime;
                if (_justFellTimer <= 0)
                {
                    fallText.color = Color.white;
                }
            }

            _engineState = engineToWatch.engineState;
            collidingAgainst.text = string.Format("Colliding Against: {0}", engineToWatch.collidingAgainst);

            if (ladderZone != null)
            {
                ladderZone.text = string.Format("Ladder Zone: {0}", engineToWatch.ladderZone);
            }

            if (restrictedArea != null)
            {
                restrictedArea.text = string.Format("restricted: {0} OWP: {1} Solid? {2}", engineToWatch.IsRestricted(), engineToWatch.enableOneWayPlatforms, engineToWatch.oneWayPlatformsAreWalls);
            }
        }
    }
}
