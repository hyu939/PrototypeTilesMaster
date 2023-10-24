using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private SpawnTile_Script mapData;
    [SerializeField] private TextMeshProUGUI uiTime;

    public int Duration { get; private set; }


    private int remainingDuration;

    public GameObject loseWindow;

    private void Awake()
    {
        ResetTimer();
    }

    private void Start()
    {
        mapData = GetComponent<SpawnTile_Script>();
        SetDuration(mapData.mapData.time).Begin();
    }

    private void ResetTimer()
    {
        uiTime.text = "00:00";
        Duration = remainingDuration = 0;

    }


    public Timer SetDuration(int seconds)
    {
        Duration = remainingDuration = seconds;
        return this;
    }



    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0 )
        {
            UpdateUI(remainingDuration);
            if (!GamePause.Instance.isPause)
            {
                remainingDuration--;

            }
            yield return new WaitForSeconds(1f);
        }
        End();
    }

    private void UpdateUI(int seconds)
    {
        uiTime.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    public void End()
    {
        SoundCtrl.Instance.AudioLose();
        ResetTimer();
        Debug.Log("GAMEOVER");
        loseWindow.SetActive(true);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
