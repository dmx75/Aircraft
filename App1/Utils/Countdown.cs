using Android.OS;

namespace App1.Utils
{
    public delegate void TickEvent(long millisUntilFinished);
    public delegate void FinishEvent();

    public class CountDown : CountDownTimer
    {
        public event TickEvent Tick;
        public event FinishEvent Finish;

        public CountDown(long totaltime, long interval)
            : base(totaltime, interval)
        {
        }

        public override void OnTick(long millisUntilFinished)
        {
            if (Tick != null)
                Tick(millisUntilFinished);
        }

        public override void OnFinish()
        {
            if (Finish != null)
                Finish();
        }
    }
}