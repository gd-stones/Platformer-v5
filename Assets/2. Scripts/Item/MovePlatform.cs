using UnityEngine;

namespace StonesGaming
{
	public class MovePlatform : MonoBehaviour
	{
		public Vector3 distance = new Vector3(5, 0, 0);
		public float speed = 0.5f;

		private Vector3 _startPosition;
		private Vector3 _endPosition;

		private void Awake()
		{
			_startPosition = transform.localPosition;
			_endPosition = _startPosition + distance;
		}

		private void Update()
		{
			var t = Mathf.PingPong(Time.time * speed, 1);
			var newPosition = Vector3.Lerp(_startPosition, _endPosition, t);

			transform.localPosition = newPosition;
		}
	}
}