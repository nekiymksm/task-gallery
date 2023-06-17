using System.Collections;
using _project.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui.SceneView
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private TextMeshProUGUI _percentage;
        [SerializeField] private float _duration;

        private float _fillInterval;
        
        private void Start()
        {
            _bar.fillAmount = 0;
            _fillInterval = _duration / 100;
            
            StartCoroutine(ShowLoad());
        }

        private IEnumerator ShowLoad()
        {
            while (_bar.fillAmount < 1)
            {
                SetPercentage();
                _bar.fillAmount += _fillInterval;
                
                yield return new WaitForSeconds(_fillInterval);
            }
            
            SetPercentage();
            SceneManager.UnloadSceneAsync((int)SceneKind.LoadingScene);
        }

        private void SetPercentage()
        {
            _percentage.SetText($"{(int)(_bar.fillAmount * 100)}%");
        }
    }
}