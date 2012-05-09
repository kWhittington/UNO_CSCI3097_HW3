using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSCI3097_HW3.Character
{
  /// <summary>
  /// Will model an enemy character receiving input from the program for now.
  /// </summary>
  class Enemy : AbstractCharacter
  {
    private float sight_range;
    private AI.AI personality;

    /// <summary>
    /// Will create a new instance of this enemy character with the given
    /// texture, at the given position, with the given walking, running
    /// and jumping speeds.
    /// REQUIRE:  given.texture != null
    ///            given.position != nil && given.position.X,Y > 0
    ///            given.walking, running, jumping speed != null && > 0
    /// ENSURE:   this.currentSpeed() == given.walking_speed
    ///            this.boundingBox().X,Y == given.position.X,Y
    /// </summary>
    public Enemy(Texture2D texture, Vector2 position, float walk_speed,
      float run_speed, float jump_speed, float jump_height, float sight_range,
      AI.AI personality)
    {
      this.personality = personality;
      //register this enemy to the personality
      this.personality.register(this);
      this.texture = texture;
      this.position = position;
      this.sight_range = sight_range;
      this.walk_velocity = walk_speed;
      this.run_velocity = run_speed;
      this.jump_velocity = jump_speed;
      this.fall_velocity = jump_speed;
      this.max_jump_height = jump_height;
      this.is_jumping_left = false;
      this.is_jumping_right = false;
      this.current_velocity = new Vector2(0, 0);
      //start the enemy off as alive
      this.is_alive = true;
    }

    /// <summary>
    /// Will return the sight range of this enemy.
    /// ENSURE:   this.sight_range is returned
    /// </summary>
    public float sightRange()
    {
      return this.sight_range;
    }

    /// <summary>
    /// Will return whether this enemy can see the given character.
    /// If the character is within sight of this enemy,
    /// then the enemy can react to them.
    /// REQUIRE:  given character != null
    /// ENSURE:   if the given character's position is
    ///            within this enemy's sight range,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool canSee(Character character)
    {
      bool result = false;

      //calculate the distance from this enemy to the character
      float distance = Vector2.Distance(this.position, character.currentPosition());

      //if the character is within sight
      if (Math.Abs(distance) < this.sight_range)
      {
        //then this enemy can see it
        result = true;
      }
      return result;
    }

    public AI.AI Personality()
    {
      return this.personality;
    }

    /// <summary>
    /// Will call this enemy's AI routine to update it
    /// based on the given character.
    /// REQUIRE:  given character != null
    /// </summary>
    public void react(Character character)
    {
      this.personality.update(character);
    }
  }
}
