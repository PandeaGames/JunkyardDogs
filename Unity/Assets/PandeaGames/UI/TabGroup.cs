using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandeaGames.UI
{
	public class TabGroup : MonoBehaviour
	{
		public const int NO_SELECTION_INDEX = -1;

		public event Action<int> OnIndexChanged;

		[SerializeField] private List<Tab> _tabs;

		private int _selectedIndex = NO_SELECTION_INDEX;

		protected virtual void Start()
		{
			for (var i = 0; i < _tabs.Count; i++)
			{
				_tabs[i].OnRequestFocus += OnRequestFocus;
			}
		}

		protected virtual void OnRequestFocus(Tab tab)
		{
			SetSelectedIndex(_tabs.IndexOf(tab));
		}

		public void SetSelectedIndex(int index)
		{
			if (index != _selectedIndex)
			{
				if (index > -1 && index < _tabs.Count)
				{
					if (_selectedIndex != NO_SELECTION_INDEX)
					{
						_tabs[_selectedIndex].Blur();
					}

					_selectedIndex = index;

					if (index != NO_SELECTION_INDEX)
					{
						_tabs[index].Focus();
					}

					if (OnIndexChanged != null)
					{
						OnIndexChanged(index);
					}
				}
				else
				{
					throw new IndexOutOfRangeException("Attempting to set tabs to invalid index "+index);
				}
			}
		}
	}
}
