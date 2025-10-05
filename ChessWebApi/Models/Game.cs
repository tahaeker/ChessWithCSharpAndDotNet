using ChessEngine.Core;
using System.ComponentModel.DataAnnotations;



namespace ChessWebApi.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }// Primary key
        public string whiteName { get; set; }
        public string blackName { get; set; }

        public DateTime CreatedAt { get; set; } // Oyun oluşturulma tarihi
        public string Status { get; set; } // ongoing, white_won, black_won, draw

        public List<MoveEntity> Moves { get; set; } = new ();// Hamleleri tutan liste
    }
}
