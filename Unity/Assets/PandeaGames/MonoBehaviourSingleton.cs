using UnityEngine;

namespace PandeaGames
{
    public class MonoBehaviourSingleton<TBehaviour> :MonoBehaviour where  TBehaviour:MonoBehaviour, new()
    {
        private static TBehaviour instance;
        
        public static TBehaviour Instance
        {
            get
            {
                if (instance == null)
                {
                    TBehaviour instanceInScene = GameObject.FindObjectOfType<TBehaviour>();

                    if (instanceInScene == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.name = typeof(TBehaviour).ToString();
                        instance = gameObject.AddComponent<TBehaviour>();
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