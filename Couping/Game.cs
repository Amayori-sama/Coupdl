using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Couping
{
    public class Game : Control
    {
        protected int _sizeS;
        protected int _size;
        public int minSize;
        public int maxSize;

        protected int _x;// расположение X
        protected int _y;// расположение Y

        public int _imSize = 150;//ширина фото высота фото

        protected bool _gameStatus = false;
        public bool GameStatus
        {
            get 
            {
                return _gameStatus;
            }
        }

        private int _level;
        public int gameLevel
        {
            get => _level;
            set { 
                if (value > 0 && value < 10)
                    _level = value;
            }
        }

        protected int X
        {
            get => _x;
            set { if (_x != 0) { _x = 0; } }
        }
        protected int Y
        {
            get => _y;
            set { if (_y != 0) { _y = 0; } }
        }

        //Метод вызывающийся при запуске игры
        public void StarProcess()
        {            
            _gameStatus = true;
            if (_gameStatus)
            {
                left = gameLevel + 1;
                GenerateCoup();
            }

            Invalidate();
        }
        //Метод вызывающийся при завершении игры
        public void EndProcess()
        {
            _gameStatus = false;
            AllClear();
            Invalidate();
        }

        public async void NextLvl() 
        {
            AllClear(); 
            _gameStatus = true;
            gameLevel += 1;
            left = gameLevel + 1;
            if (gameLevel == 4)
            {
                WinOrLose();
            }
            else
            {
                MessageBox.Show("Загрузка уровня");
                await Task.Delay(2000);
                GenerateCoup();
            }
        }
        public void WinOrLose() 
        {
            MessageBox.Show("You Win");
            EndProcess();
            gameLevel = 1;
        }
        //Метод очистки игровой зоны
        public void AllClear()
        {
            cards1.Clear();
            cards2.Clear();
            cards1bg.Clear();
            cards2bg.Clear();
            selectedBone = -1;
            selectedBone1 = -1;
        }

        List<int> cards1 = new List<int>();
        List<int> cards2 = new List<int>();
        List<int> cards1bg = new List<int>();
        List<int> cards2bg = new List<int>();

        //генерация наименований картинок
        public void GenerateCoup()
        {
            bool flag = true;
            bool flag2 = true;
            int cc = gameLevel + 1;
            while (cards1.Count != cc)
            {
                Random r = new Random();
                int value = r.Next(0, 7);
                flag = cards1.Contains(value);
                if (flag == false)
                {
                    cards1.Add(value);
                    cards1bg.Add(10);
                }
            }
            while (cards2.Count != cards1.Count)
            {
                Random d = new Random();
                int value = d.Next(0, 7);
                flag = cards1.Contains(value);
                flag2 = cards2.Contains(value);
                if (flag)
                {
                    if (flag2 == false)
                    {
                        cards2.Add(value);
                        cards2bg.Add(10);
                    }
                }
            }
        }

        //Метод отвечающий за прорисовку уровня
        protected override void OnPaint(PaintEventArgs e)
        {
            int countcards = 0;
            if (_gameStatus == true) 
            {
                GenerateCoup();
                countcards =  gameLevel + 1;
            }
            Rectangle[] rct1 = new Rectangle[countcards];
            Rectangle[] rct2 = new Rectangle[countcards];
            for (int j = 0; j < countcards; j++)
            {
                rct1[j] = new Rectangle(X + (j * _imSize) + j * 20, Y, _imSize, _imSize);
                rct2[j] = new Rectangle(X + (j * _imSize) + j * 20, Y + (_imSize) + 20, _imSize, _imSize);
                Bitmap img1 = (Bitmap)Properties.Resources.ResourceManager.GetObject($"p{cards1[j]}");
                Bitmap img2 = (Bitmap)Properties.Resources.ResourceManager.GetObject($"p{cards2[j]}");

                if (j == selectedBone && flag1)
                {
                    e.Graphics.DrawImage(img1, rct1[j]);
                }
                else
                {
                    e.Graphics.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject($"p{cards1bg[j]}"), rct1[j]);
                }

                if (j == selectedBone1 && flag2)
                {
                    e.Graphics.DrawImage(img2, rct2[j]);
                }
                else
                {
                    e.Graphics.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject($"p{cards2bg[j]}"), rct2[j]);
                }
            }
            e.Dispose();
        }

        public Game()
        {
            _size = 20;
            minSize = 200;
            maxSize = 800;
            _y = 20;
            _x = 20;
        }  

        public int iSize
        {
            get
            {
                return _imSize;
            }
            set
            {
                if (_imSize == 0)
                {
                    _imSize = 100;
                }
                else
                {
                    _imSize = value;
                    Invalidate();
                }
            }
        }

        bool flag1 = false; 
        bool flag2 = false;
        int selectedBone = -1; 
        int selectedBone1 = -1;
        int fl1; 
        int fl2;
        public void onMouseClick(MouseEventArgs e)
        {
            int zi = e.X / (_imSize + 20);
            int zj = e.Y / (_imSize + 20);
            if (e.Button == MouseButtons.Left)
            {

                if (zi < cards1.Count && zj == 0)
                {
                    flag1 = true;
                    selectedBone = zi;

                    fl1 = cards1.ElementAt(selectedBone);
                }
                if (zi < cards2.Count && zj == 1)
                {
                    flag2 = true;
                    selectedBone1 = zi;
                    fl2 = cards2.ElementAt(selectedBone1);
                }
                IsCoup();
            }
            Invalidate();
        }

        public int left;
        //Метод проверки одинаковых изображений
        public async void IsCoup()
        {
            int f1 = fl1;
            int f2 = fl2;
            if (flag1 == flag2)
            {
                await Task.Delay(1000);
                if (f1 == f2)
                {
                    cards1bg[selectedBone] = 9;
                    cards1[selectedBone] = 9;
                    cards2bg[selectedBone1] = 9;
                    cards2[selectedBone1] = 9;
                    flag1 = false;
                    flag2 = false;
                    left -= 1;
                    if (left == 0) 
                    { 
                        NextLvl(); 
                    } 
                }
                else
                {
                    flag1 = false;
                    flag2 = false;                
                }
            }
            Invalidate();
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            Invalidate();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;
                return cp;
            }
        }
    }
}