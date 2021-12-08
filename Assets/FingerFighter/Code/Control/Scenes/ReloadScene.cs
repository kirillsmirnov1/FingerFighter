using UnityEngine;
using UnityEngine.SceneManagement;

namespace FingerFighter.Control.Scenes
{
    public class ReloadScene : MonoBehaviour
    {
        public void Reload()
        {
            SceneManagerCustom.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}