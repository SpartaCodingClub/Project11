using DG.Tweening;
using UnityEngine;
using VInspector;

public class RainController : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private Camera worldCamera;

    [SerializeField, ReadOnly]
    private float rainIntensity = 1.0f;
    #endregion

    private ParticleSystem RainFallParticleSystem;
    private ParticleSystem RainMistParticleSystem;
    private ParticleSystem RainExplosionParticleSystem;
    private float initialEmissionRain;

    private readonly ParticleSystem.Particle[] particles = new ParticleSystem.Particle[2048];

    private Bounds visibleBounds;
    private float visibleWorldWidth;

    private float lastRainIntensityValue;

    private void Awake()
    {
        RainFallParticleSystem = gameObject.FindComponent<ParticleSystem>(nameof(RainFallParticleSystem));
        RainMistParticleSystem = gameObject.FindComponent<ParticleSystem>(nameof(RainMistParticleSystem));
        RainExplosionParticleSystem = gameObject.FindComponent<ParticleSystem>(nameof(RainExplosionParticleSystem));
        initialEmissionRain = RainFallParticleSystem.emission.rateOverTime.constant;
    }

    private void Update()
    {
        CheckForRainChange();

        visibleBounds.min = worldCamera.ViewportToWorldPoint(Vector3.zero);
        visibleBounds.max = worldCamera.ViewportToWorldPoint(Vector3.one);
        visibleWorldWidth = visibleBounds.size.x;

        TransformParticleSystem(RainFallParticleSystem);
        TransformParticleSystem(RainMistParticleSystem);
        TransformParticleSystem(RainExplosionParticleSystem);
    }

    private void FixedUpdate()
    {
        CheckForCollisionsRainParticles();
        CheckForCollisionsMistParticles();
    }

    public void Stop()
    {
        DontDestroyOnLoad(this);

        DOTween.To(() => rainIntensity, value => rainIntensity = value, 0.0f, 1.0f);
        Managers.Audio.Stop_Ambient();

        DOVirtual.DelayedCall(5.0f, () => Destroy(gameObject));
    }

    private void CheckForRainChange()
    {
        if (lastRainIntensityValue == rainIntensity)
        {
            return;
        }
        lastRainIntensityValue = rainIntensity;

        if (RainFallParticleSystem != null)
        {
            ParticleSystem.EmissionModule emission = RainFallParticleSystem.emission;
            ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;

            float emissionRate = initialEmissionRain * rainIntensity;
            rateOverTime.constantMin = rateOverTime.constantMax = emissionRate;
            emission.rateOverTime = rateOverTime;
        }

        if (RainMistParticleSystem != null)
        {
            ParticleSystem.MainModule main = RainMistParticleSystem.main;
            ParticleSystem.EmissionModule emission = RainMistParticleSystem.emission;
            ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;

            float emissionRate = main.maxParticles / main.startLifetime.constant * rainIntensity * rainIntensity;
            rateOverTime.constantMin = rateOverTime.constantMax = emissionRate;
            emission.rateOverTime = rateOverTime;
        }
    }

    private void TransformParticleSystem(ParticleSystem particleSystem)
    {
        particleSystem.transform.SetPositionY(visibleBounds.max.y);
        particleSystem.transform.localScale = new(visibleWorldWidth * 2.0f, 1.0f, 1.0f);
    }

    private void CheckForCollisionsRainParticles()
    {
        bool isChange = false;
        int size = RainFallParticleSystem.GetParticles(particles);

        for (int i = 0; i < size; i++)
        {
            Vector2 origin = particles[i].position + RainFallParticleSystem.transform.position;
            Vector2 direction = particles[i].velocity.normalized;
            float distance = particles[i].velocity.magnitude * Time.deltaTime;
            if (Physics2D.Raycast(origin, direction, distance).collider == null)
            {
                continue;
            }

            particles[i].remainingLifetime = Mathf.Min(particles[i].remainingLifetime, Random.Range(0.01f, 0.04f));
            isChange = true;
        }

        for (int i = 0; i < size; i++)
        {
            if (particles[i].remainingLifetime > 0.2f)
            {
                continue;
            }

            Vector3 position = particles[i].position + RainFallParticleSystem.transform.position;
            Emit(position);
        }

        if (isChange)
        {
            RainFallParticleSystem.SetParticles(particles, size);
        }
    }

    private void CheckForCollisionsMistParticles()
    {
        bool isChange = false;
        int size = RainMistParticleSystem.GetParticles(particles);

        for (int i = 0; i < size; i++)
        {
            Vector2 position = particles[i].position + RainMistParticleSystem.transform.position;
            Vector2 direction = particles[i].velocity.normalized;
            float distance = particles[i].velocity.magnitude * Time.deltaTime;
            if (Physics2D.Raycast(position, direction, distance).collider == null)
            {
                continue;
            }

            particles[i].velocity *= 0.5f;
            isChange = true;
        }

        if (isChange)
        {
            RainMistParticleSystem.SetParticles(particles, size);
        }
    }

    private void Emit(Vector3 position)
    {
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(1.0f, 3.0f);
            ParticleSystem.EmitParams emitParams = new()
            {
                position = position,
                velocity = new(x, y, 0.0f),
                startLifetime = Random.Range(0.1f, 0.2f),
                startSize = Random.Range(0.05f, 0.1f)
            };

            RainExplosionParticleSystem.Emit(emitParams, 1);
        }
    }
}