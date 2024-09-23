using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Interfaces
{
    // Would have texture and position properties
    public interface IBlock
    {
        Vector2 Position { get; set; } // Block position

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        void OnCollide(IPlayer player);

        bool IsBreakable(); // Is block breakable

        void Trigger(); // Trigger block
    }
}
