using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSCI3097_HW3.Character
{
  /// <summary>
  /// Will model a character that would
  /// exist on and interact with a level object.
  /// </summary>
  interface Character
  {
    /// <summary>
    /// Will return the texture of this character.
    /// </summary>
    Texture2D Texture();
    
    /// <summary>
    /// Will return the velocity of this charcter
    /// at the moment the method is called.
    /// ENSURE:   this.speed is returned
    /// </summary>
    Vector2 currentSpeed();

    /// <summary>
    /// Will return the current position of this character
    /// in the form of a Vector2 object. The Vector2 represents
    /// ENSURE:   a Vector2 of this character's
    ///            current position is returned.
    /// </summary>
    Vector2 currentPosition();

    /// <summary>
    /// Will return the position from where this character jumped from.
    /// If the character is not in the process of jumping,
    /// the returned Vector2 object state should not be trusted.
    /// REQUIRE:  this character is currently jumping
    /// ENSURE:   a Vector2 of this character's starting point
    ///            of the jump is returned
    /// </summary>
    Vector2 jumpingPoint();


    /// <summary>
    /// Will return the hit box for this character,
    /// designating the rectangular coordinates a "hit" 
    /// has to occur in order to register on this character.
    /// ENSURE:   a Rectangle with X, Y, Width and Height > 0 is returned
    /// </summary>
    Rectangle BoundingBox();

    /// <summary>
    /// Will return whether or not the character is alive.
    /// ENSURE:   if the character is alive,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isAlive();

    /// <summary>
    /// Will return whether or not the character is currently walking.
    /// ENSURE:   if the character is walking,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isWalking();

    /// <summary>
    /// Will return whether or not the character is currently running.
    /// ENSURE:   if the character is running,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isRunning();

    /// <summary>
    /// Will return whether or not the character is currently jumping.
    /// ENSURE:   if the character is jumping,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isJumping();

    /// <summary>
    /// Will return whether or not the character is currently falling.
    /// ENSURE:   if the character is falling,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool isFalling();

    /// <summary>
    /// Will return whether or not this character is grounded
    /// on a surface and not falling or jumping currently.
    /// ENSURE:   if the character has a vertical velocity,
    ///            return true
    ///           otherwise
    ///            return false
    /// </summary>
    bool isGrounded();

    /// <summary>
    /// Will return whether or not the character has reached
    /// the maximum height achievable by jumping.
    /// ENSURE:   if max height has been reached,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool jumpHeightReached();

    /// <summary>
    /// Will kill the character, what else is there to say really?
    /// REQUIRE:  you don't want to use the character anymore
    ///            &&/|| it really deserves to be dead
    /// ENSURE:   this.isAlive() == false
    /// </summary>
    void kill();

    /// <summary>
    /// Will set this character's position to the given 2D coordinate.
    /// REQUIRE:  given position != null
    /// ENSURE:   this character's new position == given position
    /// </summary>
    void setPosition(Vector2 new_position);

    /// <summary>
    /// Will set this character's velocity to the given values.
    /// REQUIRE:  given horizontal and vertical forces != null
    /// ENSURE:   this character's new velocity.X and Y equal the
    ///            given horizontal and vertical forces respectively
    /// </summary>
    void setVelocity(int horizontal, int vertical);

    /// <summary>
    /// Will set this character's velocity to the given vector.
    /// REQUIRE:  given 2D vector != null
    /// ENSURE:   this character's new velocity == given velocity
    /// </summary>
    void setVelocity(Vector2 new_velocity);

    /// <summary>
    /// Will apply a negative horizontal force to this character's
    /// velocity, which will move the character to the left when it moves.
    /// ENSURE:   when this character moves,
    ///            new.position.X is less than old.position.X &&
    ///            new.position.X is greater or equal to
    ///            old.position.X-horizontal velocity
    /// </summary>
    void setLeftMove();

    /// <summary>
    /// Will apply a positive horizontal force to this character's
    /// velocity, which will move the character to the right when it moves.
    /// ENSURE:   when this character moves,
    ///            new.position.X is greater than old.position.X &&
    ///            new.position.X is less or equal to
    ///            old.position.X-horiztonal velocity
    /// </summary>
    void setRightMove();

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
    void moveCharacter();

    /// <summary>
    /// Will make this character walk, setting its current velocity to
    /// whatever its walking velocity is.  While walking, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to it walking velocity
    /// </summary>
    void walk();

    /// <summary>
    /// Will make this character run, setting its current velocity to
    /// whatever its running velocity is.  While running, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to its running velocity
    /// </summary>
    void run();

    /// <summary>
    /// Will make this character jump, giving it a brief positive
    /// veritcal velocity. While jumping, a character cannot jump again.
    /// REQUIRE:  this character must not already be jumping
    /// ENSURE:   this character will be given a vertical velocity to
    ///            simulate jumping into the air.
    /// </summary>
    void jump();

    /// <summary>
    /// Will make this character fall, giving it a positive velocity.
    /// While falling, a character cannot jump if they have already.
    /// REQUIRE:  the character is allowed to pass through the space
    ///            their current position.Y - their fall speed
    /// ENSURE:   this character will be given a positive velocity to
    ///            simulate falling from the air.
    /// </summary>
    void fall();

    /// <summary>
    /// Will set this character's vertical velocity to 0,
    /// neutralizing any vertical forces this character might experience.
    /// ENSURE:   this character will neighter fall nor jump next move
    /// </summary>
    void ground();

    /// <summary>
    /// Will make this character cease vertical and horizontal movement.
    /// ENSURE:   this character's horizontal and vertical velocity will be 0
    /// </summary>
    void resetVelocity();
  }
}
