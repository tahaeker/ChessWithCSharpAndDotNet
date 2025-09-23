using ChessEngine.Core;

namespace ChessWebApi.Services.Interfaces
{
    public interface IChessService
    {
        public void StartNewGame(string nameWhite, string nameBlack);
        public IEnumerable<string> GetBoard();
        public IEnumerable<Move> GetHistory();
    }
}
