using System.Collections;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PandeaGames.Views
{
    public class SceneView : ContainerView
    {
        private string _sceneName;
        private Scene _scene;
        
        public SceneView(string sceneName)
        {
            _sceneName = sceneName;
        }

        public override void Destroy()
        {
            base.Destroy();
            SceneManager.UnloadSceneAsync(_scene);
        }

        public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            try
            {
                AsyncOperation asyncLoad =
                    SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
                
                asyncLoad.completed += (operation) =>
                {
                    _scene = SceneManager.GetSceneByName(_sceneName);

                    /*foreach (GameObject go in _scene.GetRootGameObjects())
                    {
                        go.transform.SetParent(GetTransform());
                    }*/
                    
                    onLoadSuccess();
                };
                
            }
            catch (Exception e)
            {
                onLoadError(new LoadException("There was a problem loading scene data", e));
            }
        }
    }
}