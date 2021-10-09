using UnityEngine;
using UnityEngine.UI;

namespace alexshko.prisonescape.life
{
    public class LifeBar : MonoBehaviour
    {
        public LifeEngine lifeEngRef;

        private Slider sliderLife;
        // Start is called before the first frame update
        void Awake()
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
        }

        // Update is called once per frame
        void Update()
        {
            sliderLife.value = Mathf.Clamp01(lifeEngRef.LifeLeft / lifeEngRef.maxLife);
        }
    }
}
