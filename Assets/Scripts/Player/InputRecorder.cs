using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder
{
	private readonly List<(InputEvent Event, float Time)> records;
	private float currentTime;
	private int currentEvent;

	public InputRecorder()
	{
		records = new List<(InputEvent, float)>();
	}

	public void Update(float deltaTime)
	{
		currentTime += deltaTime;
	}

	public void Reset()
	{
		currentTime = 0;
		currentEvent = 0;
	}

	public void Record(InputEvent @event)
	{
		records.Add((@event, currentTime));
	}

	public InputEvent[] GetEvents()
	{
		var result = new List<InputEvent>();
		for (var i = currentEvent; i < records.Count; ++i) {
			var record = records[i];
			if (record.Time <= currentTime) {
				result.Add(record.Event);
				currentEvent++;
			} else {
				return result.ToArray();
			}
		}

		return result.ToArray();
	}
}

public struct InputEvent
{
	public KeyCode Key;
	public bool IsPressed;
	public Quaternion Rotation;

	public InputEvent(KeyCode key, bool isPressed, Quaternion rotation)
	{
		Key = key;
		IsPressed = isPressed;
		Rotation = rotation;
	}
}
