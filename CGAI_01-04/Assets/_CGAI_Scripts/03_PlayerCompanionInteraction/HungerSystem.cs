using System;
using System.Collections;
using UnityEngine;
using _CreativeGameAI_Scripts.Chapter03_PlayerCompanionInteraction;
namespace _CGAI_Scripts._03_PlayerCompanionInteraction
{
    public class HungerSystem : MonoBehaviour, IFeedable
    {
        [SerializeField] private float currentHungerSatisfaction = 100;
        [SerializeField] private float hungryThreshold = 50f;
        [SerializeField] private float hungerSatisfactionDecreaseRate = 0.5f;
        [SerializeField] private float satisfactionSustainDuration = 5;
        [SerializeField] private bool isSatisfied = true;
        [SerializeField] private bool isHungry = false;
        [SerializeField] private bool isStarving = false;

        [SerializeField] private float checkHungerStatusInterval = 2;

        public event Action PetFeelingHungry;
        public event Action PetFeelingHungerSatisfied;

        private void Start()
        {
            StartCoroutine(StartSatisfactionTimer(satisfactionSustainDuration));
            InvokeRepeating(nameof(CheckHungerStatus), 0, checkHungerStatusInterval);
        }

        private void Update()
        {
            DecreaseHungerSatisfaction();
        }
        
        private IEnumerator StartSatisfactionTimer(float satisfactionDuration)
        {
            float satisfactionTimer = satisfactionDuration;
            isSatisfied = true;
            while (satisfactionTimer > 0)
            {
                satisfactionTimer -= Time.deltaTime;
                yield return null;
            }
            isSatisfied = false;
        }
    
        private void DecreaseHungerSatisfaction()
        {
            if (isSatisfied) { return; }
            currentHungerSatisfaction -= hungerSatisfactionDecreaseRate * Time.deltaTime;
            currentHungerSatisfaction = Mathf.Max(currentHungerSatisfaction, 0f);
        }
        
        private void CheckHungerStatus()
        {
            if (!IsBelowHungryThreshold())
            {
                isHungry = false;
                isStarving = false;
                return;
            }
            
            if (IsBelowHungryThreshold())
            {
                OnPetFeelsHungry();
            }

            if (IsBelowStarvingThreshold())
            {
                isStarving = true;
                // OnPetIsStarving();
            }
        }
        
        private bool IsBelowHungryThreshold()
        {
            return currentHungerSatisfaction < hungryThreshold;
        }

        private bool IsBelowStarvingThreshold()
        {
            return currentHungerSatisfaction < 0;
        }

        private void OnPetFeelsHungry()
        {
            if (isHungry == false)
            {
                PetFeelingHungry?.Invoke();
                isHungry = true;
            }
        }

        private void EatFood(float hungerSatisfactionIncreaseAmount)
        {
            currentHungerSatisfaction += hungerSatisfactionIncreaseAmount;
            currentHungerSatisfaction = Mathf.Min(currentHungerSatisfaction, 100f);
            if (IsBelowHungryThreshold() == false)
            {
                OnPetFeelingHungerSatisfied();
            }
            StartCoroutine(StartSatisfactionTimer(satisfactionSustainDuration));
        }

        private void OnPetFeelingHungerSatisfied()
        {
            PetFeelingHungerSatisfied?.Invoke();
        }

        public void Feed(float satisfactionAmount)
        {
            EatFood(satisfactionAmount);
        }
    }
}
