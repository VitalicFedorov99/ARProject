using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImage : MonoBehaviour
{
    [SerializeField] private GameObject[] _arPrefabs;
    [SerializeField] private float _size;
    [SerializeField] private FactoryEnemy _factory;
    [SerializeField] private GameObject _greenBox;
    private ARTrackedImageManager _arTrackedImageManager;

    private readonly Dictionary<string, GameObject> _instantiatePrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (var prefab in _arPrefabs)
        {
            //var newPrefab = Instantiate(prefab, new Vector3(100, 100, 100), Quaternion.identity);

            var newPrefab = prefab;
            //newPrefab.SetActive(true);
            //newPrefab.transform.Rotate(new Vector3(-90, 0, 0));
            newPrefab.transform.localScale -= new Vector3(newPrefab.transform.localScale.x / _size, 0, newPrefab.transform.localScale.z / _size);
            _greenBox.transform.localScale -= new Vector3(0, _greenBox.transform.localScale.y / _size, 0);
            newPrefab.name = prefab.name;
            newPrefab.gameObject.SetActive(false);
            _instantiatePrefabs.Add(prefab.name, newPrefab);
        }
    }


    void Update()
    {
        if (_instantiatePrefabs["Lunka"].active)
        {
            _arTrackedImageManager.trackedImagesChanged -= ImageChanged;
            return;
        }


        _arTrackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            _instantiatePrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage image)
    {
        AssignGameObject(image.referenceImage.name, image.transform.position);
    }

    private void AssignGameObject(string name, Vector3 position)
    {
        if (_instantiatePrefabs == null)
            return;
        _instantiatePrefabs[name].SetActive(true);
        _instantiatePrefabs[name].transform.position = position;
        //_factory.StartWork();
        foreach (var go in _instantiatePrefabs.Values)
        {
            if (go.name != name)
                go.SetActive(false);
        }
    }
}


