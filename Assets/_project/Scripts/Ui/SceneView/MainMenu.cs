using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui.SceneView
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _galleryButton;

        private void Start()
        {
            _galleryButton.onClick.AddListener(OnGalleryButtonClick);
        }

        private void OnDestroy()
        {
            _galleryButton.onClick.RemoveListener(OnGalleryButtonClick);
        }

        private void OnGalleryButtonClick()
        {
            SceneManager.LoadSceneAsync((int)SceneKind.Gallery);
            SceneManager.LoadSceneAsync((int)SceneKind.LoadingScene, LoadSceneMode.Additive);
        }
    }
}