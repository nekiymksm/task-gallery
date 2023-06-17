using System.Collections;
using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui
{
    public class PicView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _picButton;

        public bool IsLoaded { get; private set; }

        private void Start()
        {
            _canvasGroup.alpha = 0;
            IsLoaded = false;
            
            _picButton.onClick.AddListener(OnPicButtonClick);
        }
        
        private void OnDestroy()
        {
            _picButton.onClick.RemoveListener(OnPicButtonClick);
        }

        public IEnumerator ImageLoad(string url)
        {
            IsLoaded = true;
            
            var request = UnityWebRequestTexture.GetTexture(url);
        
            yield return request.SendWebRequest();
            
            var texture = DownloadHandlerTexture.GetContent(request);
            var sprite = Sprite
                .Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            
            _canvasGroup.alpha = 1;
            _picButton.image.sprite = sprite;
            
            request.Dispose();
        }

        private void OnPicButtonClick()
        {
            LinksHolder.GetInstance().Hold(_picButton.image.sprite);
            
            SceneManager.LoadSceneAsync((int)SceneKind.Overview, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync((int)SceneKind.LoadingScene, LoadSceneMode.Additive);
        }
    }
}