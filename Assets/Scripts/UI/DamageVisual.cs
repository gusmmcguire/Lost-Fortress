using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageVisual : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;

    public void Initialize(float damage, Color baseColor, Color highDamageColor)
    {
        damageText.color = Color.Lerp(baseColor, highDamageColor, damage * .01f);
        damageText.text = damage.ToString();
    }

    public void EndOfLife()
    {
        Destroy(gameObject);
    }

	private void OnDisable()
	{
        EndOfLife();
	}
}
