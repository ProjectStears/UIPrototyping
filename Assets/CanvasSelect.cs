using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CanvasSelect : MonoBehaviour
{
    private Dictionary<string, GameObject> _canvases;
    private GameObject _root;
    public GameObject StartCanvas;

    private void Start()
    {
        _root = GameObject.Find("root");
        _canvases = new Dictionary<string, GameObject>();

        for (int i = 0; i < _root.transform.childCount; i++)
        {
            var child = _root.transform.GetChild(i);

            if (!_canvases.ContainsKey(child.name))
            {
                _canvases.Add(child.name, child.gameObject);
            }
            else
            {
                Debug.LogWarning("Duplicate Canvas name '" + child.name + "' found.");
            }
        }

        List<string> canvasNames = _canvases.Keys.ToList();
        canvasNames.Sort();
        string canvasNamess = String.Join(", ", canvasNames.ToArray());

        Debug.Log("Available canvas names: " + canvasNamess);

        if (StartCanvas != null)
        {
            ChangeCanvas(StartCanvas.name);
        }
        else
        {
            Debug.LogWarning("No Start Canvas set.");
        }
    }

    public void ChangeCanvas(string canvasName)
    {
        if (_canvases.ContainsKey(canvasName))
        {
            foreach (var canvase in _canvases)
            {
                canvase.Value.SetActive(false);
            }

            _canvases[canvasName].SetActive(true);
            Debug.Log("Switched to canvas: " + canvasName);
        }
        else
        {
            Debug.LogWarning("No canvas with the name '" + canvasName + "' found.");
        }
    }

    public void ChangeCanvas(GameObject canvasGameObject)
    {
        ChangeCanvas(canvasGameObject.name);
    }
}
