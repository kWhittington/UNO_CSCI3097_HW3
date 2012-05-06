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
    protected float fall_velocity;
    protected bool is_alive;
    protected bool is_running;
    protected bool is_jumping;
    protected bool is_falling;
    protected Vector2 position;
    protected Vector2 jumping_point;

    /////////////////////////////////////////////////////////////////
    #region QUERIES

    /// <summary>
    /// Will return the texture of this character.
    /// </summary>
    public Texture2D Texture()
    {
      return this.texture;
    }

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
    /// Will return the current position of this character
    /// in the form of a Vector2 object. The Vector2 represents
    /// ENSURE:   a Vector2 of this character's
    ///            current position is returned.
    /// </summary>
    public Vector2 currentPosition()
    {
      return this.position;
    }

    /// <summary>
    /// Will return the position from where this character jumped from.
    /// If the character is not in the process of jumping,
    /// the returned Vector2 object state should not be trusted.
    /// REQUIRE:  this character is currently jumping
    /// ENSURE:   a Vector2 of this character's starting point
    ///            of the jump is returned
    /// </summary>
    public Vector2 jumpingPoint()
    {
      return this.jumping_point;
    }

    /// <summary>
    /// Will return whether or not the character is alive.
    /// ENSURE:   if the character is alive,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool isAlive()
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

    /// <summary>
    /// Will return whether or not this character is grounded
    /// on a surface and not falling or jumping currently.
    /// ENSURE:   if the character has a vertical velocity,
    ///            return true
    ///           otherwise
    ///            return false
    /// </summary>
    public bool isGrounded()
    {
      //assume the character is grounded
      bool result = true;

      //if the character is jumping or falling
      if (this.is_jumping == true || this.is_falling)
      {
        //it can't be grounded
        result = false;
      }

      return result;
    }

    #endregion QUERIES
    /////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////
    #region COMMANDS

    /// <summary>
    /// Will kill the character, what else is there to say really?
    /// REQUIRE:  you don't want to use the character anymore
    ///            &&/|| it really deserves to be dead
    /// ENSURE:   this.isAlive() == false
    /// </summary>
    public void kill()
    {
      this.is_alive = false;
    }

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
    public void setLeftMove()
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
    public void setRightMove()
    {
      this.position.X = this.position.X + this.current_velocity.X;
    }

    /// <summary>
    /// Will move this character's position based on the current
    /// velocity.  If the character has a negative horizontal velocity,
    /// the character will move to the left.  If it is positive, the
    /// character will move to the right.  If the character has a
    /// negative vertical velocity, the characte will move up.  If
    /// it is positive, the character will move down.
    /// ENSURE:   if current velocity.X is negative
    ///            character moves left
    ///           or if current velocity.X is positive
    ///            character moves right
    ///           if current velocity.Y is negative
    ///            character moves up
    ///           or if current velocity.Y is positive
    ///            character moves down
    /// </summary>
    public void moveCharacter()
    {
      //apply any stored horizontal forces to the character
      this.position.X = this.position.X + this.current_velocity.X;
      //apply any stored vertical forces to the character
      this.position.Y = this.position.Y + this.current_velocity.Y;
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
    /// Will make this character jump, giving it a brief veritcal velocity.
    /// While jumping, a character cannot jump again.
    /// REQUIRE:  this character must not already be jumping
    /// ENSURE:   this character will be givin a vertical velocity to
    ///            simulate jumping into the air.
    /// </summary>
    public void jump()
    {
      //make sure the fall flag is off
      this.is_falling = false;
      //turn the flag on
      this.is_jumping = true;
      //save the current jumping point
      this.jumping_point = this.position;
      //apply a negative force to Y (up is negative, down positive)
      this.current_velocity.Y = (0 - this.jump_velocity);
    }

    /// <summary>
    /// Will make this character fall, giving it a positive velocity.
    /// While falling, a character cannot jump if they have already.
    /// REQUIRE:  the character is allowed to pass through the space
    ///            their current position.Y - their fall speed
    /// ENSURE:   this character will be given a positive velocity to
    ///            simulate falling from the air.
    /// </summary>
    public void fall()
    {
      //make sure the jump flag is off
      this.is_jumping = false;
      //turn the fall flag on
      this.is_falling = true;
      //apply a positive force to Y (up is negative, down positive)
      this.current_velocity.Y = (0 + this.fall_velocity);
    }

    /// <summary>
    /// Will set this character's vertical velocity to 0,
    /// neutralizing any vertical forces this character might experience.
    /// ENSURE:   this character will neighter fall nor jump next move
    /// </summary>
    public void ground()
    {
      //turn the fall flag off
      this.is_falling = false;
      //turn the jump flag off
      this.is_jumping = false;
      //reset the vertical velocity, set it to 0,
      //neutralizing all vertical forces
      this.current_velocity.Y = 0;
    }

    /// <summary>
    /// Will make this character cease vertical and horizontal movement.
    /// ENSURE:   this character's horizontal and vertical velocity will be 0
    /// </summary>
    public void resetVelocity()
    {
    }

    #endregion COMMANDS
    /////////////////////////////////////////////////////////////////
  }
}
