using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class Affint {
    private const int PULSE_HIGH = 90;
    private const int PULSE_LOW = 70;

    private const int KILL_SPEED_HIGH = 3;
    private const int KILL_SPEED_LOW = 2;

    private const int INTERVAL = 15;

    private const int HISTORY_LENGTH = 10;
    
    public static float ZombieSpeedFactor = 1.0f;
    public static int EmotionalStatePoints = 0;
    private static int Pulse = 80;

    private static int zombieKilled = Oncollision.zombieKilled;
    private static int zombieKilledInInterval = 0;
    private static bool EmotionalStateChanged = false;

    private static Timer timer = new Timer();


    public static void Start() {
        timer.Interval = INTERVAL * 1000;
        timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        zombieKilled = Oncollision.zombieKilled;
        timer.Start();
    }

    public static void Pause() {
        timer.Stop();
    }

    public static void Stop() {
        timer.Stop();
        ZombieSpeedFactor = 1.0f;
        EmotionalStatePoints = 0;
        Pulse = 80;

        zombieKilled = Oncollision.zombieKilled;
        zombieKilledInInterval = 0;
        EmotionalStateChanged = false;
}

    private static void TimerElapsed(object sender, ElapsedEventArgs e) {
        Debug.Log("Timer callback started");
        zombieKilledInInterval = Oncollision.zombieKilled - zombieKilled;
        Debug.Log("Killed " + zombieKilledInInterval + " zombies in last 5 seconds");

        if (zombieKilledInInterval > KILL_SPEED_HIGH) {
            EmotionalStatePoints++;
            EmotionalStateChanged = true;
        } else if (zombieKilledInInterval < KILL_SPEED_LOW) {
            EmotionalStatePoints--;
            EmotionalStateChanged = true;
        }

        if (EmotionalStateChanged) {
            //TODO Implement pulse read
            if (EmotionalStatePoints > 0 &&
                 Pulse > PULSE_LOW && Pulse < PULSE_HIGH) {
                EmotionalStatePoints++;
            } else if (EmotionalStatePoints < 0 &&
                Pulse > PULSE_HIGH) {
                EmotionalStatePoints--;
            }
        }
        Debug.Log("ESP: " + EmotionalStatePoints);

        ZombieSpeedFactor = 1f + ((float)EmotionalStatePoints / 10f);
        if (ZombieSpeedFactor < 0.1) {
            ZombieSpeedFactor = 0.1f;
        }

        Debug.Log("ZSF: " + ZombieSpeedFactor);

        zombieKilled = Oncollision.zombieKilled;
    }

}
