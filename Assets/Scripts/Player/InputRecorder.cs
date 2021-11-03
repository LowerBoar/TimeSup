using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder
{
	private List<(InputEvent Event, float Time)> records;
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

	public void Record(InputEvent @event, float time)
	{
		records.Add((@event, time));
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

	public InputEvent(KeyCode key, bool isPressed)
	{
		Key = key;
		IsPressed = isPressed;
	}
}
