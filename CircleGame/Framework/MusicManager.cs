using CircleGame.Framework.Utilities;
using FRESHMusicPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircleGame.Framework
{
    /// <summary>
    /// An audio player with accurate timing for gameplay.
    /// </summary>
    public class MusicManager
    {
        public double SongTime { get; private set; }
        public TimingPoint[] TimingPoints { get; private set; }
        public TimingPoint CurrentTimingPoint { get => TimingPoints[CurrentTimingPointIndex]; }
        public int CurrentTimingPointIndex { get; private set; }
        public Player Player { get; set; }

        private double lastReportedPlayheadPosition;
        private double previousFrameTime;

        private const int timingPrecision = 100; 
        public MusicManager(TimingPoint[] timingPoints)
        {
            TimingPoints = timingPoints;
            CurrentTimingPointIndex = 0;
            Player = new Player { CurrentVolume = 0.4f };
        }
        public void Update(GameTime gameTime)
        {
            SongTime += gameTime.ElapsedGameTime.TotalMilliseconds - previousFrameTime;
            previousFrameTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Player.CurrentBackend?.CurrentTime.TotalMilliseconds % 50 == 0)
            {
                SongTime = (SongTime + Player.CurrentBackend.CurrentTime.TotalMilliseconds) / 2;
                lastReportedPlayheadPosition = Player.CurrentBackend.CurrentTime.TotalMilliseconds;
            }
            if (TimingPoints.Length > 1 && Math.Abs(SongTime - TimingPoints[CurrentTimingPointIndex + 1].Offset) < timingPrecision) CurrentTimingPointIndex++;
        }
        public bool CheckBeat() => (SongTime - CurrentTimingPoint.Offset) % CurrentTimingPoint.BpmOffset < timingPrecision;
        public void PlayMusic(string path)
        {
            Player.AddQueue(path);
            Player.PlayMusic();
            lastReportedPlayheadPosition = Player.CurrentBackend.CurrentTime.TotalMilliseconds;
        }
    }
    public struct TimingPoint
    {
        public float Offset { get; }
        public float Bpm { get; }
        public double BpmOffset { get; }
        public TimingPoint(float bpm, float offset)
        {
            Bpm = bpm;
            Offset = offset;
            BpmOffset = MusicUtils.ToMsOffset(bpm);
        }
    }
}
