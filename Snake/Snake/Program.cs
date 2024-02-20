using System;
using System.Collections.Generic;
using static System.Console;

namespace SnakeGame
{
    class Program
    {
        enum Direction { Up, Down, Left, Right };
        static void DrawBorder()
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

        static void Main(string[] args)
        {
            WindowHeight = 16;
            WindowWidth = 32; ;
            Random randomNumber = new Random();
            int score = 5;
            bool gameOver = false;
            Pixel head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            head.XPos = WindowWidth / 2;
            head.YPos = WindowHeight / 2;
            head.ScreenColor = ConsoleColor.Red;
            Direction movement = Direction.Right;
            List<int> xPosBody = new List<int>();
            List<int> yPosBody = new List<int>();
            int berryX = randomNumber.Next(0, WindowWidth);
            int berryY = randomNumber.Next(0, WindowHeight);
            DateTime time = DateTime.Now;
            DateTime time2 = DateTime.Now;
            bool buttonPressed = false;
            while (true)
            {
                Clear();
                if (head.XPos == WindowWidth - 1 || head.XPos == 0 || head.YPos == WindowHeight - 1 || head.YPos == 0)
                {
                    gameOver = true;
                }
                DrawBorder();

                ForegroundColor = ConsoleColor.Green;
                if (berryX == head.XPos && berryY == head.YPos)
                {
                    score++;
                    berryX = randomNumber.Next(1, WindowWidth - 2);
                    berryY = randomNumber.Next(1, WindowHeight - 2);
                }
                for (int i = 0; i < xPosBody.Count; i++)
                {
                    SetCursorPosition(xPosBody[i], yPosBody[i]);
                    Write("■");
                    if (xPosBody[i] == head.XPos && yPosBody[i] == head.YPos)
                    {
                        gameOver = true;
                    }
                }
                if (gameOver == true)
                {
                    break;
                }
                SetCursorPosition(head.XPos, head.YPos);
                ForegroundColor = head.ScreenColor;
                Write("■");
                SetCursorPosition(berryX, berryY);
                ForegroundColor = ConsoleColor.Cyan;
                Write("■");
                time = DateTime.Now;
                buttonPressed = false;
                while (true)
                {
                    time2 = DateTime.Now;
                    if (time2.Subtract(time).TotalMilliseconds > 500) { break; }
                    if (KeyAvailable)
                    {
                        ConsoleKeyInfo key = ReadKey(true);
                        if (!buttonPressed)
                        {
                            if (key.Key.Equals(ConsoleKey.UpArrow) && movement != Direction.Down)
                            {
                                movement = Direction.Up;
                            }
                            if (key.Key.Equals(ConsoleKey.DownArrow) && movement != Direction.Up)
                            {
                                movement = Direction.Down;
                            }
                            if (key.Key.Equals(ConsoleKey.LeftArrow) && movement != Direction.Right)
                            {
                                movement = Direction.Left;
                            }
                            if (key.Key.Equals(ConsoleKey.RightArrow) && movement != Direction.Left)
                            {
                                movement = Direction.Right;
                            }
                            buttonPressed = true;
                        }
                    }
                }
                xPosBody.Add(head.XPos);
                yPosBody.Add(head.YPos);
                switch (movement)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                }
                if (xPosBody.Count > score)
                {
                    xPosBody.RemoveAt(0);
                    yPosBody.RemoveAt(0);
                }
            }
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine("Game over, Score: " + score);
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
        }
        class Pixel
        {
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }

            public Pixel(int xPos, int yPos, ConsoleColor color)
            {
                XPos = xPos;
                YPos = yPos;
                ScreenColor = color;
            }
        }
    }
}