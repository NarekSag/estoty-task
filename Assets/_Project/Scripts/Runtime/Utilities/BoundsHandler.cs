using System;
using UnityEngine;

public class BoundsHandler
{
    private Camera _mainCamera;
    private Transform _target;

    public event Action OnOutsideBounds;

    public BoundsHandler(Transform target)
    {
        _target = target;
        _mainCamera = Camera.main;
    }

    public void CheckBounds()
    {
        if (!IsVisibleInCamera())
        {
            OnOutsideBounds?.Invoke();
        }
    }

    private bool IsVisibleInCamera()
    {
        if (_mainCamera == null) return true;

        Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(_target.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
               viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
               viewportPosition.z > 0;
    }
}