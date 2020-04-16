using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StaminaGauge : MonoBehaviour
{
    public Color fullColor;
    public Color emptyColor;
    public AnimationCurve colorCurve;
    private UnityEngine.UI.Image image;
    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }
    // Update is called once per frame
    void Update()
    {
        image.fillAmount = PlayerController.instance.currentStamina / PlayerController.instance.maxStamina;
        image.color = Color.Lerp(emptyColor, fullColor, colorCurve.Evaluate(image.fillAmount));
    }
}
