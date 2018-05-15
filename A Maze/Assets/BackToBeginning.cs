using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToBeginning : MonoBehaviour {

	public void ChangeScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
