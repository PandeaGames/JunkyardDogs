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
        
        public SceneView(string sceneName)
        {
            _sceneName = sceneName;
        }
        
        public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
        {
            try
            {
                AsyncOperation asyncLoad =
                    SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
                
                asyncLoad.completed += (operation) =>
                {
                    Scene scene = SceneManager.GetSceneByName(_sceneName);

                    foreach (GameObject go in scene.GetRootGameObjects())
                    {
                        go.transform.SetParent(GetTransform());
                    }
                    
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