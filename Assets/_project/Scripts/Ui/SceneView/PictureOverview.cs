using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui.SceneView
{
    public class PictureOverview : MonoBehaviour
    {
        [SerializeField] private Image _picImage;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            
            _picImage.sprite = LinksHolder.GetInstance().Take<Sprite>() as Sprite;
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnCloseButtonClick();
            }
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            
            SceneManager.UnloadSceneAsync((int)SceneKind.Overview);
            SceneManager.LoadSceneAsync((int)SceneKind.LoadingScene, LoadSceneMode.Additive);
        }
    }
}