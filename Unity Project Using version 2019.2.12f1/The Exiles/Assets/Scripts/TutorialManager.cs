using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject exit;
    private Win winScript;

    public Canvas canvas;
    public bool displayHelp = false;

    private RawImage tutorialImage;
    private bool animalGateOpened = false;

    void Start()
    {
        tutorialImage = canvas.GetComponentInChildren<RawImage>();
        winScript = exit.GetComponent<Win>();

        // Ignore enemy collision with invisible barriers at layer 15 (PlayerInvisibleBarrier).
        // Only the player and animals can collide with the PlayerInvisibleBarrier layer.
        Physics.IgnoreLayerCollision(10, 15);
    }

    void Update()
    {
        if (displayHelp && tutorialImage != null && !tutorialImage.enabled)
        {
            tutorialImage.enabled = true;
        }
        else if (!displayHelp && tutorialImage != null && tutorialImage.enabled)
        {
            tutorialImage.enabled = false;
        }
    }

    /// <summary>
    /// Sets the texture for the tutorial help image.
    /// </summary>
    /// <param name="texture">A texture asset to be used in the tutorial image.</param>
    /// <param name="setNativeSize">A boolean indicating whether the image should be its native size.</param>
    /// <param name="scale">A Vector3 for the scale of the texture.</param>
    public void setTexture(Texture texture, bool setNativeSize, Vector3 scale)
    {
        tutorialImage.texture = texture;
        tutorialImage.rectTransform.localScale = scale;
        if (setNativeSize)
        {
            tutorialImage.SetNativeSize();
        }
    }
}
