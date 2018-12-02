using System.Collections;
using System.Collections.Generic;
using PandeaGames;
using UnityEngine;
using PandeaGames.Views;
using PandeaGames.Views.Screens;

public class SimpleWindowView : AbstractUnityView
{
	private GameObject _unityView;

	public override void Show()
	{
		ViewStaticDataProvider data = Game.Instance.GetStaticDataPovider<ViewStaticDataProvider>();
		ViewStaticData viewData = data.ViewStaticData;
		GameObject prefab = viewData.SimpleWindowPrefab;
		
		_unityView = GameObject.Instantiate(prefab);

		_window = _unityView.GetComponentInChildren<WindowView>();
		_serviceManager = _unityView.GetComponentInChildren<ServiceManager>();
	}
}
