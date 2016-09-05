using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Canvas))]
public class DamageCanvas : MonoBehaviour {

	[SerializeField] private Text damageTemplate;

    private void Awake ()
    {
        damageTemplate.gameObject.SetActive(false);
    }

    public void ShowDamage(float damage)
    {
        Text damageText = Instantiate(damageTemplate, damageTemplate.transform.parent) as Text;
        damageText.gameObject.SetActive(true);
        damageText.rectTransform.localPosition =
            damageTemplate.rectTransform.localPosition +
            Vector3.right * Random.Range(-150f, 150f) +
            Vector3.up * Random.Range(-200f, 200f);

        damageText.text = damage.ToString("0");
        Destroy(damageText, 0.3f);
    }
}
