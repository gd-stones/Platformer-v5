using UnityEngine;

namespace StonesGaming
{
	public class Spike : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.name.Contains("Player") || other.CompareTag("Player"))
			{
				var player = other.GetComponent<PlatformerCustomize>();
				player.Dead();
			}
		}
	}
}