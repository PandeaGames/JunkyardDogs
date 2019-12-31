using System;
using System.Collections;
using JunkyardDogs.Components;
using UnityEditor;
using UnityEngine;

public class BotAnimation : MonoBehaviour
{
    [SerializeField] public Vector3 _botScale;
    [SerializeField] float _botReturnLerpTime;
    
    private BotRenderer _botRenderer;
    private JunkyardMonoView _junkyardMonoView;
    private Junkyard _junkyard;

    private int _lastXPosition;
    private int _lastYPosition;

    private Vector3 _targetPosition;
    
    public void Setup(JunkyardRenderConfig config, Junkyard junkyard, Bot bot, JunkyardMonoView junkyardMonoView)
    {
        GameObject botObject = config.BotRenderConfiguration.BotPrefabFactory.InstantiateAsset(bot.Chassis.Specification, transform, false);
        botObject.transform.localScale = _botScale;
        _botRenderer = botObject.AddComponent<BotRenderer>();
        _botRenderer.Render(bot, config.BotRenderConfiguration);
        _junkyardMonoView = junkyardMonoView;
        _junkyardMonoView.OnJunkPointerDown += JunkyardMonoViewOnOnJunkPointerDown;
        _junkyardMonoView.OnJunkCleared += JunkyardMonoViewOnOnJunkCleared;
        _junkyard = junkyard;
    }

    private void JunkyardMonoViewOnOnJunkCleared(int x, int y, JunkyardJunk junk)
    {
        //_botRenderer.transform.position = new Vector3(x, 0, y);
        _targetPosition = new Vector3(x, junk.gameObject.transform.position.y, y);
    }

    private void JunkyardMonoViewOnOnJunkPointerDown(int x, int y, JunkyardJunk junk)
    {
        bool isLastPositionAdjacent = Math.Abs(x - _lastXPosition) <= 1 && Math.Abs(y - _lastYPosition) <= 1;
        
        Vector2 adj = isLastPositionAdjacent ? new Vector2(_lastXPosition, _lastYPosition):_junkyard.GetAdjacentToCleared(x, y);

        float dx = x - adj.x;
        float dy = y - adj.y;
        float a = Mathf.Rad2Deg * Mathf.Atan2(dy, dx);
        
        _botRenderer.transform.rotation = Quaternion.Euler(0, -90 - (a +90), 0);
        Debug.LogFormat("[a:{0}] [dx:{1}] [dy:{2}]", a, dx, dy);
        
        _botRenderer.transform.position = new Vector3(adj.x, junk.gameObject.transform.position.y, adj.y);
        _targetPosition = _botRenderer.transform.position;

        _lastXPosition = x;
        _lastYPosition = y;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return null;
            if (_botRenderer != null)
            {
                _botRenderer.transform.position = Vector3.Lerp(_botRenderer.transform.position, _targetPosition, _botReturnLerpTime);
            }
        }
    }

    private void OnDestroy()
    {
        if (_junkyardMonoView != null)
        {
            _junkyardMonoView.OnJunkPointerDown -= JunkyardMonoViewOnOnJunkPointerDown;
            _junkyardMonoView.OnJunkCleared -= JunkyardMonoViewOnOnJunkCleared;
        }
    }
}
