using AlkalineThunder.Pandemic;
using AlkalineThunder.Pandemic.Audio;
using AlkalineThunder.Pandemic.Gui;
using AlkalineThunder.Pandemic.Gui.Controls;
using AlkalineThunder.Pandemic.Gui.Markup;
using AlkalineThunder.Pandemic.Rendering;
using AlkalineThunder.Pandemic.Scenes;
using AlkalineThunder.Pandemic.Skinning;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using FRESHMusicPlayer;
using FRESHMusicPlayer.Handlers;
using System;
using CircleGame.Framework;
using System.Diagnostics;
using CircleGame.Framework.Utilities;

namespace CircleGame.Scenes
{
    public class HelloWorldScene : Scene
    {
        private FileStream fileStream = new FileStream("C:\\Users\\poohw\\OneDrive\\Pictures\\Wallpaper Material\\6fc252ee2960b9ff92d4840d194c39eb(UpRGB)(noise_scale)(Level0)(tta)(x6.000000).png", FileMode.Open);
        private bool onbeat = false;

        private MusicManager musicManager = new MusicManager(new TimingPoint[] {
        new TimingPoint(125, 1977)});

        protected override void OnLoad()
        {
            musicManager.PlayMusic(@"C:\Users\poohw\AppData\Local\osu!\Songs\1016472 Shiina Yuika - Shiina Yuika o Matteimasu\audio.mp3");
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            musicManager.Update(gameTime);
            onbeat = musicManager.CheckBeat();
            base.OnUpdate(gameTime);
        }
        protected override void OnDraw(GameTime gameTime, SpriteRocket2D renderer)
        {
            //Texture2D weebTexture = Texture2D.FromStream(renderer.GraphicsDevice, fileStream);
            
            var font = Skin.GetFont(SkinFontStyle.Heading1);
            renderer.Begin();
            if (onbeat) renderer.FillRectangle(Gui.BoundingBox, Color.White);
            else renderer.FillRectangle(Gui.BoundingBox, Color.LightGray);
            renderer.DrawString(font, "hello", new Vector2((Gui.BoundingBox.Width - font.MeasureString("hello").X) / 2, (Gui.BoundingBox.Width - font.MeasureString("hello").Y) / 2 - ((float)musicManager.SongTime - 7737)), Color.LimeGreen);
            string stuff = $"{onbeat}: BPM: {musicManager.CurrentTimingPoint.Bpm} Offset: {musicManager.CurrentTimingPoint.Offset} Clock - {musicManager.SongTime} | FMP - {musicManager.Player.CurrentBackend?.CurrentTime.TotalMilliseconds} {(float)musicManager.SongTime - 40137}";
            renderer.DrawString(font, stuff, new Vector2((Gui.BoundingBox.Width - font.MeasureString(stuff).X) / 2, (Gui.BoundingBox.Height - font.MeasureString(stuff).Y) / 2), Color.Black);
            renderer.End();
            base.OnDraw(gameTime, renderer);
        }
    }
}
