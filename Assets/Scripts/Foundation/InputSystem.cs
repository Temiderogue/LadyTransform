﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public static InputSystem Instance { get; private set; }

	public event Action OnTouch;
	public event Action OnRelease;
	public event Action<SwipeDirection> OnSwipe;
	public event Action<Vector2> OnDragAction;

	public enum SwipeDirection { Up, Down, Right, Left }

	private Camera _mainCamera;

	private Vector3 _startTouchPosition;
	private Vector3 _endTouchPosition;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		_mainCamera = Camera.main;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnTouch?.Invoke();
		GameC.Instance.isInGame = true;

		//GetSwipePoints(ref _startTouchPosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		OnRelease?.Invoke();

		//GetSwipePoints(ref _endTouchPosition);
		//Swipe();
	}

	public void OnDrag(PointerEventData eventData)
	{
		OnDragAction?.Invoke(eventData.delta);
	}

	//private void GetSwipePoints(ref Vector3 point) => point = _mainCamera.ScreenToWorldPoint(
	//	new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));

	//public void Swipe()
	//{
	//	var delta = _endTouchPosition - _startTouchPosition;
	//	var absDelta = delta.Abs();

	//	if (delta.x > 0 && absDelta.x > absDelta.y) OnSwipe?.Invoke(SwipeDirection.Right);
	//	if (delta.x < 0 && absDelta.x > absDelta.y) OnSwipe?.Invoke(SwipeDirection.Left);
	//	if (delta.y > 0 && absDelta.x < absDelta.y) OnSwipe?.Invoke(SwipeDirection.Up);
	//	if (delta.y < 0 && absDelta.x < absDelta.y) OnSwipe?.Invoke(SwipeDirection.Down);
	//}
}