using UnityEngine;

namespace Asteroids
{
    public class OnBecameInvisibleDestroyer : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}