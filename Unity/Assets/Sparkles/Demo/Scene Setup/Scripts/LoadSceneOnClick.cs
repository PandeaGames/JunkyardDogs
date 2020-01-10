using UnityEngine;
using UnityEngine.SceneManagement;

namespace SparkleFX
{

public class LoadSceneOnClick : MonoBehaviour
{
	public bool GUIHide = false;
	
    public void LoadSceneDemo1()
    {
        SceneManager.LoadScene("SparklesDemo1");
    }
    public void LoadSceneDemo2()
    {
        SceneManager.LoadScene("SparklesDemo2");
    }
    public void LoadSceneDemo3()
    {
        SceneManager.LoadScene("SparklesDemo3");
    }
    public void LoadSceneDemo4()
    {
        SceneManager.LoadScene("SparklesDemo4");
    }
	
	public void LoadBloomSceneDemo1()
    {
        SceneManager.LoadScene("SparklesBloomDemo1");
    }
    public void LoadBloomSceneDemo2()
    {
        SceneManager.LoadScene("SparklesBloomDemo2");
    }
    public void LoadBloomSceneDemo3()
    {
        SceneManager.LoadScene("SparklesBloomDemo3");
    }
    public void LoadBloomSceneDemo4()
    {
        SceneManager.LoadScene("SparklesBloomDemo4");
    }
	
	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.L))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
         }
     }
     }
}
}