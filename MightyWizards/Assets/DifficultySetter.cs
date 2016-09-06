using UnityEngine;
using System.Collections;

public class DifficultySetter : MonoBehaviour {

	[Tooltip("Global value multiplier")]
    [SerializeField] private float multiplier;

    public void ApplyDifficulty ()
    {
        PlayerPrefs.SetFloat("Multiplier", multiplier);
    }
}
