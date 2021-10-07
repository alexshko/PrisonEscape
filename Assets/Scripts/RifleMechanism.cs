using alexshko.prisonescape.Core;
using System;
using UnityEngine;

namespace alexshko.prisonescape.Shooting
{
    public class RifleMechanism : MonoBehaviour
    {
        public int bulletDamage = 10;
        public float maxDistance = 200;
        public int bulletsInFullStack = 30;
        public float bulletsInSecond = 3;
        public Transform shotEffectPref;

        [SerializeField]
        private int bulletsLeft;

        private float timeOfLastShot = 0;

        // Start is called before the first frame update
        void Start()
        {
            bulletsLeft = bulletsInFullStack;
        }

        public void Fire()
        {

            //make sure enough time passed since last shot so it will keep the fire frequency:
            if (Time.time > timeOfLastShot + 1.0f / bulletsInSecond)
            {
                //make effect on the gun:
                Instantiate(shotEffectPref, transform.position, Quaternion.identity);
                timeOfLastShot = Time.time;
                bulletsLeft--;

                MakeShotToTarget();
            }
        }

        private void MakeShotToTarget()
        {
            RaycastHit hit;

            Vector3 aimWorldSpace = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(aimWorldSpace, Camera.main.transform.forward, out hit, maxDistance))
            {
                Debug.LogFormat("hit: {0}", hit.transform.name);
                if (hit.transform.GetComponent<LifeEngine>() != null)
                {
                    //has LifeEngine, shoud have an effect from the LifeEngine of the object.
                    hit.transform.GetComponent<LifeEngine>().TakeShot(hit.point, hit.normal, bulletDamage);
                }
                else
                {
                    //doesn't have LifeEngine, will have a spark.
                    Transform bloodEffect = Instantiate(shotEffectPref, hit.point, Quaternion.identity);
                    bloodEffect.LookAt(hit.point + hit.normal);
                    Debug.LogFormat("shot an obstacle");
                }
            }
        }
    }
}
