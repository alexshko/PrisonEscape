using alexshko.prisonescape.life;
using UnityEngine;

namespace alexshko.prisonescape.Shooting
{
    public class RifleMechanism : MonoBehaviour
    {
        public int bulletDamage = 10;
        public float maxDistance = 200;
        public int bulletsInFullStack = 30;
        public float bulletsInSecond = 3;
        public LayerMask layersToShoot;
        public Transform gunShotEffectPref;
        public Transform EmptySpotShotEffectPref;
        [Tooltip("Transform of the barrel of the gun, for fire effects")]
        public Transform BarrelPosRef;

        [SerializeField]
        private int bulletsLeft;

        private float timeOfLastShot = 0;

        // Start is called before the first frame update
        void Start()
        {
            bulletsLeft = bulletsInFullStack;
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                Fire();
            }
        }

        public void Fire()
        {

            //make sure enough time passed since last shot so it will keep the fire frequency:
            if (Time.time > timeOfLastShot + 1.0f / bulletsInSecond)
            {
                //make effect on the rifle mechanism, usually on the end of the barrel.
                Instantiate(gunShotEffectPref, BarrelPosRef.position, Quaternion.identity);
                timeOfLastShot = Time.time;
                bulletsLeft--;

                //make the shot itself, including finding the target object and making effects over there:
                MakeShotToTarget();
            }
        }

        private void MakeShotToTarget()
        {
            RaycastHit hit;

            Vector3 aimWorldSpace = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            //if hit something in the world:
            if (Physics.Raycast(aimWorldSpace, Camera.main.transform.forward, out hit, maxDistance, layersToShoot))
            {
                Debug.LogFormat("hit: {0}", hit.transform.name);
                //if hit something with life, let the lifeEngine class deal with it (make blood and so on). otherwise, the RifleMechanism makes an effect on the hit spot.
                if (hit.transform.GetComponent<LifeEngine>() != null)
                {
                    //has LifeEngine, shoud have an effect from the LifeEngine of the object.
                    hit.transform.GetComponent<LifeEngine>().TakeShot(hit.point, hit.normal, bulletDamage);
                }
                else
                {
                    //doesn't have LifeEngine, will have a spark.
                    Transform bloodEffect = Instantiate(EmptySpotShotEffectPref, hit.point + new Vector3(0,0.1f,0), Quaternion.identity);
                    bloodEffect.LookAt(hit.point + hit.normal);
                    Debug.LogFormat("shot an obstacle");
                }
            }
        }
    }
}
