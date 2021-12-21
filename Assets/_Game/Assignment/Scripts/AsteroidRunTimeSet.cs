using System.Collections.Generic;
using Asteroids;
using UnityEngine;

namespace Assignment.Scripts
{
    [CreateAssetMenu(fileName = "Asteroid Set")]
    public class AsteroidRunTimeSet : ScriptableObject
    {
        private Dictionary<int, Asteroid> asteroids = new Dictionary<int, Asteroid>();
        public int AsteroidsCount => asteroids.Count;
        private void Awake()
        {
            Clear();
        }
        public void Add(Asteroid asteroid)
        {
            asteroids.Add(asteroid.GetInstanceID(), asteroid);
        }
        public void Remove(Asteroid asteroid)
        {
            if(!asteroids.ContainsKey(asteroid.GetInstanceID()))
                return;
            asteroids.Remove(asteroid.GetInstanceID());
        }
        public Asteroid Get(int id)
        {
            if (!asteroids.ContainsKey(id))
                return null;
            
            return asteroids[id];
        }
        private void Clear()
        {
            asteroids = new Dictionary<int, Asteroid>();
        }
    }
}
