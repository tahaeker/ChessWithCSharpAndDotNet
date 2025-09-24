using ChessWebApi.Services;
using Microsoft.AspNetCore.Mvc;



namespace ChessWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessController : ControllerBase

    {
        private readonly IChessService _svc;
        public ChessController(IChessService chessService)
        {
            _svc = chessService;
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> NewGame([FromQuery] string WhiteName, [FromQuery] string BlackName)
        {
            await _svc.StartNewGameAsync(WhiteName, BlackName);
            return Ok(new { message = "New game Started!" });
        }




        [HttpGet("board")]
        public async Task<IActionResult> GetBoard()
        {
            var board = await _svc.GetBoardAsync();
            return Ok(board);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MakeMove([FromQuery] string from, [FromQuery] string to)
        {
            var (success, message) = await _svc.TryMoveAsync(from, to);
            if (success)
                return Ok(new { success = true, message });

            return BadRequest(new { success = false, message });

        }

        [HttpGet("History")]
        public async Task<IActionResult> GetHistory()
        {

            var history = await _svc.GetHistoryAsync();
            return Ok(history);
        }







    }
}

