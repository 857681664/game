using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class CalMinimumStep
    {
        class Point
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Point Last { get; set; }

        }
        public static int MiniMumStep(int[,] b, int sR, int sC, int eR, int eC)
        {
            int i, j;
            int result = 0;
            var rowNum = b.GetLength(0);
            var columnNum = b.GetLength(1);
            sR++;
            sC++;
            eR++;
            eC++;
            int[,] a = new int[rowNum + 2, columnNum + 2];
            for (i = 1; i < rowNum + 1; ++i)
            {
                for (j = 1; j < columnNum + 1; ++j)
                {
                    a[i, j] = b[i - 1, j - 1];
                }
            }
            for (i = 0; i < columnNum + 2; ++i)
            {//加墙  
                a[0, i] = 1;
                a[rowNum + 1, i] = 1;
            }
            for (i = 1; i < rowNum + 1; ++i)
            {//加墙  
                a[i, 0] = 1;
                a[i, columnNum + 1] = 1;
            }

            Queue<Point> q = new Queue<Point>();
            Point start = new Point
            {
                X = sR,
                Y = sC
            }; //起点
            start.Last = start;
            q.Enqueue(start);
            a[start.X, start.Y] = 2;

            Point end = new Point
            {
                X = eR,
                Y = eC
            };

            int[,] aspect = { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 } };//转向：上下左右  
            int flag = 0;//是否有路可走的标志  
            while (q.Count() != 0)
            {
                Point front = q.Peek();
                q.Dequeue();//弹出队头  
                if (front.X == end.X && front.Y == end.Y)
                {
                    flag = 1;
                    result = a[front.X, front.Y] - 2;
                    a[front.X, front.Y] = -6;
                    Point lastPoint = front;
                    front = front.Last;
                    while ((front.X != start.X) || (front.Y != start.Y))
                    {
                        if (lastPoint.X - front.X == 1)
                        {
                            a[front.X, front.Y] = -1;
                        }
                        else if (lastPoint.X - front.X == -1)
                        {
                            a[front.X, front.Y] = -2;
                        }
                        else if (lastPoint.Y - front.Y == 1)
                        {
                            a[front.X, front.Y] = -3;
                        }
                        else
                        {
                            a[front.X, front.Y] = -4;
                        }
                        lastPoint = front;
                        front = front.Last;
                    }
                    a[start.X, start.Y] = -5;
                    break;

                }
                for (i = 0; i < 4; ++i)
                {
                    Point temp = new Point();
                    temp.X = front.X + aspect[i, 0];
                    temp.Y = front.Y + aspect[i, 1];
                    if (a[temp.X, temp.Y] == 0)
                    {
                        temp.Last = front;
                        q.Enqueue(temp);
                        a[temp.X, temp.Y] = a[front.X, front.Y] + 1;
                    }
                }
            }
            if (flag == 0)
                result = 0;
            return result;
        }
    }
}