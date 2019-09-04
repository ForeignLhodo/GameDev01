using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenzin_Lote_Game
{
    class Animation
    {
        private List<AnimationFrame> frames;
        public AnimationFrame CurrentFrame { get; set; }
        public int AmountOfMovePerSec { get; set; }

        private int counter = 0;

        private double x = 0;
        public double Offset { get; set; }

        private int totalwidth = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
            AmountOfMovePerSec = 8;
        }
        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
            Offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in frames)
                totalwidth += f.SourceRectangle.Width;
        }


        public void Update(GameTime gameTime)
        {
            double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            x += temp;
            if (x >= CurrentFrame.SourceRectangle.Width / AmountOfMovePerSec)
            {
                x = 0;
                counter++;
                if (counter >= frames.Count)
                    counter = 0;
                CurrentFrame = frames[counter];
                Offset += CurrentFrame.SourceRectangle.Width;
            }
            if (Offset >= totalwidth)
                Offset = 0;


        }
    }
}
