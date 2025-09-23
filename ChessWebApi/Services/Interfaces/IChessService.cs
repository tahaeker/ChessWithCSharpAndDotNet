using ChessEngine.Core;

namespace ChessWebApi.Services
{
    public interface IChessService
    {
        public void StartNewGame(string nameWhite, string nameBlack);
        public bool TryMove(string from, string to, out string message);
        public IEnumerable<string> GetBoard();
        public IEnumerable<Move> GetHistory();
    }
}
