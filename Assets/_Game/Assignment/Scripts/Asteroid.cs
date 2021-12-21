using System;
using System.Collections;
using System.Collections.Generic;
using Assignment.Scripts;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;
        
        [Header("Config:")]
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minTorque;
        [SerializeField] private float _maxTorque;
        public int size = 1;
        public int Size
        {
            get => size;
            set
            {
                size = value;
            }
        }
        [Header("References:")]
        [SerializeField] private Transform _shape;
        [SerializeField] private AsteroidRunTimeSet asteroidRunTimeSet;
        [SerializeField] private ScriptableOnDestroyEvent scriptableOnDestroyEvent;
        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }

        private void OnEnable()
        {
            asteroidRunTimeSet.Add(this);
        }
        private void OnDisable()
        {
            asteroidRunTimeSet.Remove(this);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Laser"))
            {
               OnHitByLaser();
            }
        }
        private void OnHitByLaser() 
        {
            scriptableOnDestroyEvent.Raise(gameObject);
        }

        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            _shape.localScale = new Vector3(size, size, 0f);
        }
    }
}
