using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

[Serializable] public class HealthEvent : UnityEvent<float> { }
[Serializable] public class RoundEvent : UnityEvent<int> { }
[Serializable] public class MashFloatEvent : UnityEvent<float> { }
[Serializable] public class MashStringEvent : UnityEvent<string> { }
[Serializable] public class ResourceEvent : UnityEvent<PickupData, int> { }
[Serializable] public class ModelLoadEvent : UnityEvent<GameObject> { }

