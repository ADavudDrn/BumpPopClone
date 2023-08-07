using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int SceneIndex = 0;
        
        public void LoadScene()
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}