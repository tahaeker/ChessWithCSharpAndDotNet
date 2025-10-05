namespace ChessWebApi.Models
{
    public class MoveEntity
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public char Piece { get; set; }
        public char Captured { get; set; }
        public int TurnNumber { get; set; }
        public bool IsWhiteTurn { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;






    }
}
