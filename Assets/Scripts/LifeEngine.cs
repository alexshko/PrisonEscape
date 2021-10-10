using System;
using UnityEngine;

namespace alexshko.prisonescape.life
{
    public class LifeEngine : MonoBehaviour
    {
        [Tooltip("Initial Life")]
        public int maxLife = 100;
        [Tooltip("Prefab of the shoot effect on the hitted object.")]
        public Transform shotEffectPref;

        //event that happens when the entity dies:
        public Action OnDieEvent { get; set; }

        [SerializeField]
        private int lifeLeft;
        //will be used for lifebar and check how much life left for other classes.
        public int LifeLeft
        {
            get => lifeLeft;
        }

        private void Awake()
        {
            lifeLeft = maxLife;
        }

        public void TakeShot(Vector3 worldSpacePosition, Vector3 normalOfSurface, int damage =10)
        {
            Debug.LogFormat("hit {0}", gameObject.name);

            //make an effect of blood:
            Transform bloodEffect = Instantiate(shotEffectPref, worldSpacePosition, Quaternion.identity);
            bloodEffect.LookAt(worldSpacePosition + normalOfSurface);

            //apply damage and make sure it's within limits:
            lifeLeft -= damage;
            lifeLeft = Mathf.Clamp(lifeLeft, 0, maxLife);

            //if life less than zero, call event and destroy the prisoner:
            if (lifeLeft <= 0)
            {
                if (OnDieEvent != null)
                {
                    OnDieEvent();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
