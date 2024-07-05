using UnityEngine;
using UnityEngine.SceneManagement;
using static StonesGaming.PlatformerEngine;

namespace StonesGaming
{
    public class PlatformerCustomize : MonoBehaviour
    {
        // Core
        PlatformerEngine _engine;

        // Attack
        public int turnAttack = -1;
        public bool attackDirection;
        [SerializeField] GameObject _fire1;
        [SerializeField] GameObject _fire2;
        [SerializeField] GameObject _fire3;
        [SerializeField] GameObject _fireRun1;
        [SerializeField] GameObject _fireRun2;
        [SerializeField] GameObject _fireRun3;
        [SerializeField] Vector3 _firePointRight;
        [SerializeField] Vector3 _firePointLeft;

        // Push
        [SerializeField] float _pushSpeed = 0.5f;
        float _groundSpeed;

        // Hurt || hit
        [SerializeField] GameObject _playerHitPrefab;

        // Jump
        public bool isSkipJumpSe;
        [SerializeField] GameObject _jumpVfx1;
        [SerializeField] GameObject _jumpVfx2;

        // Health
        [SerializeField] int _originalHealth = 30;
        private int _health;
        [SerializeField] int _damagePlayer = 10;

        Vector3 _offset = new Vector3(0.4f, 0, 0);
        Vector3 _playerOriginPosition;
        GameObject[] _fireRun;
        GameObject[] _fire;

        public enum EngineCState
        {
            None,
            Attack,
            Hurt,
            OnLadder,
            Push,
            Teleport,
        }
        public EngineCState engineCState { get; private set; }
        public bool portalIn;

        public void SetStateEngineCustomize(EngineCState state)
        {
            engineCState = state;

            if (IsTeleport()) portalIn = true;
        }

        public void ResetStateEngineCustomize()
        {
            engineCState = EngineCState.None;
            portalIn = false;
        }

        public bool IsPush()
        {
            return engineCState == EngineCState.Push;
        }

        public bool IsAttack()
        {
            return engineCState == EngineCState.Attack;
        }

        public bool IsHurt()
        {
            return engineCState == EngineCState.Hurt;
        }

        public bool IsOnLadder()
        {
            return engineCState == EngineCState.OnLadder;
        }

        public bool IsTeleport()
        {
            return engineCState == EngineCState.Teleport;
        }

        public int CalculateLives()
        {
            return _health / _damagePlayer;
        }

        void Awake()
        {
            _engine = GetComponent<PlatformerEngine>();
            _engine.onJump += OnJump;
            _groundSpeed = _engine.groundSpeed;
            
            _health = _originalHealth;
        }

        void Start()
        {
            _playerOriginPosition = transform.position;
            _fireRun = new GameObject[] { _fireRun1, _fireRun2, _fireRun3 };
            _fire = new GameObject[] { _fire1, _fire2, _fire3 };
        }

        public void Dead()
        {
            Transform firstChild = transform.GetChild(0);
            firstChild.gameObject.SetActive(false);

            var cameraShake = FindObjectOfType<CameraShaker>();
            cameraShake.Shake();

            Invoke("OnRetry", 2f);
            Instantiate(_playerHitPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Globals.SetCameraFade(1f));
        }

        public void Attack()
        {
            SetStateEngineCustomize(EngineCState.Attack);

            turnAttack++;
            turnAttack = turnAttack % 3;

            Vector3 firePoint = Globals.AttackDirection ? _firePointRight : _firePointLeft;
            GameObject selectedFire = (_engine.velocity.sqrMagnitude > 0) ? _fireRun[turnAttack] : _fire[turnAttack];

            SimplePool.Spawn(selectedFire, transform.position + firePoint, transform.rotation);
        }

        public void Jump()
        {
            SimplePool.Spawn(_jumpVfx1, transform.position, transform.rotation);
        }

        public void OnRetry()
        {
            ResetStateEngineCustomize();
            Globals.Score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void OnJump()
        {
            isSkipJumpSe = false;
        }

        public bool IsDead()
        {
            if (_health <= 0) 
                return true;

            return false;
        }

        public void TakeDamage()
        {
            _health -= _damagePlayer;
        }

        public void Revival()
        {
            _health = _originalHealth;
            Globals.Score = 0;

            if (Globals.Checkpoint != Vector3.zero)
            {
                StartCoroutine(Globals.SetCameraFade(transform, Globals.Checkpoint));
            }
            else
            {
                StartCoroutine(Globals.SetCameraFade(transform, _playerOriginPosition));
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ToggleMovingPlatform"))
            {
                _engine.movingPlatformLayerMask = LayerMask.GetMask("Moving Platforms");
            }

            if (collision.CompareTag("FireOfBoss"))
            {
                TakeDamage();
                SetStateEngineCustomize(EngineCState.Hurt);
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            bool isPushing = Mathf.Abs(_engine.velocity.x) > 0;
            string tag = collision.gameObject.tag;

            if (tag == "Barrel" || tag == "Rock")
            {
                if (isPushing)
                {
                    SetStateEngineCustomize(EngineCState.Push);
                    _engine.groundSpeed = tag == "Barrel" ? _pushSpeed : 0.0001f;
                }
                else
                {
                    _engine.groundSpeed = _groundSpeed;
                    if (tag == "Rock")
                    {
                        transform.position -= _offset;
                    }
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Barrel") || collision.gameObject.CompareTag("Rock"))
            {
                ResetStateEngineCustomize();
                _engine.groundSpeed = _groundSpeed;
            }
        }
    }
}