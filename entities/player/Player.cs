using System;
using Godot;

public sealed class Player : Character
{
    [Signal] public delegate void PlayerDied();

    [Signal]
    public delegate void ScoreAdded(int amount);

    [Signal]
    public delegate void DamagePrepared(int amount);

    protected override Vector2 Forward => Vector2.Down;

    protected override void OnDeath()
    {
        EmitSignal(nameof(PlayerDied));
    }

    public void ShowPunchButton()
    {
        GetNode<CanvasItem>("PunchButton").Visible = true;
    }

    public void HidePunchButton()
    {
        GetNode<CanvasItem>("PunchButton").Visible = false;
    }

    public void StartWalking()
    {
        GetNode<AnimatedSprite>("Offset/AnimatedSprite").Play("default");
    }

    public void StopWalking()
    {
        var sprite = GetNode<AnimatedSprite>("Offset/AnimatedSprite");
        sprite.Stop();
        sprite.Frame = 0;
    }

    public override void DoAction(Action @do, Action complete)
    {
        var sprite = GetNode<AnimatedSprite>("Offset/AnimatedSprite");
        base.DoAction(@do, resetAnimationAndComplete);
        sprite.Frame = 1;
        return;

        void resetAnimationAndComplete()
        {
            sprite.Frame = 0;
            complete();
        }
    }

    public void AddScore(int amount)
    {
        EmitSignal(nameof(ScoreAdded), amount);
    }

    public void PreviewDamage(int amount)
    {
        EmitSignal(nameof(DamagePrepared), amount);
    }
}
