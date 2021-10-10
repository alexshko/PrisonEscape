using alexshko.prisonescape.life;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.Shooting
{
    public class RifleMechanism : MonoBehaviour
    {
        public int bulletDamage = 10;
        public float maxDistance = 200;
        public int bulletsInFullStack = 30;
        public float bulletsInSecond = 3;
        public LayerMask layersToShoot;

        [Tooltip("Text component to update the number of bullets left")]
        public Text textBulletsLeftRef;

        [Header("Shooting Effects:")]
        public Transform gunShotEffectPref;
        public Transform EmptySpotShotEffectPref;
        [Tooltip("Transform of the barrel of the gun, for fire effects")]
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
            if (Core.GameController.Instance.isGameActive)
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

            //Vector3 initPosOfStack = stackRef.position;
            //Vector3 finalPosOfStack = stackRef.position + Vector3.down * stackReloadAnimOffset;
            //for (float i = 0; i <= 1; i+=Time.deltaTime)
            //{
            //    stackRef.position = Vector3.Lerp(initPosOfStack, finalPosOfStack, i);
            //    if (Vector3.Distance(stackRef.position, finalPosOfStack) < 0.05f)
            //    {
            //        Debug.Log("Got to final postion");
            //    }
            //    await Task.Delay(10);
            //}
            //for (float i = Time.deltaTime; i <= 1; i += Time.deltaTime)
            //{
            //    stackRef.position = Vector3.Lerp(finalPosOfStack, initPosOfStack, i);
            //    await Task.Delay(10);
            //}
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
