﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Interfaces;

public interface IItem
{
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
    public Rectangle getDestinationRectangle();
    public string getName();
    public void moveY(int y);
    public void OnCollide();
    public bool HasCollision();
    public float GetLifeTime();
}

