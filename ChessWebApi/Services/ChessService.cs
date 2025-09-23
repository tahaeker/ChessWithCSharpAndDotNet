using ChessEngine.Core;
using ChessWebApi.Data;
using ChessWebApi.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System;


namespace ChessWebApi.Services
{
    public class ChessService : IChessService
    {
        private readonly ChessContext _context;
        private readonly ChessDbContext _db;

        public ChessService(ChessDbContext db)
        {
            _context = new ChessContext();
            _db = db;

        }

        private int _currentGameId;
        public void StartNewGame(string nameWhite, string nameBlack)
        {
            _context.ResetBoard();
            takePlayerName(nameWhite, nameBlack);
            var game = new Game()
            {
                Id = _currentGameId++,
                whiteName = nameWhite,
                blackName = nameBlack,
                Status = "ongoing",
                CreatedAt = DateTime.Now,
                Moves = new List<Move>()

            };
            _db.Games.Add(game);
            _db.SaveChanges();
        }

        
        

        public void takePlayerName(string nameWhite,string nameBlack)
        {
            _context.WhitePlayer = new Player(nameWhite, true);
            _context.BlackPlayer = new Player(nameBlack, false);
        }





        public bool TryMove(string from, string to, out string message)
        {
            var result = ChessEngine.Core.ChessEngine.TryMove(from, to, _context);

            if (!result.Success)
            {
                message = result.ErrorMessage;
                return false;
            }
            else
            {
                var move = BoardConverter.stringToMove(from, to, _context);
                move.Id = _currentGameId ; 
               
                _db.Moves.Add(move);
                _db.SaveChanges();
               
                
                message = "Move successful!";

                return true;
            }
                
                
        }

        public IEnumerable<string> GetBoard()
        {
            
            var rows = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                string row = "";
                for (int j = 0; j < 8; j++)
                {
                    row += _context.Board[i, j] + " ";
                }
                rows.Add(row.Trim());
            }
            return rows;
        }

        public IEnumerable<Move> GetHistory()
        {
            return _context.MoveHistory;
        }

        
    }
}
