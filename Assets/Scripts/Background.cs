using System.Collections;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] dayBackground;
    public Sprite[] nightBackground;
    private SpriteRenderer renderer;
    public int timeOfDay = 1;

    private Coroutine animationCoroutine;
    private int lastTimeOfDay;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        lastTimeOfDay = timeOfDay;
        animationCoroutine = StartCoroutine(AnimateBackground());
    }

    void Update()
    {
        if (timeOfDay != lastTimeOfDay)
        {
            lastTimeOfDay = timeOfDay;

            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            animationCoroutine = StartCoroutine(AnimateBackground());
        }
    }

    public void RerollTime()
    {
        timeOfDay = Random.Range(1, 3);
    }

    IEnumerator AnimateBackground()
    {
        Sprite[] currentBackground = timeOfDay == 1 ? dayBackground : nightBackground;
        int index = 0;

        while (true)
        {
            if (timeOfDay != lastTimeOfDay)
                yield break;

            renderer.sprite = currentBackground[index];
            index = (index + 1) % currentBackground.Length;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
