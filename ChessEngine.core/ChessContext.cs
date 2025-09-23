using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessEngine.Core
{
    public class ChessContext
    {

        public char[,] Board { get; set; }
        public string inputFrom = "";
        public string inputTo = "";
        public char empty = '.';
        public bool whiteTurn = true;


        public Cell touchedCell { get; set; }
        public Cell lastFromCell = new Cell(0, 0, null);
        public Cell lastToCell = new Cell(0, 0, null);


        public bool whiteKingMoved = false;
        public bool blackKingMoved = false;
        public bool whiteQueensideRookMoved = false;
        public bool whiteKingsideRookMoved = false;
        public bool blackQueensideRookMoved = false;
        public bool blackKingsideRookMoved = false;


        public bool IsFakeMovement = false; // for testing sub purposes without stack overflow exception
        public bool IsMoveTouchedMovableControl = false; // for testing sub purposes without stack overflow exception
        public bool IsInputFromValid = true;
        public string InputFromError { get; set; }
        public bool IsInputToValid = true;
        public string InputToError { get; set; }

        public bool whiteWins = false;
        public bool blackWins = false;
        public bool drawStuation = false;
        public bool isGameEnd = false;



        public int howMuchWhitekingMoved = 0;
        public int howMuchBlackkingMoved = 0;



        public List<Move> MoveHistory { get; set; } = new List<Move>();
        public List<char[,]> BoardHistory { get; set; } = new List<char[,]>();

        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }



        public ChessContext()
        {

            Board= new char[8, 8]
                 {
                { 'r','n','b','q','k','b','n','r' },
                { 'p','p','p','p','p','p','p','p' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { 'P','P','P','P','P','P','P','P' },
                { 'R','N','B','Q','K','B','N','R' }
             }; ; // 8x8 satranç tahtası
            
        }

        // Her hamle sonrası çağrılacak yardımcı metot:
        public void SaveBoardToHistory()
        {
            // Board'un derin kopyasını oluştur
            var boardCopy = new char[8, 8];
            Array.Copy(Board, boardCopy, Board.Length);
            BoardHistory.Add(boardCopy);
        }


        public void ResetBoard()
        {
            Board= new char[8, 8]
                {
                { 'r','n','b','q','k','b','n','r' },
                { 'p','p','p','p','p','p','p','p' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { '.','.','.','.','.','.','.','.' },
                { 'P','P','P','P','P','P','P','P' },
                { 'R','N','B','Q','K','B','N','R' }
            };

            string inputFrom = "";
            string inputTo = "";
            char empty = '.';
            bool whiteTurn = true;

            touchedCell = new Cell(0, 0, null);
            lastFromCell = new Cell(0, 0, null);
            lastToCell = new Cell(0, 0, null);


            whiteKingMoved = false;
            blackKingMoved = false;
            whiteQueensideRookMoved = false;
            whiteKingsideRookMoved = false;
            blackQueensideRookMoved = false;
            blackKingsideRookMoved = false;
            WhitePlayer = new Player("Alice", true);
            BlackPlayer = new Player("Bob", false);
            MoveHistory.Clear();
            whiteTurn = true;


        }

    }
}
