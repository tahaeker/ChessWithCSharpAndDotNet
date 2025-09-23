using chess.Interfaces;
using ChessEngine.Core;


namespace chess
{
    public class InputHandler
    {
        public static void TakeFrom(ChessContext ctx, IInputProvider inputProvider)
        {
            string errorMessage;
            do
            {
                Console.WriteLine("Hangi taşı oynatmak istiyorsunuz? ");
                ctx.inputFrom = inputProvider.ReadLine();
                ctx.inputFrom = ctx.inputFrom.ToLower();
                MoveValidator.IsValidFromToCondition(ctx.inputFrom, ctx.inputTo, ctx);
                errorMessage = ctx.InputFromError;
                
                bool IstouchedMovable = MoveValidator.IsTouchedCellMovable(ctx.inputFrom, ctx);
               
                
                var (isFromValid, _) = MoveValidator.IsValidFromToCondition(ctx.inputFrom, null, ctx);
                if (!isFromValid )
                {
                    errorMessage = ctx.InputFromError;
                    Console.WriteLine(errorMessage);
                }
                else if (!IstouchedMovable)
                {
                    errorMessage = "The selected piece cannot be moved! Please select another piece.";
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    errorMessage = "";
                    ctx.touchedCell = BoardConverter.StringToCell(ctx.inputFrom, ctx);
                }
                Console.WriteLine();
            } while (errorMessage != "");
        }

        public static void TakeTo(ChessContext ctx, IInputProvider inputProvider)
        {
            string errorMessage;
            
            do
            {
                Console.WriteLine("Nereye taşımak istiyorsunuz? ");
                ctx.inputTo = inputProvider.ReadLine();
                ctx.inputTo = ctx.inputTo.ToLower();
                MoveValidator.IsValidFromToCondition(ctx.inputFrom, ctx.inputTo, ctx);
                errorMessage = ctx.InputToError;

                var (_, isToValid) = MoveValidator.IsValidFromToCondition(ctx.inputFrom, ctx.inputTo, ctx);
                if (!isToValid)
                {
                    errorMessage = ctx.InputToError;
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    errorMessage = "";
                }
                Console.WriteLine();
            } while (errorMessage != "");
        }

        
    }
}
