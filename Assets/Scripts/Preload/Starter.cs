using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
	void Awake()
	{
		FindObjectOfType<SceneLoader>().LoadScene(SceneLoader.Scenes.GameScreen);
	}
}
