using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSCI3097_HW3.Character
{
  /// <summary>
  /// Will model a player character receiving input
  /// from a source outside of this program but being
  /// processed by its components.
  /// </summary>
  class Player : AbstractCharacter
  {
    /// <summary>
    /// Will create a new instance of this player character with the given
    /// texture, at the given position, with the given walking, running
    /// and jumping speeds.
    /// REQUIRE:  given.texture != null
    ///            given.position != nil && given.position.X,Y > 0
    ///            given.walking, running, jumping speed != null && > 0
    /// ENSURE:   this.currentSpeed() == given.walking_speed
    ///            this.boundingBox().X,Y == given.position.X,Y
    /// </summary>
    public Player(Texture2D texture, Vector2 position, float walk_speed,
      float run_speed, float jump_speed)
    {
      this.texture = texture;
      this.position = position;
      this.walk_velocity = walk_speed;
      this.run_velocity = run_speed;
      this.jump_velocity = jump_speed;
      this.current_velocity = new Vector2(walk_speed, 0);
    }
  }
}
