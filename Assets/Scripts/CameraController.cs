using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float zoomSpeed = 10;
    private Vector3 startPosition;
    private new Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UiController.Instance.HideOnMapUi();
        startPosition = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 cameraTransform = Vector3.zero;
        cameraTransform.x = eventData.pointerCurrentRaycast.worldPosition.x - startPosition.x;
        cameraTransform.y = eventData.pointerCurrentRaycast.worldPosition.y - startPosition.y;
        camera.transform.position -= cameraTransform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UiController.Instance.ShowOnMapUi();
    }
}
