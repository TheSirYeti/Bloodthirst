using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackBar : MonoBehaviour
{
    private Slider slider;

    //[SerializeField] private EnemyBase enemiesLife;
    public float FillSpeed = 0.5f;
    private float targetProgress = 0;
    
    //private float enemyLife = enemiesLife.life;

    private void Awake ()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(1f);
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseScore();
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    public void addValue(float value)
    {
        slider.value += value;
    }

    public void IncreaseScore()
    {
        slider.value += FillSpeed * Time.deltaTime;   
    }

    public float getValue()
    {
        return slider.value;
    }

    public void resetValue()
    {
        slider.value = 0f;
    }
  
}
