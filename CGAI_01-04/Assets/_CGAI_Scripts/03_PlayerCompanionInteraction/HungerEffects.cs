using System.Collections;
using _CGAI_Scripts._03_PlayerCompanionInteraction;
using _CreativeGameAI_Scripts.Chapter03_PlayerCompanionInteraction;
using UnityEngine;
using UnityEngine.UI;

namespace _CGAI_Scripts.CGAI_03_PlayerCompanionInteraction
{
    public class HungerEffects : MonoBehaviour
    {
        [SerializeField] private Image hungryIcon;
        [SerializeField] private HungerSystem hungerSystem;
        [SerializeField] private float iconEffectTime = 1;
        [SerializeField] private float iconEffectTargetY = 2;
        
        private GameObject _hungryIconGameObject;
        private bool _isEffectLooping = false;
        private Vector3 _originalIconPosition;
    
        private void OnEnable()
        {
            SubscribeToHungerEvents();
            SetupIcons();
        }

        private void SubscribeToHungerEvents()
        {
            hungerSystem.PetFeelingHungry += StartFeelingHungryEffect;
            hungerSystem.PetFeelingHungerSatisfied += StopFeelingHungryEffect;
        }
        
        private void SetupIcons()
        {
            _hungryIconGameObject = hungryIcon.gameObject;
            _hungryIconGameObject.SetActive(false);
            _originalIconPosition = hungryIcon.transform.localPosition;
        }

        private void StartFeelingHungryEffect()
        {
            if (!_isEffectLooping)
            {
                StartCoroutine(FeelingHungryEffect());
            }
        }

        private IEnumerator FeelingHungryEffect()
        {
            _isEffectLooping = true;
            Transform hungryIconTransform = hungryIcon.gameObject.transform;
            hungryIconTransform.gameObject.SetActive(true);
            Vector3 targetPosition = _originalIconPosition + Vector3.up * iconEffectTargetY;
            while (_isEffectLooping)
            {
                float elapsedTime = 0;
                while (elapsedTime < iconEffectTime)
                {
                    LerpIconToTarget(hungryIconTransform, targetPosition, elapsedTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                hungryIcon.transform.localPosition = _originalIconPosition;
            }
        }

        private void LerpIconToTarget(Transform iconTransform, Vector3 targetPosition, float elapsedTime)
        {
            float amountToLerp = elapsedTime / iconEffectTime;
            iconTransform.localPosition = Vector3.Lerp(_originalIconPosition, targetPosition, 
                amountToLerp);
        }
        
        private void StopFeelingHungryEffect()
        {
            _isEffectLooping = false;
            hungryIcon.transform.localPosition = _originalIconPosition;
            hungryIcon.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            UnsubscribeFromHungerEffects();
            ResetIcons();
        }
        
        private void UnsubscribeFromHungerEffects()
        {
            hungerSystem.PetFeelingHungry -= StartFeelingHungryEffect;
            hungerSystem.PetFeelingHungerSatisfied -= StopFeelingHungryEffect;
        }
        
        private void ResetIcons()
        {
            hungryIcon.transform.localPosition = _originalIconPosition;
            _isEffectLooping = false;
        }
    }
}
