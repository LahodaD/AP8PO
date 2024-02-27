using System;
using System.Collections.Generic;
using static System.Console;

namespace SnakeGame
{
    enum Direction { Up, Down, Left, Right };

    class Game
    {
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int Score { get; set; }
        public bool GameOver { get; set; }
        Random RandomNumber { get; set; }        
        Pixel Head { get; set; }
        Direction Movement { get; set; }

        List<int> XPosBody { get; set; }
        List<int> YPosBody { get; set; }

        int BerryX { get; set; }
        int BerryY { get; set; }

        DateTime Time { get; set; }
        DateTime Time2 { get; set; }

        bool ButtonPressed { get; set; }

        public Game()
        {
            WindowHeight = 16;
            WindowWidth = 32;
            Score = 5;
            GameOver = false;
            RandomNumber = new Random();
            Head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            Movement = Direction.Right;
            XPosBody = new List<int>();
            YPosBody = new List<int>();
            BerryX = RandomNumber.Next(0, WindowWidth);
            BerryY = RandomNumber.Next(0, WindowHeight);
            Time = DateTime.Now;
            Time2 = DateTime.Now;
        }


        public void DrawBorder()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                SetCursorPosition(i, 0);
                Write("■");

                SetCursorPosition(i, WindowHeight - 1);
                Write("■");
            }

            for (int i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition(0, i);
                Write("■");

                SetCursorPosition(WindowWidth - 1, i);
                Write("■");
            }
        }

        public void Draw()
        {
            Clear();
            DrawBorder();

            ForegroundColor = Head.BackgroundColor;
            SetCursorPosition(Head.XPos, Head.YPos);
            Write("■");

            ForegroundColor = ConsoleColor.Cyan;
            SetCursorPosition(BerryX, BerryY);
            Write("■");

            foreach (var pixel in GetSnakePixels())
            {
                SetCursorPosition(pixel.XPos, pixel.YPos);
                Write("■");
            }
        }

        public void Start()
        {
            ButtonPressed = false;

            while(true)
            {
                Update();
                Draw();
                CheckCollision();
                Eat();
                HandleInput();

                if (GameOver)
                {
                    break;
                }
            }

            GameOverInfo();
        }

        public void CheckCollision()
        {
            if (Head.XPos == WindowWidth - 1 || Head.XPos == 0 || Head.YPos == WindowHeight - 1 || Head.YPos == 0)
            {
                GameOver = true;
            }

            for (int i = 0; i < XPosBody.Count; i++)
            {
                if (XPosBody[i] == Head.XPos && YPosBody[i] == Head.YPos)
                {
                    GameOver = true;
                }
            }

        }

        public void Eat()
        {
            if (BerryX == Head.XPos && BerryY == Head.YPos)
            {
                Score++;
                BerryX = new Random().Next(1, WindowWidth - 2);
                BerryY = new Random().Next(1, WindowHeight - 2);
            }
        }

        public List<Pixel> GetSnakePixels()
        {
            List<Pixel> snakePixels = new List<Pixel>();

            for (int i = 0; i < XPosBody.Count; i++)
            {
                snakePixels.Add(new Pixel(XPosBody[i], YPosBody[i], ConsoleColor.Green));
            }

            return snakePixels;
        }

        public void HandleInput()
        {
            Time = DateTime.Now;
            ButtonPressed = false;

            while (true)
            {
                Time2 = DateTime.Now;
                if (Time2.Subtract(Time).TotalMilliseconds > 500)
                {
                    break;
                }

                if (KeyAvailable)
                {
                    ConsoleKeyInfo key = ReadKey(true);
                    if (!ButtonPressed)
                    {
                        if (key.Key.Equals(ConsoleKey.UpArrow) && Movement != Direction.Down)
                        {
                            Movement = Direction.Up;
                        }
                        if (key.Key.Equals(ConsoleKey.DownArrow) && Movement != Direction.Up)
                        {
                            Movement = Direction.Down;
                        }
                        if (key.Key.Equals(ConsoleKey.LeftArrow) && Movement != Direction.Right)
                        {
                            Movement = Direction.Left;
                        }
                        if (key.Key.Equals(ConsoleKey.RightArrow) && Movement != Direction.Left)
                        {
                            Movement = Direction.Right;
                        }
                        
                        ButtonPressed = true;
                    }
                }
            }
        }

        public void Update()
        {
            XPosBody.Add(Head.XPos);
            YPosBody.Add(Head.YPos);

            switch (Movement)
            {
                case Direction.Up:
                    Head.YPos--;
                    break;
                case Direction.Down:
                    Head.YPos++;
                    break;
                case Direction.Left:
                    Head.XPos--;
                    break;
                case Direction.Right:
                    Head.XPos++;
                    break;
            }

            if (XPosBody.Count > Score)
            {
                XPosBody.RemoveAt(0);
                YPosBody.RemoveAt(0);
            }
        }

        private void GameOverInfo()
        {            
            Clear();
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine("Game over, Score: " + Score);
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
        }

    }

    class Pixel
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public Pixel(int xPos, int yPos, ConsoleColor backgroundColor)
        {
            XPos = xPos;
            YPos = yPos;
            BackgroundColor = backgroundColor;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
