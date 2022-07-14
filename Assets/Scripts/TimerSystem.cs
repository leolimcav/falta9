using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TimerSystem : MonoBehaviour
{
  private float REAL_SECONDS_PER_INGAME_DAY = 2100f;
  
  private Transform HourHandTransform;
  private Transform MinuteHandTransform;
  private TextMeshProUGUI TimeText;

  private float day;

  public float startingHourAndMinute;
  public float startingMinutePointer;
  private void Awake()
  {
    HourHandTransform = transform.Find("HourClockHand");
    MinuteHandTransform = transform.Find("MinuteClockHand");
    TimeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();

    MinuteHandTransform.eulerAngles = new Vector3(0, 0, startingMinutePointer);
  }

  private void Update()
  {
    day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

    var hoursPerDay = 24f;
    var dayNormalized = (startingHourAndMinute / hoursPerDay) + (day % 1f);
    var rotationDegreesPerDay = 360f;
    
    HourHandTransform.eulerAngles = new Vector3(0, 0, (-dayNormalized * rotationDegreesPerDay) * 2);
    
    var minutePointerUpdate = startingMinutePointer + (-dayNormalized * rotationDegreesPerDay * hoursPerDay);
    MinuteHandTransform.eulerAngles = new Vector3(0, 0, minutePointerUpdate);

    var hourString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");
    var minutesPerHour = 60f;
    var minuteString = (((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

    TimeText.text = $"{hourString}:{minuteString}";
  }
}