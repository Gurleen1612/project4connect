using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace project_connect4
{
    interface IMakeBoard
    {
        void MakeABoard(char[,] board);
    }
    interface IDropNumber
    {
        int DropANumber(char[,] board, String Player);
    }
    interface ICoinDrop
    {
        void CheckDropCoin(char[,] board, string Player, int dropNumber, string name1, string name2);
    }
    interface ICheckFull
    {
        int CheckFullBoard(char[,] board);
    }
    public class Game : IMakeBoard, IDropNumber, ICoinDrop, ICheckFull
    {
        string name1, name2;
        char sign1, sign2;
        char[,] board = new char[10, 10];
        int number, win, full;
        public Game(string name1, string name2, char sign1, char sign2)
        {
            this.name1 = name1;
            this.name2 = name2;
            this.sign1 = sign1;
            this.sign2 = sign2;
        }
        public void Rules()
        {
            Console.WriteLine("Rules to play the game:\n");
            Console.WriteLine("Both players will have particular symbols and they will have alternative turns. In each of their turn they will drop their symbol in a particular row. The player needs to have four consecutive coins in order to win the game.");
        }
        public void MakeABoard(char[,] board)
        {
            int Rows = 6, Columns = 7, i, j;

            for (i = 1; i <= Rows; i++)
            {
                Console.Write("|");
                for (j = 1; j <= Columns; j++)
                {
                    if (board[i, j] != 'X' && board[i, j] != 'O')
                        board[i, j] = '#';

                    Console.Write(board[i, j]);
                }
                Console.Write("| \n");
            }
        }
        public int DropANumber(char[,] board, String Player)
        {
            int dropNumber;

            Console.WriteLine(Player + "! Its Your Turn ");
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7: ");
                dropNumber = Convert.ToInt32(Console.ReadLine());
            }
            while (dropNumber < 1 || dropNumber > 7);

            while (board[1, dropNumber] == 'X' || board[1, dropNumber] == 'O')
            {
                Console.WriteLine("This row is full, please enter your coin in a new row: ");
                dropNumber = Convert.ToInt32(Console.ReadLine());
            }

            return dropNumber;
        }
        public void CheckDropCoin(char[,] board, string Player, int dropNumber, string name1, string name2)
        {
            int length, turn;
            length = 6;
            turn = 0;

            do
            {
                if (board[length, dropNumber] != 'X' && board[length, dropNumber] != 'O')
                {
                    if (Player.Equals(name1))
                    {
                        board[length, dropNumber] = 'X';
                        turn = 1;
                    }
                    else
                    {
                        board[length, dropNumber] = 'O';
                        turn = 1;
                    }
                }
                else
                    length--;
            } while (turn != 1);
        }
        public int CheckFullBoard(char[,] board)
        {
            int full;
            full = 0;
            for (int i = 1; i <= 7; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    if (board[i, j] != '*')
                        full++;
                }

            }

            return full;
        }
        public static bool RestartGame(char[,] board)
        {
            char r;

            Console.WriteLine("Would you like to replay the game? Yes('Y') No('N'): ");
            r = Console.ReadLine()[0];
            if (r == 'Y' || r == 'y')
            {
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 7; j++)
                    {
                        board[i, j] = '*';
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Thanks for playing Connect 4.");
                return false;
            }
        }
      public static void CheckWin(string Player)
        {
            Console.WriteLine(Player + " You Win Connect 4 game.");
        }
        public virtual void Play(string player1, string player2)
        {
            FourCoins obj1 = new HorizontalCheck();
            FourCoins obj2 = new VerticalCheck();
            FourCoins obj3 = new DiagonalCheck();
            MakeABoard(board);
            int dChoice;bool again=true;
            do
            {
                dChoice = DropANumber(board, player1);
                CheckDropCoin(board, player1, dChoice, player1, player2);
                MakeABoard(board);
               
                if ( obj1.CheckFour(board, player2, player1, player2)==1|| obj2.CheckFour(board, player2, player1, player2) == 1|| obj3.CheckFour(board, player2, player1, player2) == 1)
                {
                   CheckWin (player1);
                    again = RestartGame(board);
                    if (again == true)
                    {
                        break;
                    }
                }

                dChoice = DropANumber(board, player2);
                CheckDropCoin(board, player2, dChoice, player1, player2);
                MakeABoard(board);
                win = obj1.CheckFour(board, player2, player1, player2);
                win = obj2.CheckFour(board, player2, player1, player2);


                if  (obj1.CheckFour(board, player2, player1, player2) == 1 || obj2.CheckFour(board, player2, player1, player2) == 1 || obj3.CheckFour(board, player2, player1, player2) == 1)
                {
                    CheckWin(player2);
                    again = RestartGame(board);
                    if (again == true)
                    {
                        break;
                    }
                }
                full = CheckFullBoard(board);
                if (full == 7)
                {
                    Console.WriteLine("Oops! The board is full, so it is a draw!");
                    again = RestartGame(board);
                }

            } while (again != false);
        }
    }
     public  abstract class FourCoins
    {
        public abstract int CheckFour(char[,] board, string Player, String name1, string name2);
    }
    public class HorizontalCheck : FourCoins
    {
        char symb;
        int w;
        public override int CheckFour(char[,] board, string Player, String name1, string name2)
        {
            if (Player.Equals(name1))
                symb = 'X';
            else
                symb = 'O';
            w = 0;

            for (int i = 8; i >= 1; i--)
            {

                for (int j = 9; j >= 1; j--)
                {
                    if (board[i, j] == symb &&
                        board[i - 1, j] == symb &&
                        board[i - 2, j] == symb &&
                        board[i - 3, j] == symb)
                    {
                        w = 1;
                    }
                    if (board[i, j] == symb &&
                        board[i - 1, j] == symb &&
                        board[i - 2, j] == symb &&
                        board[i - 3, j] == symb)
                    {
                        w = 1;
                    }
                }

            }
            return w;
        }

    }
    public class VerticalCheck : FourCoins
    {
        char symb;
        int w;
        public override int CheckFour(char[,] board, string Player, String name1, string name2)
        {
            if (Player.Equals(name1))
                symb = 'X';
            else
                symb = 'O';
            w = 0;

            for (int i = 8; i >= 1; i--)
            {

                for (int j= 9; j >= 1; j--)
                {
                    if (board[i, j] == symb &&
                         board[i, j - 1] == symb &&
                         board[i, j - 2] == symb &&
                         board[i,j- 3] == symb)
                    {
                        w = 1;
                    }
                    if (board[i, j] == symb &&
                         board[i, j + 1] == symb &&
                         board[i, j + 2] == symb &&
                         board[i, j + 3] == symb)
                    {
                        w = 1;
                    }
                }

            }
            return w;
        }

    }
    public class DiagonalCheck : FourCoins
    {
        char symb;
        int w;
        public override int CheckFour(char[,] board, string Player, String name1, string name2)
        {
            if (Player.Equals(name1))
                symb = 'X';
            else
                symb = 'O';
            w = 0;

            for (int i = 8; i >= 1; i--)
            {

                for (int j = 9; j >= 1; j--)
                {

                    if (board[i, j] == symb &&
                        board[i - 1, j - 1] == symb &&
                        board[i - 2, j - 2] == symb &&
                        board[i - 3, j - 3] == symb)
                    {
                        w = 1;
                    }
                }

            }
            return w;
        }

    }




    internal class Program
    {
        static void Main(string[] args)
        {
            string Name1, Name2;
            char symb1, symb2;
            Console.WriteLine("Welcome to Connect 4");
            Console.WriteLine("First Player: Enter your name");
            Name1 = Console.ReadLine();
            Console.WriteLine("Second Player: Enter your name");
            Name2 = Console.ReadLine();
            symb1 = 'X';
            symb2 = 'O';
            Game ob = new Game(Name1, Name2, symb1, symb2);
            ob.Rules();
            ob.Play(Name1,Name2);

        }     
    }
}
