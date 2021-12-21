using System;
using System.Collections;
using System.Collections.Generic;
using Assignment.Scripts;
using DefaultNamespace.GameEvents;
using UnityEngine;
using Variables;

namespace Asteroids
{
    
    
    public class AsteroidDestroyer : MonoBehaviour
    {
        [SerializeField] private AsteroidRunTimeSet asteroids;
        [SerializeField] private ScriptableOnDestroyEvent scriptableOnDestroyEvent;
        
        [Header("Config")] 
        [SerializeField] private float clusterForce;
        [SerializeField] private float clusterDistance;

        private void Awake()
        {
            scriptableOnDestroyEvent.Register(ClusterAsteroid);
        }

        public void ClusterAsteroid(GameObject asteroidGo)
        {
            if(!asteroidGo.TryGetComponent(out Asteroid asteroid))
                return;
            
            if (asteroid.size > 1)
            {
                decimal clusterStep = (decimal) (2 * Mathf.PI) / asteroid.size;
                
                for (int i = 0; i < asteroid.size; i++)
                {
                    Asteroid newAsteroid = Instantiate(asteroid, asteroid.transform.position, Quaternion.identity);
                    newAsteroid.size = asteroid.size - 1;
                    newAsteroid.transform.position += (Vector3) new Vector2(Mathf.Cos((float)clusterStep * i),
                        Mathf.Sin((float)clusterStep * i) * clusterDistance); 
                    newAsteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2( Mathf.Cos((float)clusterStep * i),
                        Mathf.Sin((float)clusterStep * i)) * clusterForce, ForceMode2D.Impulse);
                }
            }
            Destroy(asteroid.gameObject);
        }
    }
}
