using UnityEngine;

public class PingPongMover : MonoBehaviour
{
    [Tooltip("Desplazamiento relativo desde la posición inicial")]
    public Vector3 offset = new Vector3(0, 2, 0);

    [Tooltip("Tiempo total de ida y vuelta (en segundos)")]
    public float totalDuration = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float halfDuration;
    private float timer = 0f;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + offset;
        halfDuration = totalDuration / 2f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = timer % totalDuration;

        if (t <= halfDuration)
        {
            // Ida (0 → 1)
            float progress = t / halfDuration;
            transform.position = Vector3.Lerp(startPos, endPos, progress);
        }
        else
        {
            // Vuelta (1 → 0)
            float progress = (t - halfDuration) / halfDuration;
            transform.position = Vector3.Lerp(endPos, startPos, progress);
        }
    }
}
