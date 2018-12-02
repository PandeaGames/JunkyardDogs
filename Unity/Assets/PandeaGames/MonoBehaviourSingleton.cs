using UnityEngine;

namespace PandeaGames
{
    public class MonoBehaviourSingleton<T> :MonoBehaviour where  T:MonoBehaviour, new()
    {
        private static T instance;
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T instanceInScene = GameObject.FindObjectOfType<T>();

                    if (instanceInScene == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.name = typeof(T).ToString();
                        instance = gameObject.AddComponent<T>();
                    }
                    else
                    {
                        instance = instanceInScene;
                    }
                }
                
                return instance;
            }
        }
    }
}