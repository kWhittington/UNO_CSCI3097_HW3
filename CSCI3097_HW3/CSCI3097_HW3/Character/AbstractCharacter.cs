using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSCI3097_HW3.Character
{
  /// <summary>
  /// Will contain and implement all common functionality
  /// between the separte concrete instantiations of a character.
  /// </summary>
  abstract class AbstractCharacter : Character
  {
    //INSTANCE VARIABLES
    protected Texture2D texture;
    protected Vector2 current_velocity;
    protected float walk_velocity;
    protected float run_velocity;
    protected float jump_velocity;
    protected bool is_alive;
    protected bool is_jumping;
    protected bool is_running;
    protected Vector2 position;

    /////////////////////////////////////////////////////////////////
    #region QUERIES

    /// <summary>
    /// Will return the hit box for this character,
    /// designating the rectangular coordinates a "hit" 
    /// has to occur in order to register on this character.
    /// ENSURE:   a Rectangle with X, Y, Width and Height > 0 is returned
    /// </summary>
    public Rectangle BoundingBox()
    {
      //return a new rectagle based on this character's
      //current position and width and height
      return new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
    }

    /// <summary>
    /// Will return the velocity of this charcter
    /// at the moment the method is called.
    /// ENSURE:   this.speed is returned
    /// </summary>
    public Vector2 currentSpeed()
    {
      return this.current_velocity;
    }

    /// <summary>
    /// Will return whether or not the character is alive.
    /// ENSURE:   if the character is alive,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isAlive()
    {
      return this.is_alive;
    }

    /// <summary>
    /// Will return whether or not the character is currently walking.
    /// ENSURE:   if the character is walking,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool isWalking()
    {
      //return the opposite of is_running
      return this.is_running.Equals(false);
    }

    /// <summary>
    /// Will return whether or not the character is currently running.
    /// ENSURE:   if the character is running,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool isRunning()
    {
      //return is_running
      return this.is_running;
    }

    /// <summary>
    /// Will return whether or not the character is currently jumping.
    /// ENSURE:   if the character is jumping,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool isJumping()
    {
      //return is_jumping
      return this.is_jumping;
    }

    #endregion QUERIES
    /////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////
    #region COMMANDS

    /// <summary>
    /// Will move this character to the left of their current position.
    /// REQUIRE:  there must not be any collidable objects between
    ///            the character and space to the left up to the 
    ///            character's horizontal velocity.
    /// ENSURE:   this character's new position changes &&
    ///            new.position.X is less than old.position.X &&
    ///            new.position.X is greater or equal to
    ///            old.position.X-horizontal velocity
    /// </summary>
    public void moveLeft()
    {
      this.position.X = this.position.X - this.current_velocity.X;
    }

    /// <summary>
    /// Will move this character to the right of their current position.
    /// REQUIRE:  there must not be any collidable objects between
    ///            the character and space to the right up to the
    ///            character's horizontal velocity.
    /// ENSURE:   this character's new position changes &&
    ///            new.position.X is greater than old.position.X &&
    ///            new.position.X is less or equal to
    ///            old.position.X-horiztonal velocity
    /// </summary>
    public void moveRight()
    {
      this.position.X = this.position.X + this.current_velocity.X;
    }

    /// <summary>
    /// Will make this character jump, giving it a brief veritcal velocity.
    /// While jumping, a character cannot jump again.
    /// REQUIRE:  this character must not already be jumping
    /// ENSURE:   this character will be givin a vertical velocity to
    ///            simulate jumping into the air.
    /// </summary>
    public void jump()
    {
      //turn the flag on
      this.is_jumping = true;
      //apply a negative force to Y (up is negative, down positive)
      this.current_velocity.Y = (0 - this.jump_velocity);
    }

    /// <summary>
    /// Will make this character run, setting its current velocity to
    /// whatever its running velocity is.  While running, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to its running velocity
    /// </summary>
    public void run()
    {
      //turn the flag on
      this.is_running = true;
      //set the current horizontal velocity to the running velocity
      this.current_velocity.X = this.run_velocity;
    }

    /// <summary>
    /// Will make this character walk, setting its current velocity to
    /// whatever its walking velocity is.  While walking, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to it walking velocity
    /// </summary>
    public void walk()
    {
      //turn the flag off
      this.is_running = false;
      //set the current horizontal velocity to the walking velocity
      this.current_velocity.X = this.walk_velocity;
    }

    #endregion COMMANDS
    /////////////////////////////////////////////////////////////////
  }
}
