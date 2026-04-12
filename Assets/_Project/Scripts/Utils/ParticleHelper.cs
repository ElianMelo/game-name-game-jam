using UnityEngine;

/// <summary>
/// This is a middleman script that hosts a bunch of useful particle system related methods
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleHelper : MonoBehaviour
{
    private ParticleSystem ParticleRef;
    private ParticleSystem.EmissionModule EmissionRate;
    private ParticleSystem.MainModule ParticleRefMain;

    private void Awake()
    {
        ParticleRef = GetComponent<ParticleSystem>();
        EmissionRate = ParticleRef.emission;
        ParticleRefMain  = ParticleRef.main;
    }

    // Playback


    /// <summary>Plays the particle system (does not loop by default).</summary>
    public void Play()
    {
        ParticleRef.Play();
    }

    /// <summary>Stops emission and clears all existing particles.</summary>
    public void Stop()
    {
        ParticleRef.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    /// <summary>Stops new emission but lets existing particles finish.</summary>
    public void StopEmitting()
    {
        ParticleRef.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    /// <summary>Pauses the particle system.</summary>
    public void Pause()
    {
        ParticleRef.Pause();
    }

    // Emission Rate Over Time

    /// <summary>
    /// Sets the constant emission rate over time (particles per second).
    /// </summary>
    /// <param name="rate">Particles emitted per second. Use 0 to stop emission without clearing particles.</param>
    public void SetEmissionRateOverTime(float rate)
    {
        EmissionRate.enabled         = rate > 0f;
        EmissionRate.rateOverTime    = Mathf.Max(0f, rate);
    }

    /// <summary>
    /// Multiplies the current emission rate over time
    /// </summary>
    /// <param name="multiplier">Scale factor applied to the current rate.</param>
    public void MultiplyEmissionRateOverTime(float multiplier)
    {
        float current          = EmissionRate.rateOverTime.constant;
        SetEmissionRateOverTime(current * Mathf.Max(0f, multiplier));
    }

   
    // Burst Emission


    /// <summary>
    /// Emits a one-shot burst of particles immediately.
    /// </summary>
    /// <param name="count">Number of particles to emit.</param>
    public void EmitBurst(int count)
    {
        if (count <= 0) return;
        ParticleRef.Emit(count);
    }

    // Particle Lifetime


    /// <summary>Sets the start lifetime of newly emitted particles (seconds).</summary>
    /// <param name="lifetime">Duration in seconds. Clamped to a minimum of 0.01.</param>
    public void SetStartLifetime(float lifetime)
    {
        ParticleRefMain.startLifetime = Mathf.Max(0.01f, lifetime);
    }


    // Speed

    /// <summary>Sets the start speed of newly emitted particles.</summary>
    /// <param name="speed">Speed value. Can be negative (emits inward if shape allows).</param>
    public void SetStartSpeed(float speed)
    {
        ParticleRefMain.startSpeed = speed;
    }

    // Size


    /// <summary>Sets the uniform start size of newly emitted particles.</summary>
    /// <param name="size">Size value. Clamped to 0.001 minimum.</param>
    public void SetStartSize(float size)
    {
        ParticleRefMain.startSize = Mathf.Max(0.001f, size);
    }


    // Color


    /// <summary>Sets the start color of newly emitted particles.</summary>
    /// <param name="color">Target color (alpha is respected).</param>
    public void SetStartColor(Color color)
    {
        ParticleRefMain.startColor = color;
    }

    // Gravity


    /// <summary>Sets the gravity modifier on the particle system.</summary>
    /// <param name="gravity">Gravity multiplier (1 = normal Unity gravity).</param>
    public void SetGravityModifier(float gravity)
    {
        ParticleRefMain.gravityModifier = gravity;
    }


    // Simulation Speed


    /// <summary>Scales how fast the entire particle simulation runs.</summary>
    /// <param name="speed">Simulation speed multiplier. Clamped to 0.</param>
    public void SetSimulationSpeed(float speed)
    {
        ParticleRefMain.simulationSpeed = Mathf.Max(0f, speed);
    }


    // Enable / Disable

    /// <summary>Enables or disables the emission module without touching playback.</summary>
    /// <param name="enabled">True to enable emission, false to disable.</param>
    public void SetEmissionEnabled(bool enabled)
    {
        EmissionRate.enabled = enabled;
    }
}