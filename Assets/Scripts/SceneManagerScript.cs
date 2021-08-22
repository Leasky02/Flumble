using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private string thisScene;
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
