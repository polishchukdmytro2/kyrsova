using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainWindow : Form
    {
        // Обробка введених даних - запускається при будь-якому натисканні клавіші
        // Потрібне очищення
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!CheckGameOver() & ((e.KeyCode == Keys.Left | e.KeyCode == Keys.A) & TestMove("left") == true))
            {
                MovePiece("left");
            }
            else if (!CheckGameOver() & ((e.KeyCode == Keys.Right | e.KeyCode == Keys.D) & TestMove("right") == true))
            {
                MovePiece("right");
            }
            else if ((e.KeyCode == Keys.Down | e.KeyCode == Keys.S) & TestMove("down") == true)
            {
                MovePiece("down");
            }
            else if (e.KeyCode == Keys.Up | e.KeyCode == Keys.W)
            {
                // Поворот

                int square1Col = grid.GetColumn(activePiece[0]);
                int square1Row = grid.GetRow(activePiece[0]);

                int square2Col = grid.GetColumn(activePiece[1]);
                int square2Row = grid.GetRow(activePiece[1]);

                int square3Col = grid.GetColumn(activePiece[2]);
                int square3Row = grid.GetRow(activePiece[2]);

                int square4Col = grid.GetColumn(activePiece[3]);
                int square4Row = grid.GetRow(activePiece[3]);

                if (currentPiece == 0) 
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю 
                    if (rotations == 0 & (square1Col == 0 | square1Col == 1 | square1Col == 9))
                    {
                        return;
                    }
                    else if (rotations == 1 & (square3Col == 0 | square3Col == 1 | square3Col == 9))
                    {
                        return;
                    }

                    // Якщо все нормально,то повернути фігуру 
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row - 3);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 2, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 1, square4Row + 3);

                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (currentPiece == 1) //Нормальна L
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю 
                    if (rotations == 0 & (square1Col == 8 | square1Col == 9))
                    {
                        return;
                    }
                    else if (rotations == 2 & (square1Col == 9))
                    {
                        return;
                    }

                    // Якщо тест пройшов, поверніть фігуру
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row + 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 2, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row - 1);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row - 1);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 2)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 3)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (currentPiece == 2) // Зад L
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю.
                    if (rotations == 0 & (square1Col == 0 | square1Col == 1))
                    {
                        return;
                    }
                    else if (rotations == 2 & square1Col == 0)
                    {
                        return;
                    }

                    // Якщо все нормально,то повернути фігуру 
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 2)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row + 1);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 3)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row - 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 2, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row + 1);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (currentPiece == 3) // Нормальна S
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю
                    if (rotations == 0 & (square1Row == 1 | square1Col == 9))
                    {
                        return;
                    }
                    else if (rotations == 1 & square1Col == 0)
                    {
                        return;
                    }

                    // Якщо все нормально,то повернути фігуру 
                    if (rotations == 0)
                    {

                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row - 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 1);


                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 1, square1Row + 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 1);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (currentPiece == 4) // Зад S
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю
                    if (rotations == 1 & square1Col == 8)
                    {
                        return;
                    }

                    // Якщо все нормально,то повернути фігуру 
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 1, square4Row - 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row + 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (currentPiece == 5) // Квадрат
                {
                    // Квадрат не повертається
                    return;
                }
                else if (currentPiece == 6) // Піраміда
                {
                    // Перевірте, чи не надто близько знаходиться фігура до краю
                    if (rotations == 1 & square1Col == 9)
                    {
                        return;
                    }
                    else if (rotations == 3 & square1Col == 0)
                    {
                        return;
                    }

                    // Якщо все нормально,то повернути фігуру 
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 2);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 1)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 2, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 2)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 2);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 2, square4Row);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (rotations == 3)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 2, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 2);

                        // Перевірте,чи нова позиція перекриває іншу фігуру. Якщо так,скасувати обертання.
                        if (TestOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                //Встановити стару позицію фігури на білу
                foreach (PictureBox square in activePiece)
                {
                    square.BackColor = Color.White;
                }

                DrawGhost();

                // Встановити нову позицію фігури до кольору цієї частини
                int x = 0;
                foreach (PictureBox square in activePiece2)
                {
                    square.BackColor = colorList[currentPiece];
                    activePiece[x] = square;
                    x++;
                }
            }
            else if (!CheckGameOver() & e.KeyCode == Keys.ShiftKey)
            {
                rotations = 0;

                // Параметри макета для збереженой фігури
                Control[,] savedPieceArray =
                {
                        { box219, box223, box227, box231 }, // I фігура
                        { box218, box222, box226, box227 }, // L фігура
                        { box219, box223, box227, box226 }, // J фігура
                        { box222, box223, box219, box220 }, // S фігура
                        { box218, box219, box223, box224 }, // Z фігура
                        { box222, box223, box226, box227 }, // O фігура
                        { box223, box226, box227, box228 }  // T фігура
                };

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

                // Стерти падаючу частину
                foreach (Control x in activePiece)
                {
                    x.BackColor = Color.White;
                }

                // Якщо жодна фігура ще не збережено
                if (savedPieceInt == -1)
                {
                    // Отримати макет для збереженой фігури
                    savedPieceInt = currentPiece;
                    for (int x = 0; x < 4; x++)
                    {
                        savedPiece[x] = savedPieceArray[savedPieceInt, x];
                    }

                    // Намалюйте збережену фігуру
                    savedPieceColor = colorList[savedPieceInt];
                    foreach (Control x in savedPiece)
                    {
                        x.BackColor = savedPieceColor;
                    }

                    DropNewPiece();
                }

                // Якщо фігуру вже збережено
                else
                {

                    // Стерти збережену фігуру
                    foreach (Control x in savedPiece)
                    {
                        x.BackColor = Color.White;
                    }

                    // Зміна фігуру місцями
                    int savedPieceTemp = currentPiece;
                    currentPiece = savedPieceInt;
                    savedPieceInt = savedPieceTemp;
                    for (int x = 0; x < 4; x++)
                    {
                        savedPiece[x] = savedPieceArray[savedPieceInt, x];
                        activePiece2[x] = activePieceArray[currentPiece, x];
                    }

                    // Намалювати збережену фігуру
                    savedPieceColor = colorList[savedPieceInt];
                    foreach (Control x in savedPiece)
                    {
                        x.BackColor = savedPieceColor;
                    }

                    // Намалювати падаючу фігуру
                    pieceColor = colorList[currentPiece];
                    foreach (Control square in activePiece2)
                    {
                        square.BackColor = pieceColor;
                    }

                    DrawGhost();

                    for (int x = 0; x < 4; x++)
                    {
                        activePiece[x] = activePiece2[x];
                    }
                }
            }

            else if (!CheckGameOver() & e.KeyCode == Keys.Space)
            {
                // Бистре скидання 
                for (int x = 0; x < 4; x++)
                {
                    Ghost[x].BackColor = colorList[currentPiece];
                    activePiece[x].BackColor = Color.White;
                }
                if (CheckForCompleteRows() > -1)
                {
                    ClearFullRow();
                }
                DropNewPiece();
            }
        }
    }
}