using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PrimeTween;
using static PowerUp;

public class PowerUpView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void ShowPowerUp(PowerUpType powerUpType)
    {
        text.text = GetPowerUpText(powerUpType);

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.gameObject.SetActive(true);

        Sequence.Create()
            .Chain(Tween.Alpha(text, 1f, 0.5f))
            .Chain(Tween.Delay(2f))              
            .Chain(Tween.Alpha(text, 0f, 0.5f))
            .OnComplete(() => text.gameObject.SetActive(false));
    }

    private string GetPowerUpText(PowerUpType powerUpType)
    {
        return powerUpType switch
        {
            PowerUpType.FIRE_RATE => "Fire Rate Boost!",
            PowerUpType.HEALTH => "Health Restored!",
            PowerUpType.DAMAGE => "Damage Increased!",
            PowerUpType.PROJECTILE_SPEED => "Projectile Speed Boost!",
            _ => "Power-Up!"
        };
    }
}
