using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    class point
    {
       public int x { set; get; }
       public int y { set; get; }
    }
    
    class Maze
    {
        public int[,] mat;
        public int row, column;
        private int[,,] moves;
        private point cheese;
        Random rnd;

        public void firstInitial(string str)
        {
            rnd = new Random();
           
            cheese = new point();
            readFile(str);
            moves = new int[row, column, 4];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    for (int k = 0; k < 4; k++)
                        moves[i, j, k] = 25;    
        }
        private void readFile(string _str)//fill mat and row and column
        {
            StreamReader sr = new StreamReader(_str);
            string str = sr.ReadLine();
            int size = str.Count();
            row = 0;
            int k;
            for (k = 0; k < size && str[k] != ' '; k++)
            {
                row *= 10;
                row += str[k] - '0';
            }
            column = 0;
            k++;
            for (; k < size && str[k] != ' '; k++)
            {
                column *= 10;
                column += str[k] - '0';
            }
            
            mat = new int[row, column];
            for (int i = 0; i < row; i++)
            {
                k = 0;
                str = sr.ReadLine();
                for (int j = 0; j < column; j++, k += 2)
                {
                   
                        mat[i, j] = str[k] - '0';
                    
                    if (mat[i, j] == 1)
                    {
                        mat[i, j] = -1;
                    }
                    else if (mat[i, j] == 2)
                    {
                        cheese.x = i;
                        cheese.y = j;
                        mat[i, j] = 100;
                    }
                }
            }
        }
        private bool isBlock(point p)
        {
            if (p.x < 0 || p.x >= row)
                return true;
            if (p.y < 0 || p.y >= column)
                return true;
            if (mat[p.x, p.y] == -1)
                return true;
            return false;
        }
        public bool isEqual(point a,point b)
        {
            return (a.x == b.x && a.y == b.y);
        }
        public void findPath()
        {
            point cur = randStart();
            int dir;
            point next;
            while(!isEqual(cur,cheese))
            {
                 dir = direction(cur);
                switch(dir)
                {
                    case 0://up
                       next= moveUp(cur);
                        break;
                    case 1:
                        next = moveDown(cur);
                        break;
                    case 2:
                        next = moveLeft(cur);
                        break;
                    default:
                        next = moveRight(cur);
                        break;
                }
                if (isBlock(next))
                {
                    blockDir(cur, dir);
                    continue;
                }
                int newVal = (int)((0.9)* mat[next.x, next.y]);
                if (newVal > mat[cur.x, cur.y])
                    addDir(cur, dir);
                mat[cur.x, cur.y] = newVal;
                cur = next;
            }
        }
        private int  direction(point p)
        {
            int x = rnd.Next(1, 101);
            int sum = 0;
            for(int i=0;i<4;i++)
            {
                sum += moves[p.x, p.y, i];
                if (sum > x)
                    return i;
            }
            return 3;//will not happen
        }
        private void blockDir(point p,int dir)
        {
            int val = moves[p.x, p.y, dir];
            moves[p.x, p.y, dir] = 0;
            int sum = 0;
            if (dir != 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i != dir)
                    {
                        moves[p.x, p.y, i] += val / 3;
                        sum += moves[p.x, p.y, i];
                    }
                }
                moves[p.x, p.y, 3] = 100 - sum;
            }
            else
            {
                moves[p.x, p.y, 0] += val / 3;
                sum += moves[p.x, p.y, 0];
                moves[p.x, p.y, 1] += val / 3;
                sum += moves[p.x, p.y, 1];
                moves[p.x, p.y, 2] = 100 - sum;
            }
        }
        private void addDir(point p,int dir)
        {
            int sum = 0;
            for(int i=0;i<4;i++)
            {
                if(i!=dir&&moves[p.x,p.y,i]!=0)
                {
                    sum++;
                    moves[p.x, p.y, i]--;
                }
            }
            moves[p.x, p.y, dir] += sum;
        }
        private point randStart()
        {
           
            point start = new point();
                do
                {
                    start.x = rnd.Next(0, row);
                    start.y = rnd.Next(0, column);
                }
                while (isBlock(start));
            return start;
        }
        private point moveUp(point _p)
        {
            point p = new point();
            p.x = _p.x - 1;
            p.y = _p.y;
            return p;
        }
        private point moveDown(point _p)
        {
            point p = new point();
            p.x = _p.x + 1;
            p.y = _p.y;
            return p;
        }
        private point moveLeft(point _p)
        {
            point p = new point();
            p.x = _p.x ;
            p.y = _p.y- 1;
            return p;
        }
        private point moveRight(point _p)
        {
            point p = new point();
            p.x = _p.x ;
            p.y = _p.y+ 1;
            return p;
        }

    }
}
