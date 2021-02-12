using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUI : MonoBehaviour
{
    public List<RectTransform> levelsImages;
    public Button nextButton;
    public Button prevButton;

    public float slideTime = 0.3f;
    public float moveValue = 1800.0f;

    private Vector3 velocityForSmoothDamp = Vector3.zero;

    private void Start()
    {
        nextButton.onClick.AddListener(MoveLevelLeft);
    }

    private void Update()
    {
        
    }

    private void MoveLevelLeft()
    {
        StartCoroutine(SmoothMoveLeft());
    }

    IEnumerator SmoothMoveLeft()
    {
        List<Vector3> targetPositions = new List<Vector3>();

        for(int i = 0; i < levelsImages.Count; i++)
        {
            targetPositions.Add(levelsImages[i].localPosition - new Vector3(moveValue, 0f, 0f));
        }

        while(true)
        {
            Debug.Log("Trying to slide");
            for(int i = 0; i < levelsImages.Count; i++)
            {
                Vector3 localPositionOfImage = levelsImages[i].localPosition;

                if (localPositionOfImage.x >= targetPositions[i].x)
                {
                    levelsImages[i].localPosition = Vector3.SmoothDamp(localPositionOfImage, targetPositions[i], ref velocityForSmoothDamp, slideTime);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
