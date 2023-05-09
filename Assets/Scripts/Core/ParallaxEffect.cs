using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrenght = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 previousTargetPosition;

    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        previousTargetPosition = followingTarget.position;
    }

    void Update()
    {
        var delta = followingTarget.position - previousTargetPosition;

        if (disableVerticalParallax)
            delta.y = 0;

        previousTargetPosition = followingTarget.position;
        transform.position += delta * parallaxStrenght;
    }
}
