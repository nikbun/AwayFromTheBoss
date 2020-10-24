using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float xShift = 0f;
    [SerializeField] private float yShift = 0f;
    [SerializeField] private float zShift = 0f;
    [Range(0.001f, 1f)]
    [Tooltip("Скорость реакции камеры")]
    [SerializeField] private float _reactionSpeed = 0.5f;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition = new Vector3(targetPosition.x + xShift, targetPosition.y + yShift, targetPosition.z + zShift);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _reactionSpeed);
    }
}