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
            Obstacle,
            Empty
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
        public Tile[] ArrTiles = new Tile[4];  //Up, Down, Left, Right

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

   /* public class Goblin : Enemy
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
            while (Check == false)
            { 
                int iCheck = rnd.Next(1, 5); //Up, Down, Left, Right 
                switch (iCheck)
                {
                    case 1:
                        if ()ArrTiles[1] = "Empty";
                        break;
                }
                    
            }
        }

        public override string ToString()
        {  
            return "Goblin " + base.ToString();
        }
    }*/

    public class map
    {

    }



}

