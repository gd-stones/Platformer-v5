using UnityEngine;

namespace StonesGaming
{
	public class PlayerHit : MonoBehaviour
	{
		public Vector3 velocity = new Vector3(0, 15, 0);
		public float gravity = 30;

		private void Update()
		{
			transform.localPosition += velocity * Time.deltaTime;
			velocity.y -= gravity * Time.deltaTime;
		}
	}
}
