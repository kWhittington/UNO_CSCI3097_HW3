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
using CSCI3097_HW3.Character;


namespace CSCI3097_HW3.Managers
{
  /// <summary>
  /// This will model a manager for seperate characters on a level.
  /// It will handle all interactions between them, each other and/or
  /// the level's contents.  It will get input from the user to
  /// determine the player character's actions.
  /// </summary>
  class CharacterManager : Microsoft.Xna.Framework.GameComponent
  {
    #region INSTANCE VARIABLES
    //there will only be one player character allowed for now
    private Player player_character;
    private Vector2 player_old_position;
    private const float player_walk_speed = 4;
    private const float player_run_speed = 8;
    private const float player_jump_speed = 10;
    //here will be the rest of the enemy characters
    private LinkedList<Enemy> enemies;
    #endregion INSTANCE VARIABLES

    public CharacterManager(Game game, Texture2D player_texture, Vector2 player_position)
      : base(game)
    {
      // TODO: Construct any child components here
      this.player_character = new Player(player_texture, player_position, player_walk_speed,
        player_run_speed, player_jump_speed);
      this.player_old_position = new Vector2(this.player_character.BoundingBox().Location.X,
        this.player_character.BoundingBox().Location.Y);
    }

    #region QUERIES

    /// <summary>
    /// Will return the current state of the player character.
    /// ENSURE:   returns this.player_character
    /// </summary>
    public Player playerCharacter()
    {
      return this.player_character;
    }

    /// <summary>
    /// Will return whether or not the player character is alive.
    /// ENSURE:   returns this.player_character.isAlive()
    /// </summary>
    public bool isPlayerAlive()
    {
      return this.player_character.isAlive();
    }

    #endregion QUERIES

    #region COMMANDS
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
      KeyboardState keyboard = Keyboard.GetState();
      this.updatePlayer(keyboard);
      base.Update(gameTime);
    }

    /// <summary>
    /// Will update the player based on input from the keyboard.
    /// </summary>
    private void updatePlayer(KeyboardState keyboard)
    {
      //if the left arrow key has been pressed
      if (keyboard.IsKeyDown(Keys.Left))
      {
        //move the player left
        this.player_character.setLeftMove();
      }
      //if the right arrow key has been pressed
      if (keyboard.IsKeyDown(Keys.Right))
      {
        //move the player right
        this.player_character.setRight();
      }
      //if the space bar has been pressed
      if (keyboard.IsKeyDown(Keys.Space))
      {
        //if the player isn't already jumping
        if (this.player_character.isJumping())
        {
          //make the player jump
          this.player_character.jump();
        }
      }
    }

    #endregion COMMANDS
  }
}
