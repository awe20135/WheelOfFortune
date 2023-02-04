using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Utilities
{
    public class ColorRandomizer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Image image = GetComponent<Image>();

            image.color = Random.ColorHSV(.1f, .3f, .8f, 1, .7f, 1);
        }
    }
}
