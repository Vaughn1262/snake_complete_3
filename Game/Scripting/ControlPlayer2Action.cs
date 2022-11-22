using Unit05.Game.Casting;
using Unit05.Game.Services;

namespace Unit05.Game.Scripting
{
    public class ControlPlayer2Action : Action
    {   
        private KeyboardService keyboardService;
        private Point direction = new Point(Constants.CELL_SIZE, 0);

        public ControlPlayer2Action(KeyboardService keyboardService)
        {
            this.keyboardService = keyboardService;
        }
        public void Execute(Cast cast, Script script){
            if (keyboardService.IsKeyDown("j"))
            {
                direction = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (keyboardService.IsKeyDown("l"))
            {
                direction = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (keyboardService.IsKeyDown("i"))
            {
                direction = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (keyboardService.IsKeyDown("k"))
            {
                direction = new Point(0, Constants.CELL_SIZE);
            }

            Snake snake = (Snake)cast.GetFirstActor("snake2");
            snake.TurnHead(direction);
        }
    }
}