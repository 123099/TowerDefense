using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

[Serializable] public class HealthEvent : UnityEvent<float> { }
[Serializable] public class RoundEvent : UnityEvent<int> { }

