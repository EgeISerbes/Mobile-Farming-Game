using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ParticleSystem _seedParticles;
    [SerializeField] private ParticleSystem _waterParticles;

    [Header(" Events ")]
    [SerializeField] private UnityEvent _startHarvestingEvent;
    [SerializeField] private UnityEvent _stopHarvestingEvent;
    
    private void PlaySeedParticles()
    {
        _seedParticles.Play();
    }
    private void PlayWaterParticles()
    {
        _waterParticles.Play();
    }
    private void StartHarvestingCallback()
    {
        _startHarvestingEvent?.Invoke();
    }
    private void StopHarvestingCallback()
    {
        _stopHarvestingEvent?.Invoke();

    }
}
