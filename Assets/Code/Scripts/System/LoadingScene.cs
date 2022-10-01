using System.Collections;
using Unavinar.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unavinar.System
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private float secondsToLoad;

        private IEnumerator Start()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelManager.Instance.CurrentLevel);
            asyncLoad.allowSceneActivation = false;

            yield return new WaitForSeconds(secondsToLoad);

            asyncLoad.allowSceneActivation = true;
        }
    }
}
