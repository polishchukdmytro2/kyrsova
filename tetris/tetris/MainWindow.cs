using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainWindow : Form
    {
        // Ініціалізація глобальних змінних
        Control[] activePiece = { null, null, null, null };
        Control[] activePiece2 = { null, null, null, null };
        Control[] nextPiece = { null, null, null, null };
        Control[] savedPiece = { null, null, null, null };
        Control[] Ghost = { null, null, null, null };
        List<int> PieceSequence = new List<int>();
        int timeElapsed = 0;
        int currentPiece;
        int nextPieceInt;
        int savedPieceInt = -1;
        int rotations = 0;
        Color pieceColor = Color.White;
        Color savedPieceColor = Color.White;
        int combo = 0;
        int score = 0;
        int clears = 0;
        int level = 0;
        bool gameOver = false;
        int PieceSequenceIteration = 0;

        readonly Color[] colorList = 
        {  
            Color.Cyan,     // I фігура
            Color.Orange,   // L фігура
            Color.Blue,     // J фігура
            Color.Green,    // S фігура
            Color.Red,      // Z фігура
            Color.Yellow,   // O фігура
            Color.Purple    // T фігура
        };

        // Головне меню
        public MainWindow()      
        {
            InitializeComponent();

            ScoreUpdateLabel.Text = "";
            SpeedTimer.Start();
            GameTimer.Start();

            // Встановлення привида
            // 1-4 - невидимі
            activePiece2[0] = box1;
            activePiece2[1] = box2;
            activePiece2[2] = box3;
            activePiece2[3] = box4;

            // Створення послідовності фігурок
            System.Random random = new System.Random();
            while (PieceSequence.Count < 7)
            {
                int x = random.Next(7);
                if (!PieceSequence.Contains(x))
                {
                    PieceSequence.Add(x);
                }
            }

            // Перша частина
            nextPieceInt = PieceSequence[0];
            PieceSequenceIteration++;

            DropNewPiece();
        }

        public void DropNewPiece()
        {
            // Скидання кількості обертань поточної фігури
            rotations = 0;

            // Перемістити наступну фігуру до поточної
            currentPiece = nextPieceInt;

            if (PieceSequenceIteration == 7)
            {
                PieceSequenceIteration = 0;

                PieceSequence.Clear();
                System.Random random = new System.Random();
                while (PieceSequence.Count < 7)
                {
                    int x = random.Next(7);
                    if (!PieceSequence.Contains(x))
                    {
                        PieceSequence.Add(x);
                    }
                }
            }

            nextPieceInt = PieceSequence[PieceSequenceIteration];
            PieceSequenceIteration++;

            // Якщо не перший хід, очистити панель наступної частини
            if (nextPiece.Contains(null) == false)
            {
                foreach (Control x in nextPiece)
                {
                    x.BackColor = Color.White;
                }
            }

            // Варіанти макета для наступної фігури
            Control[,] nextPieceArray = 
            {
                { box203, box207, box211, box215 }, // I фігура
                { box202, box206, box210, box211 }, // L фігура
                { box203, box207, box211, box210 }, // J фігура
                { box206, box207, box203, box204 }, // S фігура
                { box202, box203, box207, box208 }, // Z фігура
                { box206, box207, box210, box211 }, // O фігура
                { box207, box210, box211, box212 }  // T фігура
            };

            // Отримати макет для наступної фігури
            for (int x = 0; x < 4; x++)
            {
                nextPiece[x] = nextPieceArray[nextPieceInt,x];
            }

            // Заповнити наступну панель правим кольором
            foreach (Control square in nextPiece)
            {
                square.BackColor = colorList[nextPieceInt];
            }

            // Варіанти розкладки падаючой фігури
            Control[,] activePieceArray =
            {
                { box6, box16, box26, box36 }, // I фігура
                { box4, box14, box24, box25 }, // L фігура
                { box5, box15, box25, box24 }, // J фігура
                { box14, box15, box5, box6 },  // S фігура
                { box5, box6, box16, box17 },  // Z фігура
                { box5, box6, box15, box16 },  // O фігура
                { box6, box15, box16, box17 }  // T фігура
            };

            // Виберіть падаючу частину
            for (int x = 0; x < 4; x++)
            {
                activePiece[x] = activePieceArray[currentPiece, x];
            }

            for (int x = 0; x < 4; x++)
            {
                activePiece2[x] = activePieceArray[currentPiece, x];
            }

            // Завершення гри
            foreach (Control box in activePiece)
            {
                if (box.BackColor != Color.White & box.BackColor != Color.LightGray)
                {
                    SpeedTimer.Stop();
                    GameTimer.Stop();
                    gameOver = true;
                    MessageBox.Show("Game over!");
                    return;
                }
            }

            DrawGhost();

            // Заповнити падаючі квадрати правильним кольором
            foreach (Control square in activePiece)
            {
                square.BackColor = colorList[currentPiece];
            }
        }

        // Перевіряємо, чи буде потенційний рух (вліво/вправо/вниз) за межами сітки чи перекриватиме іншу частину
        public bool TestMove(string direction)
        {
            int currentHighRow = 21;
            int currentLowRow = 0;
            int currentLeftCol = 9;
            int currentRightCol = 0;

            int nextSquare = 0;

            Control newSquare = new Control();

            // Визначаємо найвищий, найнижчий, лівий і правий ряди потенційного переміщення
            foreach (Control square in activePiece)
            {
                if (grid.GetRow(square) < currentHighRow)
                {
                    currentHighRow = grid.GetRow(square);
                }
                if (grid.GetRow(square) > currentLowRow)
                {
                    currentLowRow = grid.GetRow(square);
                }
                if (grid.GetColumn(square) < currentLeftCol)
                {
                    currentLeftCol = grid.GetColumn(square);
                }
                if (grid.GetColumn(square) > currentRightCol)
                {
                    currentRightCol = grid.GetColumn(square);
                }
            }

            // Перевірити, чи будуть квадрати за межами сітки
            foreach (Control square in activePiece)
            {
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);

                // Ліворуч
                if (direction == "left" & squareCol > 0)
                {
                    newSquare = grid.GetControlFromPosition(squareCol - 1, squareRow);
                    nextSquare = currentLeftCol;
                }
                else if (direction == "left" & squareCol == 0)
                {
                    // Переміщення буде за межами сітки, ліворуч
                    return false;
                }

                // Праворуч
                else if (direction == "right" & squareCol < 9)
                {
                    newSquare = grid.GetControlFromPosition(squareCol + 1, squareRow);
                    nextSquare = currentRightCol;
                }
                else if (direction == "right" & squareCol == 9)
                {
                    // Переміщення буде за межами сітки, праворуч
                    return false;
                }

                // Вниз
                else if (direction == "down" & squareRow < 21)
                {
                    newSquare = grid.GetControlFromPosition(squareCol, squareRow + 1);
                    nextSquare = currentLowRow;
                }
                else if (direction == "down" & squareRow == 21)
                {
                    return false;
                    // Переміщення буде нижче сітки
                }

                // Перевірка, чи потенційний хід перекриває іншу частину
                if ((newSquare.BackColor != Color.White & newSquare.BackColor != Color.LightGray) & activePiece.Contains(newSquare) == false & nextSquare > 0)
                {
                    return false;
                }

            }

            return true;
        }

        public void MovePiece(string direction)
        {
            // Стерти стару позицію фігури
            // і визначаємо нову позицію на основі напрямку введення
            int x = 0;
            foreach (PictureBox square in activePiece)
            {
                square.BackColor = Color.White;
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);
                int newSquareRow = 0;
                int newSquareCol = 0;
                if (direction == "left")
                {
                    newSquareCol = squareCol - 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "right")
                {
                    newSquareCol = squareCol + 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "down")
                {
                    newSquareCol = squareCol;
                    newSquareRow = squareRow + 1;
                }

                activePiece2[x] = grid.GetControlFromPosition(newSquareCol, newSquareRow);
                x++;
            }

            x = 0;
            foreach (PictureBox square in activePiece2)
            {

                activePiece[x] = square;
                x++;
            }

            DrawGhost();

            x = 0;
            foreach (PictureBox square in activePiece2)
            {
                square.BackColor = colorList[currentPiece];
                x++;
            }
        }

        // Перевірка, чи потенційний поворот перекриває інший фрагмент
        private bool TestOverlap()
        {
            foreach (PictureBox square in activePiece2)
            {
                if ((square.BackColor != Color.White & square.BackColor != Color.LightGray) & activePiece.Contains(square) == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Таймер для швидкості переміщення фігур - збільшується з рівнем гри
        // Швидкість контролюється методом LevelUp().
        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                SpeedTimer.Stop();
                GameTimer.Stop();
                MessageBox.Show("Game over!");
            }

            else
            {
                //Пересунути частину вниз або скинути нову, якщо вона не може рухатися
                if (TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else
                {
                    if (CheckGameOver() == true)
                    {
                        SpeedTimer.Stop();
                        GameTimer.Stop();
                        MessageBox.Show("Game over!");
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                    }
                    DropNewPiece();
                }
            }
        }

        // Пірдрахунок часу гри ( в секундах )
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            TimeLabel.Text = "Time: " + timeElapsed.ToString();
        }

        // Видалення повнхи рядків
        private void ClearFullRow()
        {
            int completedRow = CheckForCompleteRows();

            // Зробити цей ряд білим
            for (int x = 0; x <= 9; x++)
            {
                Control z = grid.GetControlFromPosition(x, completedRow);
                z.BackColor = Color.White;
            }

            // Пересунути всі інші квадрати вниз
            for (int x = completedRow - 1; x >= 0; x--) 
            {
                for (int y = 0; y <= 9; y++)
                {
                    Control z = grid.GetControlFromPosition(y, x);

                    Control zz = grid.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = Color.White;
                }
            }

            UpdateScore();

            clears++;
            ClearsLabel.Text = "Clears: " + clears;

            if (clears % 10 == 0)
            {
                LevelUp();
            }

            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow();
            }
        }

        private void UpdateScore()
        {
            // Очищення 1-3 рядків додає 100 за рядок
            // Чистий чотири рядки (без комбо) додає 800
            // 2 або більше чотирирядних очищення підряд додають 1200

            bool skipComboReset = false;

            // Одиночний
            if (combo == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Подвійний
            else if (combo == 1)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Потрійний
            else if (combo == 2)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Чотирі рядки подряд
            else if (combo == 3)
            {
                score += 500;
                ScoreUpdateLabel.Text = "+800";
                skipComboReset = true;
            }

            // Одиночний чистий, зламана комбінація
            else if (combo > 3 && combo % 4 == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Подвійна чиста, зламана комбінація
            else if (combo > 3 && ((combo - 1) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Потрійна чиста, зламана комбінація
            else if (combo > 3 && ((combo - 2) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Чотири очищення, продовження комбо
            else if (combo > 3 && ((combo - 3) % 4 == 0))
            {
                score += 900;
                ScoreUpdateLabel.Text = "+1200";
                skipComboReset = true;
            }

            if (CheckForCompleteRows() == -1 && skipComboReset == false)
            {
                // 1-3 рядки очищені
                combo = 0;
            }
            else
            {
                combo++;
            }

            ScoreLabel.Text = "Score: " + score.ToString();
            ScoreUpdateTimer.Start();
        }

        // Повертаємо номер рядка найнижчого повного рядка
        // Якщо немає повних рядків, повертаємо -1
        private int CheckForCompleteRows()
        {
            for (int x = 21; x >= 2; x--)
            {
                for (int y = 0; y <= 9; y++)
                {
                    Control z = grid.GetControlFromPosition(y, x);
                    if (z.BackColor == Color.White)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        return x;
                    }
                }
            }
            return -1; 
        }

        // Збільшення швидкості падіння фігур.
        private void LevelUp()
        {
            level++;
            LevelLabel.Text = "Level: " + level.ToString();

            // Мілісекунди на квадрат падають
            // Рівень 1 = 800 мс на квадрат, рівень 2 = 716 мс на квадрат і т.д.
            int[] levelSpeed =
            {
                800, 716, 633, 555, 466, 383, 300, 216, 133, 100, 083, 083, 083, 066, 066,
                066, 050, 050, 050, 033, 033, 033, 033, 033, 033, 033, 033, 033, 033, 016
            };

            // Швидкість не змінюється після рівня 29
            if (level <= 29)
            {
                SpeedTimer.Interval = levelSpeed[level];
            }
        }

        // Гра закінчується, якщо фігура знаходиться у верхньому рядку, коли випадає наступна фігура
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

            foreach (Control box in topRow)
            {
                if ((box.BackColor != Color.White & box.BackColor != Color.LightGray) & !activePiece.Contains(box))
                {
                    // Кінець
                    return true;
                }
            }

            if (gameOver == true)
            {
                return true;
            }

            return false;
        }

        // Очистити сповіщення про оновлення результатів кожні 2 секунди
        private void ScoreUpdateTimer_Tick(object sender, EventArgs e)
        {
                ScoreUpdateLabel.Text = "";
                ScoreUpdateTimer.Stop();
        }

        private void TimeLabel_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }   
}
