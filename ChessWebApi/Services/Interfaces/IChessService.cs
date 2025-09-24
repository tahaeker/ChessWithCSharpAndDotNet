using ChessEngine.Core;

namespace ChessWebApi.Services
{
    public interface IChessService
    {
        Task StartNewGameAsync(string nameWhite, string nameBlack);
        Task<(bool,string)> TryMoveAsync(string from, string to);
        Task<IEnumerable<string>> GetBoardAsync();
        Task<IEnumerable<Move>> GetHistoryAsync();
    }
}
