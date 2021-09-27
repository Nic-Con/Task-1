using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADEGoblinGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public abstract class Tile
    {
        protected int X
        {
            get { return X; }
            set { }
        }
        protected int Y
        {
            get { return Y; }
            set { }
        }
        public enum Type
        {
            Hero,
            Enemy,
            Gold,
            Weapon,
            Empty,
            Obstacle
        }
        
        public int getX()
        {
            return X;
        }

        public int getY()
        {
            return Y;
        }

        public Tile(int NewX, int NewY, Type type)
        {
            X = NewX;
            Y = NewY;
        }


    }

    class Obstacle : Tile
    {
        public Obstacle(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
    }

    public class EmptyTile : Tile
    {
        public EmptyTile(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
    }

    public abstract class Character : Tile
    {
        protected int MaxHP
        {
            get { return MaxHP; }
            set { }
        }
        protected int HP
        {
            get { return HP; }
            set { }
        }
        protected int Damage
        {
            get { return Damage; }
            set { }
        }
        public Tile[] ArrVision = new Tile[4];  //Up, Down, Left, Right

        public enum Movement
        {
            None,
            Up,
            Down,
            Left,
            Right
        }
        public Character(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
        public virtual void Attack(Character Target)
        {
            Target.HP = Target.HP - Damage;
        }
        public bool IsDead()
        {
            if (HP <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int DistanceTo(Character Target)
        {
            double calculate;
            double XCalc;
            double YCalc;
            if (Target.X > X)
            {
                XCalc = (Target.X - X);
                XCalc = XCalc * XCalc;
            }
            else
            {
                XCalc = (X - Target.X);
                XCalc = XCalc * XCalc;
            }
            if (Target.Y > Y)
            {
                YCalc = (Target.Y - Y);
                YCalc = YCalc * YCalc;
            }
            else
            {
                YCalc = (Y - Target.Y);
                YCalc = YCalc * YCalc;
            }
            calculate = Math.Sqrt(XCalc + YCalc);
            return Convert.ToInt32(Math.Ceiling(calculate));
        }
        public virtual bool CheckRange(Character Target)
        {
            if (DistanceTo(Target) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Move(Movement move)
        {
            switch(move)
            {
                case Movement.Up:
                    Y = Y + 1;
                    break;
                case Movement.Down:
                    Y = Y - 1;
                    break;
                case Movement.Left:
                    X = X - 1;
                    break;
                case Movement.Right:
                    X = X + 1;
                    break;
            }
        }

        public abstract Movement ReturnMove(Movement move);
        public abstract override string ToString();
    }
    public abstract class Enemy : Character
    {
        protected Random rnd = new Random();
        public Enemy(int NewX, int NewY, Type type, int NewMaxHP, int NewDamage) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
            HP = NewMaxHP;
            MaxHP = NewMaxHP;
            Damage = NewDamage;
        }
        public override string ToString()
        {
            return " [" + this.X + "," + this.Y + "] (" + this.Damage + ")";
        }
    }

    public class Goblin : Enemy
    {
        public Goblin(int NewX, int NewY, Type type, int NewMaxHP, int NewDamage) : base(NewX, NewY, type, NewMaxHP, NewDamage)
        {
            X = NewX;
            Y = NewY;
            HP = 10;
            MaxHP = 10;
            Damage = 1;
        }

        public override Movement ReturnMove(Movement move)
        {
            bool Check = false;
            Movement temp = Movement.None;
            while (Check == false)
            { 
                int iCheck = rnd.Next(1, 5); //Up, Down, Left, Right 
                switch (iCheck)
                {
                    case 1:
                        if (ArrVision[1] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Up;                           
                        }
                        break;
                    case 2:
                        if (ArrVision[1] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Down;
                        }
                        break;
                    case 3:
                        if (ArrVision[1] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Left;
                        }
                        break;
                    case 4:
                        if (ArrVision[1] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Right;
                        }
                        break;
                }
                    
            }

            return temp;
        }

        public override string ToString()
        {  
            return "Goblin " + base.ToString();
        }
    }

    public class Hero : Character
    {
        public Hero(int NewX, int NewY, Type type, int NewMaxHP) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
            HP = NewMaxHP;
            MaxHP = NewMaxHP;
            Damage = 2;
        }

        public override Movement ReturnMove(Movement move)
        {
            Movement temp = move;
            switch (move)
            {
                case Movement.Up:
                    if (ArrVision[1] is EmptyTile)
                    {
                        temp = Movement.Up;
                    }
                    else
                    {
                        temp = Movement.None;
                    }
                    break;
                case Movement.Down:
                    if (ArrVision[1] is EmptyTile)
                    {
                        temp = Movement.Down;
                    }
                    else
                    {
                        temp = Movement.None;
                    }
                    break;
                case Movement.Left:
                    if (ArrVision[1] is EmptyTile)
                    {
                        temp = Movement.Left;
                    }
                    else
                    {
                        temp = Movement.None;
                    }
                    break;
                case Movement.Right:
                    if (ArrVision[1] is EmptyTile)
                    {
                        temp = Movement.Right;
                    }
                    else
                    {
                        temp = Movement.None;
                    }
                    break;
            }
            return temp;
        }

        public override string ToString()
        {
            string s = "Player Stats: \n";
            s = s + "HP: " + HP + "/" + MaxHP + "\n";
            s = s + "Damage: " + Damage + "\n";
            s = s + "[" + X + "," + Y + "]";
            return s;
        }
    }

    public class Map
    {
        private Tile[,] ArrMap;
        Hero hero = null;
        private Enemy[] ArrEnemy;
        private int height;
        private int width;
        Random rnd = new Random();

        public Map(int minW, int minH, int maxH, int maxW, int numEnemy)
        {
            height = rnd.Next(minH, maxH + 1);
            width = rnd.Next(minW, maxW + 1);
            ArrMap = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int a = 0; a < height; a++)
                {
                    ArrMap[i,a] = new EmptyTile(i, a, Tile.Type.Empty);
                }
            }
            for (int i = 0; i < width; i++)
            {
                ArrMap[i,0] = new Obstacle(i, 0, Tile.Type.Obstacle);
                ArrMap[i, height] = new Obstacle(i, height, Tile.Type.Obstacle);
            }
            for (int i = 0; i < height; i++)
            {
                ArrMap[0, i] = new Obstacle(0, i, Tile.Type.Obstacle);
                ArrMap[width, i] = new Obstacle(width, i, Tile.Type.Obstacle);
            }
            Create(Tile.Type.Hero);
            ArrMap[hero.getX(), hero.getY()] = hero;
            ArrEnemy = new Enemy[numEnemy];
            for (int i = 0; i < numEnemy; i++)
            {
                ArrEnemy[i] = (Enemy)Create(Tile.Type.Enemy);
                ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY()] = ArrEnemy[i];
            }
        }
        public void UpdateVision()
        {
            for (int i = 0; i < ArrEnemy.Length; i++)
            {
                for (int a = 0; a < 4; a++) //Up, Down, Left, Right
                {
                    switch (a)
                    {
                        case 0:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY() + 1];
                        break;
                        case 1:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY() - 1];
                        break;
                        case 2:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX() - 1, ArrEnemy[i].getY()];
                        break;
                        case 3:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX() + 1, ArrEnemy[i].getY()];
                        break;
                    }          
                }
                
            }
            for (int a = 0; a < 4; a++) //Up, Down, Left, Right
            {
                switch (a)
                {
                    case 0:
                        hero.ArrVision[a] = ArrMap[hero.getX(), hero.getY() + 1];
                        break;
                    case 1:
                        hero.ArrVision[a] = ArrMap[hero.getX(), hero.getY() - 1];
                        break;
                    case 2:
                        hero.ArrVision[a] = ArrMap[hero.getX() - 1, hero.getY()];
                        break;
                    case 3:
                        hero.ArrVision[a] = ArrMap[hero.getX() + 1, hero.getY()];
                        break;
                }
            }
        }
        private Tile Create(Tile.Type type)
        {
            int x = 0;
            int y = 0;
            Tile temp = null;
            while (!(ArrMap[x, y] is EmptyTile))
            {
                x = rnd.Next(1 + width);
                y = rnd.Next(1 + height);
            }
            switch (type)
            {
                case Tile.Type.Hero:
                    hero = new Hero(x, y, 0, 20);
                    temp = hero;
                    break;
                case Tile.Type.Enemy:
                    Goblin goblin = new Goblin(x, y, 0, 10, 1);
                    temp = goblin;
                    break;
            }
            return temp;
        }
    }
    public class GameEngine
    {
        public GameEngine(int minW, int minH, int maxH, int maxW, int numEnemy)
        {
            Map.Map(minW,minH,maxH,maxW,numEnemy);
        }
    }
}

