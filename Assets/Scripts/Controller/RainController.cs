using DG.Tweening;
using System;
using UnityEngine;

public class RainController : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private Camera worldCamera;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float rainIntensity;
    #endregion

    private ParticleSystem RainParticleSystem;

    private float initialEmissionRain;
    private Vector2 initialStartSpeedRain;
    private Vector2 initialStartSizeRain;

    private float cameraMultiplier;
    private Bounds visibleBounds;
    private float visibleWorldWidth;

    private float lastRainIntensityValue;

    private void Awake()
    {
        RainParticleSystem = GetComponent<ParticleSystem>();
        initialEmissionRain = RainParticleSystem.emission.rateOverTime.constant;
        initialStartSpeedRain = new Vector2(RainParticleSystem.main.startSpeed.constantMin, RainParticleSystem.main.startSpeed.constantMax);
        initialStartSizeRain = new Vector2(RainParticleSystem.main.startSize.constantMin, RainParticleSystem.main.startSize.constantMax);
    }

    private void Update()
    {
        CheckForRainChange();

        cameraMultiplier = worldCamera.orthographicSize * 0.25f;
        visibleBounds.min = worldCamera.ViewportToWorldPoint(Vector3.zero);
        visibleBounds.max = worldCamera.ViewportToWorldPoint(Vector3.one);
        visibleWorldWidth = visibleBounds.size.x;

        TransformParticleSystem(RainParticleSystem, initialStartSpeedRain, initialStartSizeRain);
    }

    public void Stop()
    {
        DOTween.To(() => rainIntensity, value => rainIntensity = value, 0.0f, 1.0f);
    }

    private void CheckForRainChange()
    {
        if (lastRainIntensityValue == rainIntensity)
        {
            return;
        }
        lastRainIntensityValue = rainIntensity;

        if (rainIntensity > 0.0f)
        {
            ParticleSystem.EmissionModule emission = RainParticleSystem.emission;
            ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
            rateOverTime.constantMin = rateOverTime.constantMax = initialEmissionRain * rainIntensity;
            emission.rateOverTime = rateOverTime;

            RainParticleSystem.Play();
        }
        else
        {
            RainParticleSystem.Stop();
        }
    }

    private void TransformParticleSystem(ParticleSystem particleSystem, Vector2 initialStartSpeed, Vector2 initialStartSize)
    {
        particleSystem.transform.SetPositionY(visibleBounds.max.y);
        particleSystem.transform.localScale = new(visibleWorldWidth * 1.5f, 1.0f, 1.0f);

        ParticleSystem.MainModule main = particleSystem.main;
        ParticleSystem.MinMaxCurve startSoeed = main.startSpeed;
        startSoeed.constantMin = initialStartSpeed.x * cameraMultiplier;
        startSoeed.constantMax = initialStartSpeed.y * cameraMultiplier;

        ParticleSystem.MinMaxCurve startSize = main.startSize;
        startSize.constantMin = initialStartSize.x * cameraMultiplier;
        startSize.constantMax = initialStartSize.y * cameraMultiplier;

        main.startSpeed = startSoeed;
        main.startSize = startSize;
    }
}