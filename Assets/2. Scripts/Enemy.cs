using UnityEngine;

namespace StonesGaming
{
	public class Enemy : MonoBehaviour
	{
		public GameObject hitPrefab;
		public AudioClip hitClip;

		private PlatformerEngine _engine;
		private SpriteRenderer _renderer;
		private BoxCollider2D _collider;

		private void Awake()
		{
			_engine = GetComponent<PlatformerEngine>();
			_engine.normalizedXMovement = -1;
			_renderer = GetComponent<SpriteRenderer>();
			_renderer.flipX = false;
			_collider = GetComponent<BoxCollider2D>();
		}

		private void Update()
		{
			var dir = 0 < _engine.normalizedXMovement
				? Vector3.right
				: Vector3.left;

			var offset = _collider.size.y * 0.5f;
			var hit = Physics2D.Raycast
			(
				transform.position - new Vector3(0, offset, 0),
				dir,
				_collider.size.x * 0.55f,
				Globals.ENV_MASK
			);

			if (hit.collider != null)
			{
				_engine.normalizedXMovement = -_engine.normalizedXMovement;
				_renderer.flipX = !_renderer.flipX;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.name.Contains("Player"))
			{
				var engine = other.GetComponent<PlatformerEngine>();

				if (engine.IsFalling())
				{
					Destroy(gameObject);
					engine.ForceJump();
					var cameraShake = FindObjectOfType<CameraShaker>();
					cameraShake.Shake();
					Instantiate(hitPrefab, transform.position, Quaternion.identity);
					var audioSource = FindObjectOfType<AudioSource>();
					audioSource.PlayOneShot(hitClip);
					var player = other.GetComponent<Player>();
					player.isSkipJumpSe = true;
				}
				else
				{
					var player = other.GetComponent<Player>();
					player.Dead();
				}
			}
		}
	}
}