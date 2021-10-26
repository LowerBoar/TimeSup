using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(Scenes scene)
	{
		SceneManager.LoadScene((int) scene);
	}

	public enum Scenes
    {
        Preload = 0,
        GameScreen = 1,
    }
}
