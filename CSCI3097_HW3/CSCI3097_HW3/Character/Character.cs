using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CSCI3097_HW3.Character
{
  /// <summary>
  /// Will model a character that would
  /// exist on and interact with a level object.
  /// </summary>
  interface Character
  {
    /// <summary>
    /// Will return the velocity of this charcter
    /// at the moment the method is called.
    /// ENSURE:   this.speed is returned
    /// </summary>
    Vector2 currentSpeed();

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
    /// Will move this character to the left of their current position.
    /// REQUIRE:  there must not be any collidable objects between
    ///            the character and space to the left up to the 
    ///            character's horizontal velocity.
    /// ENSURE:   this character's new position changes &&
    ///            new.position.X is less than old.position.X &&
    ///            new.position.X is greater or equal to
    ///            old.position.X-horizontal velocity
    /// </summary>
    void moveLeft();

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
    void moveRight();

    /// <summary>
    /// Will make this character jump, giving it a brief veritcal velocity.
    /// While jumping, a character cannot jump again.
    /// REQUIRE:  this character must not already be jumping
    /// ENSURE:   this character will be givin a vertical velocity to
    ///            simulate jumping into the air.
    /// </summary>
    void jump();

    /// <summary>
    /// Will make this character run, setting its current velocity to
    /// whatever its running velocity is.  While running, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to its running velocity
    /// </summary>
    void run();

    /// <summary>
    /// Will make this character walk, setting its current velocity to
    /// whatever its walking velocity is.  While walking, a character
    /// can run again, as it will not change anything.
    /// The speed will remain constant.
    /// ENSURE:   this character's velocity will be set to it walking velocity
    /// </summary>
    void walk();
  }
}
