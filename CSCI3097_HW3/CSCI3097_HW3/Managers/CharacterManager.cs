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
    //forgotten what this was here for, might not be needed
    //private Vector2 player_old_position;
    private const float player_walk_speed = 4;
    private const float player_run_speed = 8;
    private const float player_jump_speed = 10;
    private const float player_jump_height = 11;
    //here will be the rest of the enemy characters
    private LinkedList<Enemy> enemies;
    #endregion INSTANCE VARIABLES

    public CharacterManager(Game game, Texture2D player_texture, Vector2 player_position)
      : base(game)
    {
      // TODO: Construct any child components here
      this.player_character = new Player(player_texture, player_position, player_walk_speed,
        player_run_speed, player_jump_speed, player_jump_height);
      //this.player_old_position = new Vector2(this.player_character.BoundingBox().Location.X,
        //this.player_character.BoundingBox().Location.Y);
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
      //this.checkForFalling(this.player_character);

      this.player_character.moveCharacter();

      base.Update(gameTime);
    }

    /// <summary>
    /// Will check if the given character will hit ground,
    /// if so, their position will be set accordingly and grounded.
    /// REQUIRE:  the given character != null
    /// ENSURE:   if the character will hit ground,
    ///            set their position and ground them.
    /// </summary>
    private void checkForGround(Character.Character character)
    {

    }

    /// <summary>
    /// Will check if the given character is grounded,
    /// and make them fall if necessary.
    /// REQUIRE:  The given character != null
    /// ENSURE:   if the given character is not grounded,
    ///            the character will be told to fall
    ///           otherwise,
    ///            they are left alone
    /// </summary>
    private void checkForFalling(Character.Character character)
    {
      //if the character is falling
      if (character.isGrounded() == false)
      {
        //if not, tell the character to fall
        character.fall();
      }
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
        this.player_character.setRightMove();
      }

      //if the player is jumping
      if (this.player_character.isJumping() == true)
      {
        //if the player has reached the max jumping height
        if (this.player_character.jumpHeightReached())
        {
          //then begin their decent
          this.player_character.fall();
        }
        //otherwise, the player is still jumping
        else
        {
          this.player_character.jump();
        }
      }
      //otherwise, the player is not jumping
      else
      {
        //if the space bar has been pressed
        if (keyboard.IsKeyDown(Keys.Space))
        {
          //make the player jump
          this.player_character.jump();
        }
      }
    }

    #endregion COMMANDS
  }
}
