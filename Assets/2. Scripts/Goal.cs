using UnityEngine;

namespace StonesGaming
{
	public class Goal : MonoBehaviour
	{
		public AudioClip goalClip;
		private bool _isGoal;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!_isGoal)
			{
				if (other.name.Contains("Player"))
				{
					var cameraShake = FindObjectOfType<CameraShaker>();
					cameraShake.Shake();

					_isGoal = true;
					var animator = GetComponent<Animator>();
					animator.Play("Pressed");
					var audioSource = FindObjectOfType<AudioSource>();
					audioSource.PlayOneShot(goalClip);
				}
			}
		}
	}
}