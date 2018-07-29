using UnityEngine;
using System.Collections;

public class DialogStage : MonoBehaviour
{
    [Header("Required")]
    [SerializeField]
    private Transform _stage;

    [Header("Optional")]
    [SerializeField]
    private Transform _touchBlocker;

    private void Start()
    {
        if (_touchBlocker)
        {
            _touchBlocker.gameObject.SetActive(false);
        }
    }

    public void ShowDialog(GameObject prefab, Dialog.Config config, Dialog.DialogResponseDelegate response = null)
    {
        GameObject prefabInstance = Instantiate(prefab, _stage);

        Dialog dialog = prefabInstance.GetComponent<Dialog>();

        if (!dialog)
        {
            Debug.LogError("Dialog component not found on prefab. Cannot display: " + prefab);
            return;
        }

        dialog.OnCancel += OnDialogCancel;
        dialog.OnClose += OnDialogClose;

        dialog.Setup(config, response);

        if (_touchBlocker)
        {
            _touchBlocker.gameObject.SetActive(true);
        }
    }

    private void BlurDialog(Dialog dialog)
    {
        dialog.Blur();
    }

    private void OnDialogClose(Dialog dialog)
    {
        BlurDialog(dialog);
        Destroy(dialog.gameObject);

        if(_touchBlocker)
        {
            _touchBlocker.gameObject.SetActive(false);
        }
    }

    private void OnDialogCancel(Dialog dialog)
    {
        Destroy(dialog.gameObject);

        if (_touchBlocker)
        {
            _touchBlocker.gameObject.SetActive(false);
        }
    }
}
