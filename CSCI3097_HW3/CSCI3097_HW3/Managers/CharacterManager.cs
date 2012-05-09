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
    private const float player_jump_height = 100;
    private const float enemy_walk_speed = 1;
    private const float enemy_run_speed = 3;
    private const float enemy_jump_speed = 5;
    private const float enemy_jump_height = 50;
    private const float enemy_sight_range = 50;
    //here will be the rest of the enemy characters
    private LinkedList<Enemy> enemies;
    #endregion INSTANCE VARIABLES

    public CharacterManager(Game game, Texture2D player_texture, Vector2 player_position)
      : base(game)
    {
      // TODO: Construct any child components here
      this.player_character = new Player(player_texture, player_position, player_walk_speed,
        player_run_speed, player_jump_speed, player_jump_height);
      this.player_character.fall();
      //this.player_old_position = new Vector2(this.player_character.BoundingBox().Location.X,
        //this.player_character.BoundingBox().Location.Y);
      this.enemies = new LinkedList<Enemy>();
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
    /// Will return the current listing of enemies.
    /// ENSURE:   returns this.enemies
    /// </summary>
    public LinkedList<Enemy> enemyList()
    {
      return this.enemies;
    }

    /// <summary>
    /// Will return whether or not the player character is alive.
    /// ENSURE:   returns this.player_character.isAlive()
    /// </summary>
    public bool isPlayerAlive()
    {
      return this.player_character.isAlive();
    }

    /// <summary>
    /// Will return a list of all characters being managed by this manager.
    /// ENSURE:   a list of all characters in this manager is returned
    /// </summary>
    public List<Character.Character> characters()
    {
      List<Character.Character> result = new List<Character.Character>();
      //add the player character to the list
      result.Add(this.player_character);
      //now add each enemy character
      foreach (Enemy enemy in this.enemies)
      {
        result.Add(enemy);
      }
      return result;
    }

    /// <summary>
    /// Will return a list of all alive characters
    /// being managed by this manager.
    /// ENSURE:   a list of all alive characters in this manager is returned
    /// </summary>
    public List<Character.Character> aliveCharacters()
    {
      List<Character.Character> result = new List<Character.Character>();
      //for every character in this manager
      foreach (Character.Character character in this.characters())
      {
        //if they are alive
        if (character.isAlive() == true)
        {
          //add them to the result
          result.Add(character);
        }
      }
      return result;
    }

    #endregion QUERIES

    #region COMMANDS
    /// <summary>
    /// Will create and add a new enemy character with the given texture.
    /// REQUIRE:  given texture != null
    /// ENSURE:   this.enemies.contains(new enemy)
    /// </summary>
    public void addEnemy(Texture2D texture, Vector2 start_position, Character.AI.AI personality,
      int patrol_distance, int patrol_direction, bool is_patrolling)
    {
      //create the new enemy
      Enemy new_enemy = new Enemy(texture, start_position, enemy_walk_speed,
        enemy_run_speed, enemy_jump_speed, enemy_jump_height, enemy_sight_range,
        personality);
      new_enemy.fall();
      new_enemy.Personality().setPatrolDistance(patrol_distance);
      new_enemy.Personality().setPatrolDirection(patrol_direction);
      //if the unit is patrolling, tell it such
      if (is_patrolling == true) { new_enemy.Personality().patrol(); }

      //now add it to the enemy list
      this.enemies.AddFirst(new_enemy);
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
      KeyboardState keyboard = Keyboard.GetState();
      GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
      this.updatePlayer(keyboard, gamepad);
      foreach (Enemy enemy in this.enemies)
      {
        this.updateEnemy(enemy);
      }

      base.Update(gameTime);
    }

    /// <summary>
    /// Will update the given character.
    /// </summary>
    private void updateCharacter(Character.Character character)
    {
      //if the player is jumping
      if (character.isJumping() == true)
      {
        //if the player has reached the max jumping height
        if (character.jumpHeightReached())
        {
          //then begin their decent
          character.fall();
        }
        //otherwise, the player is still jumping
        else
        {
          character.jump();
        }
      }
      //or if the player is falling
      else if (character.isFalling() == true)
      {
        //make them fall
        character.fall();
      }
    }

    /// <summary>
    /// Will update the given enemy character.
    /// </summary>
    private void updateEnemy(Character.Enemy enemy)
    {
      //if the character is jumping right
      if (enemy.isJumpingRight())
      {
        enemy.setRightMove();
      }
      //or if the character is jumping left
      else if (enemy.isJumpingLeft())
      {
        enemy.setLeftMove();
      }
      //if the player is jumping
      if (enemy.isJumping() == true)
      {
        //if the player has reached the max jumping height
        if (enemy.jumpHeightReached())
        {
          //then begin their decent
          enemy.fall();
        }
        //otherwise, the player is still jumping
        else
        {
          enemy.jump();
        }
      }
      //or if the player is falling
      else if (enemy.isFalling() == true)
      {
        //make them fall
        enemy.fall();
      }

      //for character alive right now
      foreach (Character.Character character in this.aliveCharacters())
      {
        //if they are within view distance of this enemy
        if (character.Equals(enemy) == false && enemy.canSee(character) == true)
        {
          //then update this enemy accordingly
          enemy.react(character);
        }
      }
      enemy.Personality().update();
    }

    /// <summary>
    /// Will update the player based on input from the keyboard.
    /// </summary>
    private void updatePlayer(KeyboardState keyboard, GamePadState gamepad)
    {
      //if the left arrow key has been pressed
      if (keyboard.IsKeyDown(Keys.Left) || gamepad.IsButtonDown(Buttons.LeftThumbstickLeft))
      {
        //move the player left
        this.player_character.setLeftMove();
      }
      //if the right arrow key has been pressed
      if (keyboard.IsKeyDown(Keys.Right) || gamepad.IsButtonDown(Buttons.LeftThumbstickRight))
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
      //or if the player is falling
      else if (this.player_character.isFalling() == true)
      {
        //make them fall
        this.player_character.fall();
      }
      else
      {
        //if the space bar has been pressed
        if (keyboard.IsKeyDown(Keys.Space) || gamepad.IsButtonDown(Buttons.A))
        {
          //make the player jump
          this.player_character.jump();
        }
      }
    }

    #endregion COMMANDS
  }
}
