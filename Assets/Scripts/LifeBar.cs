using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.life
{
    public class LifeBar : MonoBehaviour
    {
        public LifeEngine lifeEngRef;

        private Camera cam;
        private Slider sliderLife;
        // Start is called before the first frame update
        void Start()
        {
            if (lifeEngRef == null)
            {
                Debug.LogError("Missing LifeEngine Reference");
            }

            sliderLife = GetComponentInChildren<Slider>();
            if (sliderLife == null)
            {
                Debug.LogError("Missing Slider in hirearchy");
            }
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)));
            sliderLife.value = Mathf.Clamp01(lifeEngRef.LifeLeft / lifeEngRef.maxLife);
        }
    }
}
