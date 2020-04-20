﻿namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;
    using Sirenix.OdinInspector;

    public class Egg : MonoBehaviour {
        [Header("Refs"), Required, HideInInspector]
        public Cell cell;
        [ReadOnly]
        public bool isInDanger { get; private set; } = false;

        [Header("Events")]
        public UnityEvent OnSpawn;
        public UnityEvent OnDeath;
        public UnityEvent Danger;
        public UnityEvent NotDanger;

        [ShowInInspector, Required]
        private Cell InspectorCell {
            get => cell;
            set {
                cell = value;
                this.transform.position = cell.transform.position + Vector3.up * -1;
            }
        }

        public void SetEggDanger () {
            isInDanger = true;
            Danger.Invoke();
        }

        public void SetEggSafe () {
            isInDanger = false;
            NotDanger.Invoke();
        }

        public void Spawn () {
            transform.position = cell.transform.position - Vector3.up * 1;
            OnSpawn.Invoke();
        }

        public void Die () {
            OnDeath.Invoke();
        }
    }
}