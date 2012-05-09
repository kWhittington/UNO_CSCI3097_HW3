using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CSCI3097_HW3.Level;


namespace CSCI3097_HW3.Managers
{
  /// <summary>
  /// This is a game component that implements IUpdateable.
  /// </summary>
  public class LevelManager : Microsoft.Xna.Framework.DrawableGameComponent
  {
    private List<CSCI3097_HW3.Level.Level> levels;
    private Level.Level current_level;

    public LevelManager(Game game, string player_texture_name, Vector2 player_start_position,
      String level_one_tile_sheet, String level_one_sheet)
      : base(game)
    {
      //first open a stream to the level sheet
      Stream level_one_sheet_stream = TitleContainer.OpenStream("Content\\"+level_one_sheet);
      //Stream level_one_sheet_stream = new FileStream((level_one_sheet), FileMode.Open);
      //now open the player texture
      Texture2D player_texture = game.Content.Load<Texture2D>(player_texture_name);
      levels = new List<Level.Level>();
      //add the first level to list
      levels.Add(new Level.Level(game, player_texture, player_start_position, level_one_tile_sheet, level_one_sheet_stream));

      current_level = new Level.Level(game, player_texture, player_start_position, level_one_tile_sheet, level_one_sheet_stream);
    }

    /// <summary>
    /// Will return the current level.
    /// </summary>
    public Level.Level currentLevel()
    {
      return this.current_level;
    }

    /// <summary>
    /// Allows the game component to perform any initialization it needs to before starting
    /// to run.  This is where it can query for any required services and load content.
    /// </summary>
    public override void Initialize()
    {
      Game.Components.Add(this.current_level); 
      base.Initialize();
    }

    protected override void LoadContent()
    {
      
      base.LoadContent();
    }

    /// <summary>
    /// Allows the game component to update itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime)
    {
      
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      SpriteBatch spriteBatch = Game.Services.GetService(
        typeof(SpriteBatch)) as SpriteBatch;

      base.Draw(gameTime);
    }
  }
}
