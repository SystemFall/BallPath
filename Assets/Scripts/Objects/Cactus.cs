using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public ParticleSystem DestroyEffectPrefab;

    private Transform _cactusTransform;
    private void Start()
    {
        _cactusTransform = this.transform;
    }
    public void CreateDestroyEffect()
    {
        GameObject effect = Instantiate(DestroyEffectPrefab.gameObject, _cactusTransform.position, Quaternion.identity);
        Destroy(effect, DestroyEffectPrefab.main.startLifetime.constant);
    }
}
