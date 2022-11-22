using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;
        private bool player1lose = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
             
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Snake snake1 = (Snake)cast.GetFirstActor("snake1");
            Snake snake2 = (Snake)cast.GetFirstActor("snake2");
            Actor head1 = snake1.GetHead();
            Actor head2 = snake2.GetHead();
            List<Actor> body1 = snake1.GetBody();
            List<Actor> body2 = snake2.GetBody();

            foreach (Actor segment1 in body1)
            {
                if (segment1.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    player1lose = true;
                }
                if (segment1.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    player1lose = false;
                }
            }
            foreach (Actor segment2 in body2)
            {
                if (segment2.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    player1lose = false;
                }
                if (segment2.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    player1lose = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Snake snake1 = (Snake)cast.GetFirstActor("snake1");
                Snake snake2 = (Snake)cast.GetFirstActor("snake2");
                List<Actor> segments1 = snake1.GetSegments();
                List<Actor> segments2 = snake2.GetSegments();
                Food food = (Food)cast.GetFirstActor("food");
                snake1.Color = Constants.WHITE;
                snake2.Color = Constants.WHITE;

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                if (player1lose == true){
                    message.SetText("Game Over! Player 2 wins!!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);
                }
                else{
                    message.SetText("Game Over! Player 1 wins!!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);
                }
                // make everything white
                foreach (Actor segment in segments1)
                {
                    segment.SetColor(Constants.WHITE);
                }
                food.SetColor(Constants.BLACK);
                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                }
                food.SetColor(Constants.BLACK);
            }
        }

    }
}