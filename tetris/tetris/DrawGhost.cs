using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainWindow : Form
    {
        // Показувати сірий попередній перегляд положення проекції
        private void DrawGhost()
        {
            Control[] Ghost2 = { null, null, null, null };
            bool ghostFound = false;

            // Видалити попередню проекцію
            foreach (Control x in Ghost)
            {
                if (x != null)
                {
                    if (x.BackColor == Color.LightGray)
                    {
                        x.BackColor = Color.White;
                    }
                }
            }

            for (int x = 0; x < 4; x++)
            {
                Ghost2[x] = activePiece2[x];
            }

            for (int x = 21; x > 1; x--)
            {
                // Перевірка позиції проекції,починаючи з нижнього рядка
                if (currentPiece == 0) //I фігура
                {
                    if (rotations == 0)
                    {
                        if (x == 2)
                        {
                            Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                        }
                        else
                        {
                            Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                            Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 2);
                            Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 3);
                        }
                    }
                    else if (rotations == 1)
                    {
                        if (x == 2) 
                        {
                            Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                        }

                        else 
                        {
                            Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                            Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                            Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                            Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                        }
                    }
                }
                else if (currentPiece == 1) // L фігура
                {
                    if (rotations == 0)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 2);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                    else if (rotations == 1)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 1);
                    }
                    else if (rotations == 2)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 2);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 2);
                    }
                    else if (rotations == 3)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 1);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                }
                else if (currentPiece == 2) // J фігура
                {
                    if (rotations == 0)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 2);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                    else if (rotations == 1)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 1);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                    else if (rotations == 2)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 2);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 2);
                    }
                    else if (rotations == 3)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 1);
                    }
                }
                else if (currentPiece == 3) // S фігура
                {
                    if (rotations == 0)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 1);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 1);
                    }
                    else if (rotations == 1)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 2);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 1);
                    }
                }
                else if (currentPiece == 4) // Z фігура
                {
                    if (rotations == 0)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                    else if (rotations == 1)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 1);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 2);
                    }
                }
                else if (currentPiece == 5) // O фігура
                {
                    Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                    Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                    Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                    Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                }
                else if (currentPiece == 6) //T фігура
                {
                    if (rotations == 0)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                    else if (rotations == 1)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 2);
                    }
                    else if (rotations == 2)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x - 1);
                    }
                    else if (rotations == 3)
                    {
                        Ghost2[0] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[0]), x - 1);
                        Ghost2[1] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[1]), x - 1);
                        Ghost2[2] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[2]), x - 2);
                        Ghost2[3] = grid.GetControlFromPosition(grid.GetColumn(Ghost2[3]), x);
                    }
                }

                //Якщо проекцію не збережено
                if (ghostFound == false)
                {
                    // Якщо всі квадрати в тесті заняті
                    if (
                        (Ghost2[0].BackColor == Color.White | activePiece.Contains(Ghost2[0])) &
                        (Ghost2[1].BackColor == Color.White | activePiece.Contains(Ghost2[1])) &
                        (Ghost2[2].BackColor == Color.White | activePiece.Contains(Ghost2[2])) &
                        (Ghost2[3].BackColor == Color.White | activePiece.Contains(Ghost2[3]))
                        )
                    {

                        ghostFound = true;
                        for (int y = 0; y < 4; y++)
                        {
                            Ghost[y] = Ghost2[y];
                        }
                    }

                    // Якщо не всі вільні квадрати (і нічого не збережено), перевірте наступний рядок
                    else
                    {
                        continue;
                    }
                }

                // Проекція збережена
                else if (ghostFound == true)
                {

                    // Не всі квадрати вільні
                    if (Ghost2[0].BackColor != Color.White | Ghost2[1].BackColor != Color.White | Ghost2[2].BackColor != Color.White | Ghost2[3].BackColor != Color.White)
                    {

                        // Чи падає фігура нижче x?
                        if (grid.GetRow(activePiece[0]) >= x | grid.GetRow(activePiece[1]) >= x | grid.GetRow(activePiece[2]) >= x | grid.GetRow(activePiece[3]) >= x)
                        {
                            continue;
                        }

                        ghostFound = false;
                        for (int y = 0; y < 4; y++)
                        {
                            Ghost[y] = null;
                        }
                        continue;
                    }
                }
            }

            // Намалювати проекцію
            if (ghostFound == true)
            {
                for (int x = 0; x < 4; x++)
                {
                    Ghost[x].BackColor = Color.LightGray;
                }
            }
        }
    }
}