using ChessEngine.Core;
using ChessWebApi.Data;
using ChessWebApi.Models;
using ChessWebApi.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System;
using System.Linq;


namespace ChessWebApi.Services
{
    public class ChessService : IChessService
    {
        private readonly ChessContext _context;
        private readonly ChessDbContext _db;
        private int _currentGameId;

        public ChessService(ChessDbContext db, ChessContext context)
        {
            _context = context;
            _db = db;

            try
            {
                _currentGameId = _db.Games.Any() ? _db.Games.Max(g => g.Id) : 0;
            }
            catch
            {
                _currentGameId = 0; 
            }

        }

        public async Task StartNewGameAsync(string nameWhite, string nameBlack)
        {
            _context.ResetBoard();
            takePlayerName(nameWhite, nameBlack);
            var game = new Game()
            {
                
                whiteName = nameWhite,
                blackName = nameBlack,
                Status = "ongoing",
                CreatedAt = DateTime.Now,
                Moves = new List<MoveEntity>()

            };
            await _db.Games.AddAsync(game);
            await _db.SaveChangesAsync();
            _currentGameId = game.Id;
            
                
        }

        
        

        public void takePlayerName(string nameWhite,string nameBlack)
        {
            _context.WhitePlayer = new Player(nameWhite, true);
            _context.BlackPlayer = new Player(nameBlack, false);
        }





        public async Task<(bool,string)> TryMoveAsync(string from, string to)
        {
            string message;
            var result = ChessEngine.Core.ChessEngine.TryMove(from, to, _context);

            if (!result.Success)
            {
                message = result.ErrorMessage;
                return (false,message);
            }
            else
            {
                var move = BoardConverter.stringToMove(from, to, _context);
                var moveEntity = move.ToEntity(_currentGameId);
                

                await _db.Moves.AddAsync(moveEntity);
                await _db.SaveChangesAsync();
               
                
                message = "Move successful!";

                return  (true,message);
            }
                
                
        }

        public async Task<IEnumerable<string>> GetBoardAsync()
        {
            if (_context.Board == null)
            {
                _context.ResetBoard();
            }

            var rows = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                string row = "";
                for (int j = 0; j < 8; j++)
                {
                    row += _context.Board[i,j] + " ";
                }
                rows.Add(row.Trim());
            }
            return await Task.FromResult(rows);
        }

        public async Task<IEnumerable<Move>> GetHistoryAsync()
        {
            if (_currentGameId >0)
            {
                var entity = await _db.Moves
                    .Where(m => m.GameId == _currentGameId)
                    .OrderBy(m => m.CreatedAt)
                    .ToListAsync();
                return entity.Select(e => e.ToDomain()).ToList();

            }

            return _context.MoveHistory;
        }
        // 

        public async Task SaveMoveAsync(Move move)
        {
            var entity = move.ToEntity(_currentGameId);
            _db.Moves.Add(entity);
            await _db.SaveChangesAsync();
        }


    }
}
