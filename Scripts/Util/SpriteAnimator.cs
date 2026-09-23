using System.Collections.Generic;
using UnityEngine;

// Cycles a SpriteRenderer through a list of frames at a fixed rate
public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameRate;

    int currentFrame;
    float timer;

    public List<Sprite> Frames => frames;

    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameRate = 0.16f)
    {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.frameRate = frameRate;
    }

    // Starts on frame 1 so a step begins mid-stride instead of on the standing frame
    public void Start()
    {
        currentFrame = 1;
        timer = 0f;
        spriteRenderer.sprite = frames[1];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer > frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Count;
            spriteRenderer.sprite = frames[currentFrame];
            timer -= frameRate;
        }
    }
}
