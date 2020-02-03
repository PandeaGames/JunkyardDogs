using System.Collections;
using System.Collections.Generic;
using JunkyardDogs;
using PandeaGames;
using UnityEngine;

public class LootCollectionMonoView : MonoBehaviour
{
    [SerializeField] private Transform _lootContainer;
    [SerializeField] private LootMonoView _lootMonoViewPrefab;
    
    private JunkyardUserViewModel _junkyardUserViewModel;
    // Start is called before the first frame update
    private void Start()
    {
        _junkyardUserViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        _junkyardUserViewModel.OnScreenSpaceLootConsumed += HandleScreenSpaceLootConsumed;
        _junkyardUserViewModel.OnLootConsumed += HandleLootConsumed;
    }

    private void HandleLootConsumed(IConsumable[] cratecontents)
    {
        for (int i = 0; i < cratecontents.Length; i++)
        {
            LootMonoView lootMonoView = Instantiate(
                _lootMonoViewPrefab,
                Vector3.zero,
                _lootMonoViewPrefab.transform.rotation,
                _lootContainer);
            lootMonoView.gameObject.SetActive(true);
            lootMonoView.RenderLoot(cratecontents[i]);
        }
    }

    private void HandleScreenSpaceLootConsumed(IConsumable[] cratecontents, Vector3 screenspacecollectiopoint)
    {
        //HandleLootConsumed(cratecontents);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (_junkyardUserViewModel != null)
        {
            _junkyardUserViewModel.OnScreenSpaceLootConsumed -= HandleScreenSpaceLootConsumed;
            _junkyardUserViewModel.OnLootConsumed -= HandleLootConsumed;
        }
    }
}
