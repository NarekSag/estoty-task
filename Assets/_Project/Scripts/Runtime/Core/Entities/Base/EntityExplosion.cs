using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityExplosion
{
    private ParticleSystem _explosionParticle;

    public EntityExplosion(ParticleSystem explosionParticle)
    {
        _explosionParticle = explosionParticle;
    }

    public void Play()
    {
        _explosionParticle.gameObject.SetActive(true);
        _explosionParticle.Play();
    }

    public void Stop()
    {
        _explosionParticle.Stop();
        _explosionParticle.gameObject.SetActive(false);
    }
}
