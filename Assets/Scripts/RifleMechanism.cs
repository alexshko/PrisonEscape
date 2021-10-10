using alexshko.prisonescape.life;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.Shooting
{
    public class RifleMechanism : MonoBehaviour
    {
        public int bulletDamage = 10;
        [Tooltip("Max. distance the rifle can shoot and hit. beyond that the bullet will fire but not hit.")]
        public float maxDistance = 200;
        [Tooltip("amount of bullets in a stack. When finished, the player has to reload to keep shooting")]
        public int bulletsInFullStack = 30;
        [Tooltip("Rate of fire. Number of bullets per second.")]
        public float bulletsInSecond = 3;
        [Tooltip("What layers the rifle can fire at.")]
        public LayerMask layersToShoot;

        [Tooltip("Text component to update the number of bullets left")]
        public Text textBulletsLeftRef;

        [Header("Shooting Effects:")]
        [Tooltip("Effect of sparks at the barrel of the gun")]
        public Transform gunShotEffectPref;
        [Tooltip("Effect of sparks from shot at a spot in correct layer.")]
        public Transform EmptySpotShotEffectPref;
        [Tooltip("Transform of the barrel of the gun, for fire effect")]
        public Transform BarrelPosRef;

        private int bulletsLeft;
        //always when update number of bullets we should update the text in the ui:
        [SerializeField]
        private int BulletsLeft
        {
            get => bulletsLeft;
            set
            {
                bulletsLeft = value;
                updateBulletsText();
            }
        }
        private float timeOfLastShot = 0;
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            BulletsLeft = bulletsInFullStack;
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Core.GameController.Instance.isGamePlaying)
            {
                if (Input.GetButton("Fire1"))
                {
                    Fire();
                }
                if (Input.GetButtonDown("Reload"))
                {
                    MakeReload().ConfigureAwait(true);
                }
            }
        }

        private async Task MakeReload()
        {
            anim.SetTrigger("Reload");
            await Task.Delay(500);
            BulletsLeft = bulletsInFullStack;
        }

        public void Fire()
        {

            //make sure enough time passed since last shot so it will keep the fire frequency:
            if ((Time.time > timeOfLastShot + 1.0f / bulletsInSecond) && (BulletsLeft > 0))
            {
                //make effect on the rifle mechanism, usually on the end of the barrel.
                Instantiate(gunShotEffectPref, BarrelPosRef.position, Quaternion.identity);
                timeOfLastShot = Time.time;
                BulletsLeft--;

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

        private void updateBulletsText()
        {
            if (textBulletsLeftRef)
            {
                textBulletsLeftRef.text = BulletsLeft.ToString();
            }
        }
    }
}
