﻿using UnityEngine;

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
            var offset = _collider.size.y * .5f;
            var hit = Physics2D.Raycast(transform.position - new Vector3(0, offset, 0), direction, _collider.size.x * 1f, Globals.ENV_MASK);

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
                var engine = other.GetComponent<PlatformerEngine>();
                var player = other.GetComponent<PlatformerCustomize>();

                if (engine.IsFalling())
                {
                    Destroy(gameObject);
                    engine.ForceJump();

                    var cameraShake = FindObjectOfType<CameraShaker>();
                    cameraShake.Shake();

                    Instantiate(hitPrefab, transform.position, Quaternion.identity);

                    var audioSource = FindObjectOfType<AudioSource>();
                    audioSource.PlayOneShot(hitClip);

                    player.isSkipJumpSe = true;
                }
                else
                {
                    player.TakeDamage();
                    Globals.HurtFlag = true;
                }
            }
        }
    }
}