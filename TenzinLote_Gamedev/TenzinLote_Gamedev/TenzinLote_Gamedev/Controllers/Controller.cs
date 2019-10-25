using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenzinLote_Gamedev
{
    public abstract class Controller
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Space { get; set; }

        abstract public void Update();
    }

    public class ControllerArrows : Controller
    {
        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.Left))
            {
                Left = true;
            }
            if (stateKey.IsKeyUp(Keys.Left))
            {
                Left = false;
            }

            if (stateKey.IsKeyDown(Keys.Right))
            {
                Right = true;
            }
            if (stateKey.IsKeyUp(Keys.Right))
            {
                Right = false;
            }
            if (stateKey.IsKeyDown(Keys.Up))
            {
                Up = true;
            }
            if (stateKey.IsKeyUp(Keys.Up))
            {
                Up = false;
            }
            if (stateKey.IsKeyDown(Keys.Down))
            {
                Down = true;
            }
            if (stateKey.IsKeyUp(Keys.Down))
            {
                Down = false;
            }
            if (stateKey.IsKeyDown(Keys.Space))
            {
                Space = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                Space = false;
            }
        }
    }
    public class ControllerKeyb : Controller
    {
        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.Q))
            {
                Left = true;
            }
            if (stateKey.IsKeyUp(Keys.Q))
            {
                Left = false;
            }

            if (stateKey.IsKeyDown(Keys.D))
            {
                Right = true;
            }
            if (stateKey.IsKeyUp(Keys.D))
            {
                Right = false;
            }
            if (stateKey.IsKeyDown(Keys.Z))
            {
                Up = true;
            }
            if (stateKey.IsKeyUp(Keys.Z))
            {
                Up = false;
            }
            if (stateKey.IsKeyDown(Keys.W))
            {
                Down = true;
            }
            if (stateKey.IsKeyUp(Keys.W))
            {
                Down = false;
            }
            if (stateKey.IsKeyDown(Keys.Space))
            {
                Space = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                Space = false;
            }
        }
    }
    public class ControllerKeybNumbers : Controller
    {
        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.F1))
            {
                Left = true;
            }
            if (stateKey.IsKeyUp(Keys.F1))
            {
                Left = false;
            }

            if (stateKey.IsKeyDown(Keys.F2))
            {
                Right = true;
            }
            if (stateKey.IsKeyUp(Keys.F2))
            {
                Right = false;
            }
            if (stateKey.IsKeyDown(Keys.F3))
            {
                Up = true;
            }
            if (stateKey.IsKeyUp(Keys.F3))
            {
                Up = false;
            }
            if (stateKey.IsKeyDown(Keys.F4))
            {
                Down = true;
            }
            if (stateKey.IsKeyUp(Keys.F4))
            {
                Down = false;
            }
            if (stateKey.IsKeyDown(Keys.Space))
            {
                Space = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                Space = false;
            }
        }
    }
}
