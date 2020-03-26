﻿using System.Collections;
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

public class SleepEvent : Event
{
    public SleepEvent(Player player) : base(player) {}

    protected override bool hasPrereqs()
    {
        return DayNightController.CanSkipNighttime();
    }

    protected override void doEvent() {
        DayNightController.SkipNighttime(this);
    }

}
