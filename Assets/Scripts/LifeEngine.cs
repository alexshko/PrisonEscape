using System;
using UnityEngine;

namespace alexshko.prisonescape.life
{
    public class LifeEngine : MonoBehaviour
    {
        public int maxLife = 100;
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

            lifeLeft -= damage;
            lifeLeft = Mathf.Clamp(lifeLeft, 0, maxLife);

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
