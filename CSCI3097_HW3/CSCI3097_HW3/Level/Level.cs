using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CSCI3097_HW3.Level
{
  /// <summary>
  /// This is a game component that implements IUpdateable.
  /// </summary>
  public class Level : Microsoft.Xna.Framework.DrawableGameComponent
  {
    //INSTANCE VARIABLES
    private Managers.CharacterManager character_manager;

    public Level(Game game)
      : base(game)
    {
      //give the character manager a player skin and location
      this.character_manager = new Managers.CharacterManager();
    }

    /// <summary>
    /// Allows the game component to perform any initialization it needs to before starting
    /// to run.  This is where it can query for any required services and load content.
    /// </summary>
    public override void Initialize()
    {
      // TODO: Add your initialization code here

      base.Initialize();
    }

    /// <summary>
    /// Allows the game component to update itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime)
    {
      // TODO: Add your update code here

      base.Update(gameTime);
    }

    /// <summary>
    /// Allows the game component to draw itself.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {

      
      base.Draw(gameTime);
    }
  }
}
