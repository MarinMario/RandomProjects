using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

namespace PoolGame
{
    class GameScene : IScene
    {
        Rectangle table = new Rectangle(100, 100, 800, 400);
        List<Ball> balls = new List<Ball>();
        Vector2 clickPos = Vector2.Zero;

        public GameScene()
        {
            balls.Add(new Ball(Vector2.One * 200, Vector2.One * 100));
            balls.Add(new Ball(Vector2.One * 300, Vector2.One * 100));
            balls.Add(new Ball(Vector2.One * 400, Vector2.One * 100));
            balls.Add(new Ball(Vector2.One * 500, Vector2.One * 100));
            balls.Add(new Ball(Vector2.One * 600, Vector2.One * 100));
        }

        public void Update()
        {
            var delta = Raylib.GetFrameTime();

            var m = Raylib.GetMousePosition();
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                clickPos = Raylib.GetMousePosition();
            foreach (var ball in balls)
            {
                if (Vector2.Distance(m, ball.position) < ball.radius && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                    ball.position = m;
                if (Vector2.Distance(m, ball.position + ball.velocity) < 10 && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                    ball.velocity = m - ball.position;
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                clickPos = m;

            if (clickPos != Vector2.Zero)
            {
                balls[0].velocity = clickPos - balls[0].position;
                clickPos = Vector2.Zero;
            }

            foreach (var ball in balls)
            {
                ball.position += ball.velocity * delta;
                ball.velocity -= ball.velocity * delta;
            }

            BallCollision();
        }
        public void Draw()
        {
            foreach (var ball in balls)
            {
                //Raylib.DrawRectangleLinesEx(table, 3, Color.BLACK);
                Raylib.DrawCircleV(ball.position, ball.radius, Color.RED);
                Raylib.DrawLineEx(ball.position, ball.position + ball.velocity, 3, Color.GREEN);
                Raylib.DrawCircleV(ball.position + ball.velocity, 10, Color.GREEN);

            }
            DrawDebugLines(clickPos);
        }

        void DrawDebugLines(Vector2 vel)
        {
            foreach (var ball in balls)
            {
                Raylib.DrawLineEx(ball.position, ball.position + ball.velocity, 3, Color.GREEN);
                foreach (var b2 in balls)
                {
                    if (Vector2.Distance(ball.position, b2.position) <= (ball.radius + b2.radius) * 2 && ball != b2)
                    {
                        Raylib.DrawLineEx(ball.position, b2.position, 3, Color.BLUE);
                        var center = (ball.position + b2.position) / 2;
                        //Raylib.DrawLineEx(center, center + new Vector2(0, 200), 3, Color.BLUE);
                        //Raylib.DrawLineEx(center, center + new Vector2(0, -200), 3, Color.BLUE);
                    }

                }
            }
        }

        void BallCollision()
        {
            var collisions = new List<(int index, Ball other)>();
            for (var b1 = 0; b1 < balls.Count; b1++)
                for (var b2 = 0; b2 < balls.Count; b2++)
                    if (b1 != b2 && Vector2.Distance(balls[b1].position, balls[b2].position) <= balls[b1].radius + balls[b2].radius)
                    {
                        collisions.Add((b1, new Ball(balls[b2].position, balls[b2].velocity, balls[b2].radius)));
                    }

            foreach (var c in collisions)
            {
                var dir = (balls[c.index].position - c.other.position).SafeNormalize() * 100;
                balls[c.index].velocity += dir;
            }
        }
    }

    class Ball
    {
        public Vector2 position;
        public Vector2 velocity;
        public float radius;

        public Ball(Vector2 position, Vector2 velocity, float radius = 40)
        {
            this.position = position;
            this.velocity = velocity;
            this.radius = radius;
        }
    }
}
