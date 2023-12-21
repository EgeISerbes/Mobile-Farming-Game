using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ParticleSystem _harvestedParticles;
    public void ScaleUp()
    {
        //transform.localScale = Vector3.one * 3;
        transform.gameObject.LeanScale(Vector3.one * 3, 1).setEase(LeanTweenType.easeOutBack);
    }
    public void ScaleDown()
    {
        //transform.localScale = Vector3.one * 3;
        _harvestedParticles.transform.parent = null;
        _harvestedParticles.gameObject.SetActive(true);
        _harvestedParticles.Play();
        transform.gameObject.LeanScale(Vector3.zero, 1).setEase(LeanTweenType.easeOutBack).setOnComplete(() => Destroy(gameObject));
        
    }
    
}
