using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event
{
    private Player playerToNotify;

    public Event(Player player)
    {
        this.playerToNotify = player;
    }

    protected abstract bool hasPrereqs();
    public void begin() {
        playerToNotify.eventBegin();
    }
    protected abstract void doEvent();

    public void end()
    {
        playerToNotify.eventEnded();
    }

    public void run()
    {
        if (hasPrereqs())
        {
            begin();
            doEvent();
        }
    }
}

public class SkipTimeEvent : Event
{
    public float minutesToSkip
    { get; set; }
public float initialTime
    { get; set; }
    public SkipTimeEvent(Player player, float seconds) : base(player) {
        minutesToSkip = seconds;
    }

    protected override bool hasPrereqs()
    {
        return DayNightController.CanSkipTime(this.minutesToSkip);
    }

    protected override void doEvent()
    {
        DayNightController.SkipTime(this);
    }

}
