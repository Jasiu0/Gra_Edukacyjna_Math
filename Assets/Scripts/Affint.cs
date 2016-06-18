using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class Affint : MonoBehaviour {
    private const int INTERVAL = 10;
    
    public static float ZombieSpeedFactor = 1.0f;

    private static int zombieKilled = Oncollision.zombieKilled;
    private static int zombieKilledInInterval = 0;

    private static int zombieEnteredInInterval = 0;

    private static int questionsAnsweredInInterval = 0;

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
        questionsAnsweredInInterval = 0;
        zombieEnteredInInterval = 0;

        zombieKilled = Oncollision.zombieKilled;
        zombieKilledInInterval = 0;
    }

    public void QuestionAnswered() {
        questionsAnsweredInInterval++;
    }

    public void ZombieEntered() {
        zombieEnteredInInterval++;
    }

    public void OnDestroy() {
        Stop();
    }

    private static void TimerElapsed(object sender, ElapsedEventArgs e) {
        zombieKilledInInterval = Oncollision.zombieKilled - zombieKilled;

        float performanceFactor = 0;
        if (questionsAnsweredInInterval != 0 && zombieEnteredInInterval != 0) {
            performanceFactor = (float)Math.Pow(zombieKilledInInterval, 2) / (float)zombieEnteredInInterval / (float)questionsAnsweredInInterval;
        }

        if (zombieEnteredInInterval == 0) {
            performanceFactor = 0.5f;
        }

        Debug.Log("Performance factor: " + zombieKilledInInterval + "^2/" + zombieEnteredInInterval + "/" + questionsAnsweredInInterval + " = " + performanceFactor);
        if (performanceFactor < 0.5) {
            decreaseZombieSpeedFactor();
        } else if (performanceFactor > 0.8) {
            increaseZombieSpeedFactor();
        }

        Debug.Log("Zombie speed factor = " + ZombieSpeedFactor);

        zombieKilled = Oncollision.zombieKilled;
        questionsAnsweredInInterval = 0;
        zombieEnteredInInterval = 0;
    }

    private static void increaseZombieSpeedFactor() {
        if (ZombieSpeedFactor < 2.5f) {
            ZombieSpeedFactor += 0.2f;
        }
    }

    private static void decreaseZombieSpeedFactor() {
        if (ZombieSpeedFactor > 0.2f) {
            ZombieSpeedFactor -= 0.2f;
        }
    }

}
