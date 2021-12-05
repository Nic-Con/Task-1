using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADEGoblinGame
{
    public partial class Form1 : Form
    {
        GameEngine GameEng;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GameEng = new GameEngine((int)Math.Truncate(MinWid.Value), (int)Math.Truncate(MinHeight.Value), (int)Math.Truncate(MaxHeight.Value), (int)Math.Truncate(MaxWid.Value), (int)Math.Truncate(NumEnemies.Value));
            Output.Text = GameEng.ToString();
            /*btnDown.Enabled = true;
            btnUp.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;*/
            btnSave.Enabled = true;
            btnLoad.Enabled = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            /*GameEng.MoveUp();
            Output.Text = GameEng.ToString();*/
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BinarySerialization.WriteToBinaryFile(@"..\Save Folder\SaveFile.bin", GameEng.GetMap());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GameEng.SetMap((Map)BinarySerialization.ReadFromBinaryFile(@"..\Save Folder\SaveFile.bin"));
            Output.Text = GameEng.ToString();
        }
    }

    public static class BinarySerialization
    {
        public static void WriteToBinaryFile(string filePath, object ObjectToWrite)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, ObjectToWrite);
            }
        }

        public static object ReadFromBinaryFile(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (object)binaryFormatter.Deserialize(stream);
            }

        }
    }

    [Serializable]
    public abstract class Tile
    {

        protected int X
        {
            get;
            set;
        }
        protected int Y
        {
            get;
            set;
        }

        public enum Type
        {
            Hero,
            Enemy,
            Gold,
            Weapon,
            Empty,
            Obstacle,
            Goblin,
            Mage,
            leader
        }

        private Type tileType;

        public Type getTileType()
        {
            return this.tileType;
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
            this.tileType = type;
        }


    }

    [Serializable]
    class Obstacle : Tile
    {
        public Obstacle(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
    }

    [Serializable]
    public class EmptyTile : Tile
    {
        public EmptyTile(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
    }

    [Serializable]
    public abstract class Item : Tile
    {
        public Item(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
        public abstract override string ToString();
    }

    [Serializable]
    public class Gold : Item
    {
        public int Amount;
        private Random rand = new Random();
        public Gold(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
            Amount = rand.Next(1, 6);
        }
        public override string ToString()
        {
            return "Gold";
        }
    }

    [Serializable]
    public abstract class Weapon : Item
    {
        public Weapon(int NewX, int NewY, Type type) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
        }
        protected int Damage { get; set; }
        protected int Range;
        public virtual int GetRange()
        {
            return Range;
        }
        protected int Durability { get; set; }
        protected int Cost { get; set; }
        protected enum WeaponType
        {
            Melee,
            Ranged
        }
    }

    [Serializable]
    public class MeleeWeapon : Weapon
    {
        public MeleeWeapon(int NewX, int NewY, Type type, MWeaponType mtype) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
            if (mtype == MWeaponType.Dagger)

            {
                Damage = 3;
                Durability = 10;
                Cost = 3;
            }
            else if (mtype == MWeaponType.Longsword)
            {
                Damage = 4;
                Durability = 6;
                Cost = 5;
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public enum MWeaponType
        {
            Dagger,
            Longsword
        }
        public override int GetRange()
        {
            return 1;
        }

    }

    [Serializable]
    public class RangedWeapon : Weapon
    {
        public enum RWeaponType
        {
            Rifle,
            Longbow
        }
        public RangedWeapon(int NewX, int NewY, Type type, RWeaponType rtype) : base(NewX, NewY, type)
        {
            X = NewX;
            Y = NewY;
            if (rtype == RWeaponType.Longbow)
            {
                Damage = 5;
                Range = 2;
                Durability = 4;
                Cost = 6;
            }
            else if (rtype == RWeaponType.Rifle)
            {
                Damage = 5;
                Range = 3;
                Durability = 4;
                Cost = 7;
            }
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public override int GetRange()
        {
            return Range;
        }
    }

    [Serializable]
    public abstract class Character : Tile
    {
        protected int MaxHP { get; set; }
        protected int HP { get; set; }
        protected int Damage { get; set; }
        public Tile[] ArrVision = new Tile[4];  //Up, Down, Left, Right
        public int AmountGold { get; set; }

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
        public int DistanceTo(Character Target)
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
            switch (move)
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
        public void Pickup(Item item)
        {
            if (item.getTileType() == Tile.Type.Gold)
            {
                AmountGold = AmountGold + ((Gold)item).Amount;
            }
        }

        public abstract Movement ReturnMove(Movement move);
        public abstract override string ToString();
    }

    [Serializable]
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

    [Serializable]
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
                        if (ArrVision[2] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Up;
                        }
                        break;
                    case 2:
                        if (ArrVision[3] is EmptyTile)
                        {
                            Check = true;
                            temp = Movement.Down;
                        }
                        break;
                    case 3:
                        if (ArrVision[0] is EmptyTile)
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

    [Serializable]
    public class Mage : Enemy
    {
        public Mage(int NewX, int NewY, Type type, int NewMaxHP, int NewDamage) : base(NewX, NewY, type, NewMaxHP, NewDamage)
        {
            X = NewX;
            Y = NewY;
            HP = 5;
            MaxHP = 5;
            Damage = 5;
        }

        public override Movement ReturnMove(Movement move)
        {
            return Movement.None;
        }

        public override bool CheckRange(Character Target)
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
    }

    [Serializable]
    public class Leader : Enemy
    {
        private Tile Target;
        public void SetTarget(Hero target)
        {
            Target = target;
        }
        public Leader(int NewX, int NewY, Type type, int NewMaxHP, int NewDamage) : base(NewX, NewY, type, NewMaxHP, NewDamage)
        {
            X = NewX;
            Y = NewY;
            HP = 20;
            MaxHP = 20;
            Damage = 2;
        }

        public override Movement ReturnMove(Movement move)
        {
            Movement temp = Movement.None;
            if (Target.getX() > this.X)
            {
                if (this.ArrVision[1] is EmptyTile)
                {
                    temp = Movement.Right;
                }
                else
                {
                    temp = Movement.None;
                }
            }
            else if (Target.getX() < this.X)
            {
                if (this.ArrVision[0] is EmptyTile)
                {
                    temp = Movement.Left;
                }
                else
                {
                    temp = Movement.None;
                }
            }
            else if (Target.getY() > this.Y)
            {
                if (this.ArrVision[2] is EmptyTile)
                {
                    temp = Movement.Up;
                }
                else
                {
                    temp = Movement.None;
                }
            }
            else if (Target.getY() < this.Y)
            {
                if (this.ArrVision[3] is EmptyTile)
                {
                    temp = Movement.Down;
                }
                else
                {
                    temp = Movement.None;
                }
            }
            if (temp == Movement.None)
            {
                bool Check = false;
                while (Check == false)
                {
                    int iCheck = rnd.Next(1, 5); //Up, Down, Left, Right 
                    switch (iCheck)
                    {
                        case 1:
                            if (ArrVision[2] is EmptyTile)
                            {
                                Check = true;
                                temp = Movement.Up;
                            }
                            break;
                        case 2:
                            if (ArrVision[3] is EmptyTile)
                            {
                                Check = true;
                                temp = Movement.Down;
                            }
                            break;
                        case 3:
                            if (ArrVision[0] is EmptyTile)
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
            }
            return temp;
        }
    }

    [Serializable]
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
            s = s + "Gold: " + AmountGold;
            s = s + "[" + X + "," + Y + "]";
            return s;
        }
    }

    [Serializable]
    public class Map
    {
        private Tile[,] ArrMap;

        public Tile[,] getMap()
        {
            return ArrMap;
        }
        public Hero hero = null;
        private Enemy[] ArrEnemy;
        public Enemy[] getEnemy()
        {
            return ArrEnemy;
        }
        private int height;
        public int getHeight()
        {
            return height;
        }

        private int width;
        public int getWidth()
        {
            return width;
        }
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
                    ArrMap[i, a] = new EmptyTile(i, a, Tile.Type.Empty);
                }
            }
            for (int i = 0; i < width; i++)
            {
                ArrMap[i, 0] = new Obstacle(i, 0, Tile.Type.Obstacle);
                ArrMap[i, height - 1] = new Obstacle(i, height, Tile.Type.Obstacle);
            }
            for (int i = 0; i < height; i++)
            {
                ArrMap[0, i] = new Obstacle(0, i, Tile.Type.Obstacle);
                ArrMap[width - 1, i] = new Obstacle(width, i, Tile.Type.Obstacle);
            }
            Create(Tile.Type.Hero);
            ArrMap[hero.getX(), hero.getY()] = hero;
            ArrEnemy = new Enemy[numEnemy];
            for (int i = 0; i < numEnemy; i++)
            {
                if (rnd.Next(1, 7) < 6)
                {
                    ArrEnemy[i] = (Enemy)Create(Tile.Type.Goblin);
                    ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY()] = ArrEnemy[i];
                }
                else
                {
                    ArrEnemy[i] = (Enemy)Create(Tile.Type.Mage);
                    ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY()] = ArrEnemy[i];
                }

            }
            int itemp = rnd.Next(1, 5);
            for (int i = 0; i < itemp; i++)
            {
                int x = 0;
                int y = 0;
                while (!(ArrMap[x, y] is EmptyTile))
                {
                    x = rnd.Next(1, width - 1);
                    y = rnd.Next(1, height - 1);
                }
                ArrMap[x, y] = new Gold(x, y, Tile.Type.Gold);
            }
        }
        public void UpdateVision()
        {
            for (int i = 0; i < ArrEnemy.Length; i++)
            {
                for (int a = 0; a < 4; a++) //left, right, up, down
                {
                    switch (a)
                    {
                        case 0:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX() - 1, ArrEnemy[i].getY()];
                            break;
                        case 1:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX() + 1, ArrEnemy[i].getY()];
                            break;
                        case 2:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY() + 1];
                            break;
                        case 3:
                            ArrEnemy[i].ArrVision[a] = ArrMap[ArrEnemy[i].getX(), ArrEnemy[i].getY() - 1];
                            break;
                    }
                }
            }
            for (int a = 0; a < 4; a++) //Up, Down, Left, Right
            {
                switch (a)
                {
                    case 0:
                        hero.ArrVision[a] = ArrMap[hero.getX() - 1, hero.getY()];
                        break;
                    case 1:
                        hero.ArrVision[a] = ArrMap[hero.getX() + 1, hero.getY()];
                        break;
                    case 2:
                        hero.ArrVision[a] = ArrMap[hero.getX(), hero.getY() + 1];
                        break;
                    case 3:
                        hero.ArrVision[a] = ArrMap[hero.getX(), hero.getY() - 1];
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
                x = rnd.Next(1, width - 1);
                y = rnd.Next(1, height - 1);
            }
            switch (type)
            {
                case Tile.Type.Hero:
                    hero = new Hero(x, y, 0, 20);
                    temp = hero;
                    break;
                case Tile.Type.Goblin:
                    Goblin goblin = new Goblin(x, y, Tile.Type.Goblin, 10, 1);
                    temp = goblin;
                    break;
                case Tile.Type.Mage:
                    Mage mage = new Mage(x, y, Tile.Type.Mage, 5, 5);
                    temp = mage;
                    break;
            }
            return temp;
        }

        public Item getItemAtPosition(int x, int y)
        {
            Item temp = null;
            if (getMap()[x, y] is Item)
            {
                temp = (Item)getMap()[x, y];
                getMap()[x, y] = new EmptyTile(x, y, Tile.Type.Empty);
            }
            return temp;
        }

    }

    [Serializable]
    public class Shop
    {
        private Weapon[] arrWeapon;
        private Random rnd = new Random();
        private Character buyer;
        public Shop(Character chr)
        {
            int MorR;
            int Weap;
            buyer = chr;
            for (int i = 0; i < 3; i++)
            {
                MorR = rnd.Next(1, 3);
                if (MorR == 1)
                {
                    Weap = rnd.Next(1, 3);
                    if (Weap == 1)
                    {

                    }
                }
            }
        }
    }

    [Serializable]
    public class GameEngine
    {
        private Map map;
        readonly static char Hero = 'H';
        readonly static char Goblin = 'G';
        readonly static char Mage = 'M';
        readonly static char Gold = 'g';
        readonly static char Empty = '~';
        readonly static char Obstacle = '#';
        public GameEngine(int minW, int minH, int maxH, int maxW, int numEnemy)
        {
            map = new Map(minW, minH, maxH, maxW, numEnemy);
        }

        public Map GetMap()
        {
            return this.map;
        }

        public void SetMap(Map newmap)
        {
            map = newmap;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < map.getWidth(); i++)
            {
                for (int a = 0; a < map.getHeight(); a++)
                {

                    switch (map.getMap()[i, a].getTileType())
                    {
                        case Tile.Type.Empty:
                            s = s + Empty;
                            break;
                        case Tile.Type.Gold:
                            s = s + Gold;
                            break;
                        case Tile.Type.Goblin:
                            s = s + Goblin;
                            break;
                        case Tile.Type.Mage:
                            s = s + Mage;
                            break;
                        case Tile.Type.Hero:
                            s = s + Hero;
                            break;
                        case Tile.Type.Obstacle:
                            s = s + Obstacle;
                            break;
                    }
                }
                s = s + "\n";
            }
            return s;
        }

        public void EnemyAttacks()
        {
            for (int i = 0; i < map.getEnemy().Length; i++)
            {
                if (map.getEnemy()[i].getTileType() == Tile.Type.Mage)
                {
                    for (int j = 0; j < map.getEnemy().Length; j++)
                    {
                        if (i != j)
                        {
                            if (map.getEnemy()[i].CheckRange(map.getEnemy()[j]))
                            {
                                map.getEnemy()[i].Attack(map.getEnemy()[j]);
                            }
                        }
                    }
                    if (map.getEnemy()[i].CheckRange(map.hero))
                    {
                        map.getEnemy()[i].Attack(map.hero);
                    }
                }
                else
                {
                    for (int j = 0; j < map.getEnemy()[j].ArrVision.Length; j++)
                    {
                        if (map.getEnemy()[j].ArrVision[j].getTileType() == Tile.Type.Hero)
                        {
                            map.getEnemy()[j].Attack(map.hero);
                        }        
                    }
                }
            }
        }

        public void MoveEnemies()
        {
            for (int i = 0; i < map.getEnemy().Length; i++)
            {
                if (map.getEnemy()[i].IsDead())
                {
                    map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()] = new EmptyTile(map.getEnemy()[i].getX(), map.getEnemy()[i].getY(), Tile.Type.Empty);
                }
                else
                {
                    Character.Movement temp2 = map.getEnemy()[i].ReturnMove(Character.Movement.None);
                    map.getEnemy()[i].Move(temp2);
                    switch (temp2)
                    {
                        case Character.Movement.Up:
                            map.getMap()[map.getEnemy()[i].getX() - 1, map.getEnemy()[i].getY()] = map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()];
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()] = new EmptyTile(map.getEnemy()[i].getX(), map.getEnemy()[i].getY(), Tile.Type.Empty);
                            break;
                        case Character.Movement.Down:
                            map.getMap()[map.getEnemy()[i].getX() + 1, map.getEnemy()[i].getY()] = map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()];
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()] = new EmptyTile(map.getEnemy()[i].getX(), map.getEnemy()[i].getY(), Tile.Type.Empty);
                            break;
                        case Character.Movement.Left:
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY() + 1] = map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()];
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()] = new EmptyTile(map.getEnemy()[i].getX(), map.getEnemy()[i].getY(), Tile.Type.Empty);
                            break;
                        case Character.Movement.Right:
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY() - 1] = map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()];
                            map.getMap()[map.getEnemy()[i].getX(), map.getEnemy()[i].getY()] = new EmptyTile(map.getEnemy()[i].getX(), map.getEnemy()[i].getY(), Tile.Type.Empty);
                            break;
                        default:
                            break;
                    }
                }
            }
        }



        /*public void MoveUp()
        {
            map.UpdateVision();
            Tile.Type temp = (map.getMap()[map.hero.getX() - 1, (map.hero.getY())].getTileType());
            if (map.hero.ReturnMove(Character.Movement.Up) != Character.Movement.None)
            {
                map.getMap()[map.hero.getX() - 1, (map.hero.getY())] = map.getMap()[map.hero.getX(), map.hero.getY()];
                map.getMap()[map.hero.getX(), map.hero.getY()] = new EmptyTile(map.hero.getX(), map.hero.getY(), Tile.Type.Empty);
                map.hero.Move(map.hero.ReturnMove(Character.Movement.Up));
            }
            else if (temp == Tile.Type.Enemy)
            {
                map.hero.Attack((Enemy)map.getMap()[map.hero.getX() - 1, (map.hero.getY())]);
            }            
        } */
    }
}

