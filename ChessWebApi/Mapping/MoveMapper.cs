using ChessEngine.Core;
using ChessWebApi.Models;
using System.Runtime.CompilerServices;

namespace ChessWebApi.Mapping
{
    public static class MoveMapper
    {
        //entity to Domain 
        public static Move ToDomain(this MoveEntity entity)
        {
            return new Move
            {
                From = entity.From,
                To = entity.To,
                Piece = entity.Piece,
                Captured = entity.Captured,
                TurnNumber = entity.TurnNumber,
                IsWhiteTurn = entity.IsWhiteTurn,



            };
        }

        //domain to entity
        public static MoveEntity ToEntity(this Move move, int gameId)
        {
            return new MoveEntity
            {
                From = move.From,
                To = move.To,
                Piece = move.Piece,
                Captured = move.Captured,
                TurnNumber = move.TurnNumber,
                IsWhiteTurn = move.IsWhiteTurn,

                GameId = gameId,
                CreatedAt = DateTime.UtcNow,
            };
        }


    }
}
