using UnityEngine;

namespace StonesGaming
{
	public class FallPlatform : MonoBehaviour
	{
		public float speed = 0.3f;
		private bool _isHit;
		
		private void Awake()
		{
			var mpEngine = GetComponent<MovingPlatformEngine>();
			mpEngine.onPlatformerEngineContact += OnContact;
		}

		private void OnContact(PlatformerEngine player)
		{
			if (player.IsFalling())
			{
				_isHit = true;
			}
		}

		private void Update()
		{
			if (_isHit)
			{
				var movingPlatformEngine = GetComponent<MovingPlatformEngine>();
				movingPlatformEngine.velocity = Physics2D.gravity * speed;
			}
		}
	}
}