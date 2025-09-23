using ChessWebApi.Services;
using ChessWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace ChessWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessController : ControllerBase

    {
        private readonly IChessService _svc;
        public ChessController(ChessService chessService)
        {
            _svc = chessService;
        }

        [HttpPost("newgame")]
        public IActionResult NewGame([FromQuery] string WhiteName, [FromQuery] string BlackName)
        {
             _svc.StartNewGame(WhiteName, BlackName);
            return Ok(new { message = "New game Started!" });
        }




        [HttpGet("board")]
        public IActionResult GetBoard()
        {
            var board = _svc.GetBoard();
            return Ok(board);
        }

        [HttpPost("move")]
        public IActionResult MakeMove([FromQuery] string from, [FromQuery] string to)
        {
            if (_svc.TryMove(from, to, out string message))
                return Ok(new { success = true, message });

            return BadRequest(new { success = false, message });

        }

        [HttpGet("History")]
        public IActionResult GetHistory()
        {
            var history = _svc.GetHistory();
            return Ok(history);
        }







    }
}

