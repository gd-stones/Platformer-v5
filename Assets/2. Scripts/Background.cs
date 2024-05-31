using UnityEngine;

namespace StonesGaming
{
	public class Background : MonoBehaviour
	{
		public Vector2 speed;

		private void Update()
		{
			var spriteRenderer = GetComponent<SpriteRenderer>();
			var material = spriteRenderer.material;
			material.mainTextureOffset += speed * Time.deltaTime;
		}
	}
}