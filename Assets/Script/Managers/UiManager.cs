using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private ComboDisplay d_combo;
    [SerializeField] private ScoreManager s_manager;
    [SerializeField] private Landscape landscape;
    
    [SerializeField] private Slider l_bar;

    [SerializeField] private Image fill;
    [SerializeField] private Image fillTmer;

    [SerializeField] private Animator hitScreen;
    [SerializeField] private Animator popUp;

    [SerializeField] private string spawnTxt;

    private float m_life;
    private Color32 fillColor;
    
    private void Awake()
    {
        Instance = this;
        fillColor = fill.color;
    }

    private void Update()
    {
        
    }
    public void PlusOne(int amount, int id)
    {
        d_combo.TriggerAnim(id);
        d_combo.UpdateTxt(amount);

        s_manager.UpdateOnCombo(amount);
    }
    public void HasKill()
    {
        s_manager.UpdateOnKill();
    }
    public void SetComboPos(int id)
    {
        d_combo.SetPos(id);
    }
    public void ComboState(int id)
    {
        d_combo.TriggerAnim(id);
        if (id == 3)
        {
            s_manager.ComboEnd();
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdatePlayerLife(int value, int c_life)
    {
        if (value >= 1)
            hitScreen.SetTrigger("getHit");

        float ratio = c_life / m_life;
        l_bar.value = ratio;

        if (ratio >= 0.1f && ratio < 1f)
            ratio += 0.1f;

        fill.color = new Color32(fillColor.r, fillColor.g, fillColor.b, (byte) (ratio * 255));
    }
    public void SetPlayerMaxLife(int max)
    {
        m_life = max;
        
        UpdatePlayerLife(0, (int) m_life);
    }
    public void UpdateSpawnTimer(float value, float max)
    {
        fillTmer.fillAmount = value / max;
    }
    public void SpawnEffect()
    {
        popUp.SetTrigger("popUp");
        landscape.NextLandscape();
        popUp.transform.GetChild(0).GetComponent<Text>().text = spawnTxt;
    }
}