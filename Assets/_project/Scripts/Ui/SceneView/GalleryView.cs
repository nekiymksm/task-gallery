using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace _project.Scripts.Ui.SceneView
{
    public class GalleryView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private PicView _picViewPrefab;
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private Mask _mask;
        [SerializeField] private string _url;
        
        private List<PicView> _picViews;

        private void Start()
        {
            _picViews = new List<PicView>();
            StartCoroutine(LoadDirectory());
        }

        private void OnDestroy()
        {
            _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }
        
        private IEnumerator LoadDirectory()
        {
            var request = UnityWebRequest.Get(_url);
            Regex regex = new Regex("pics/.*jpg(\\w*)");
        
            yield return request.SendWebRequest();

            var itemsCount = regex.Matches(request.downloadHandler.text).Count;
            
            for (int i = 0; i < itemsCount; i++)
            {
                _picViews.Add(Instantiate(_picViewPrefab, _contentTransform));
            }
            
            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

            request.Dispose();
        }
        
        private void TryLoadView()
        {
            for (int i = 0; i < _picViews.Count; i++)
            {
                float yAxisPosition = _picViews[i].transform.position.y;
                bool isShow = yAxisPosition > 0 && yAxisPosition < _mask.rectTransform.rect.size.y;

                if (_picViews[i].IsLoaded == false && isShow)
                {
                    StartCoroutine(_picViews[i].ImageLoad(_url + $"{i + 1}.jpg"));
                }
            }
        }

        private void OnScrollValueChanged(Vector2 position)
        {
            TryLoadView();
        }
    }
}