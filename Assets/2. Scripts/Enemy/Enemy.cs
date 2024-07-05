using UnityEngine;

namespace StonesGaming
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] GameObject hitPrefab;
        [SerializeField] AudioClip hitClip;

        PlatformerEngine _engine;
        SpriteRenderer _renderer;
        BoxCollider2D _collider;

        void Awake()
        {
            _engine = GetComponent<PlatformerEngine>();
            _engine.normalizedXMovement = -1;

            _renderer = GetComponent<SpriteRenderer>();
            _renderer.flipX = false;

            _collider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            var direction = 0 < _engine.normalizedXMovement ? Vector3.right : Vector3.left;
            var offset = _collider.size.y * 0.15f;

            var start = transform.position + new Vector3(0, offset, 0);
            var hit = Physics2D.Raycast(start, direction, _collider.size.x * 1f, Globals.ENV_MASK);

            //var end = start + direction * _collider.size.x * .7f;
            //Debug.DrawLine(start, end, Color.red);

            if (hit.collider != null)
            {
                _engine.normalizedXMovement = -_engine.normalizedXMovement;
                _renderer.flipX = !_renderer.flipX;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name.Contains("Player") || other.CompareTag("Player"))
            {
                var playerEngine = other.GetComponent<PlatformerEngine>();
                var playerEngineCustomize = other.GetComponent<PlatformerCustomize>();

                if (playerEngine.IsFalling())
                {
                    Destroy(gameObject);
                    playerEngine.ForceJump();

                    var cameraShake = FindObjectOfType<CameraShaker>();
                    cameraShake.Shake();

                    Instantiate(hitPrefab, transform.position, Quaternion.identity);

                    var audioSource = FindObjectOfType<AudioSource>();
                    audioSource.PlayOneShot(hitClip);

                    playerEngineCustomize.isSkipJumpSe = true;
                }
                else
                {
                    playerEngineCustomize.TakeDamage();
                    if (!playerEngineCustomize.IsOnLadder())
                        playerEngineCustomize.SetStateEngineCustomize(PlatformerCustomize.EngineCState.Hurt);
                }
            }
        }
    }
}