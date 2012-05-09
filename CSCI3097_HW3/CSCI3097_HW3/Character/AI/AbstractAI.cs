using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CSCI3097_HW3.Character.AI
{
  abstract class AbstractAI:AI
  {
    //this is the reference to the ai's owner
    protected Character owner;
    protected int patrol_distance;
    protected int patrol_direction;
    protected bool is_patrolling;
    protected Vector2 patrol_destination;

    /// <summary>
    /// Will return whether this AI is patrolling.
    /// ENSURE:   return this.is_patrolling
    /// </summary>
    public bool isPatrolling()
    {
      //if the patrol direction isn't zero, its patrolling
      return this.is_patrolling;
    }
    /// <summary>
    /// Will return the direction this AI is patrolling.
    /// The values will be -1, 0, or 1.
    /// If the character is not patrolling, 0 is returned.
    /// If the character is patrolling left, -1 is returned.
    /// If the character is patrolling right, 1 is returned.
    /// ENSURE:   if the character is not patrolling,
    ///            return 0
    ///           if the character is patrolling left,
    ///            return -1
    ///           if the character is patrolling right,
    ///            return 1
    /// </summary>
    public int patrolDirection()
    {
      return this.patrol_direction;
    }

    /// <summary>
    /// Will return whether or not this AI has reached the end
    /// of their patrol and needs to turn around.
    /// ENSURE:   if the end of patrol route is reached,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    public bool reachedEndOfPatrol()
    {
      bool result = false;

      //calculate the distance to the patrol destination
      float distance = (this.owner.currentPosition().X - this.patrol_destination.X);

      //if the character is patrolling left
      if (this.patrol_direction < 0 && distance <= 0)
      {
        result = true;
      }
      //or if the character is patrolling right
      else if (this.patrol_direction > 0 && distance >= 0)
      {
        result = true;
      }

      return result;
    }

    /// <summary>
    /// Will set the distance and of this AI's patrol.
    /// REQUIRE:  given distance != null
    /// </summary>
    public void setPatrolDistance(int distance)
    {
      this.patrol_distance = distance;
    }

    /// <summary>
    /// Will set the patrol direction of this AI's patrol.
    /// If the given number is less than 0, the direction is understood
    /// to be to the left. If the number is 0, there is no direction.
    /// If the number is greater than 0, the direction is understood
    /// to be to the right.
    /// REQUIRE:  given number != null
    /// ENSURE:   if given number less than 0,
    ///            patrol to the left
    ///           if given number is 0,
    ///            no patrol direction
    ///           if given number is greater than 0,
    ///            patrol to the right
    /// </summary>
    public void setPatrolDirection(int direction)
    {
      //if the direction is negative
      if (direction < 0)
      {
        //patrol left
        this.patrol_direction = -1;
      }
      //or if the direction is positive
      else if (direction > 0)
      {
        //patrol right
        this.patrol_direction = 1;
      }
      //otherwise
      else
      {
        //no patrol
        this.patrol_direction = 0;
      }

      //now set the destination point
      this.patrol_destination = new Vector2(
        this.owner.currentPosition().X + (this.patrol_direction * this.patrol_distance),
        this.owner.currentPosition().Y);
    }

    /// <summary>
    /// Will reset the patrol destination in relation to the AI's current position.
    /// </summary>
    public void resetPatrolDestination()
    {
      this.patrol_destination.X = this.owner.currentPosition().X + (this.patrol_direction * this.patrol_distance);
    }

    /// <summary>
    /// Will register the given character to this ai,
    /// it is necessary to perform any update procedures.
    /// REQUIRE:  given character != null
    /// ENSURE:   the character will react according to this AI's rules
    /// </summary>
    public void register(Character character)
    {
      this.owner = character;
    }

    /// <summary>
    /// Will update this character based on the given character's state.
    /// REQUIRE:  given character != null
    /// </summary>
    public abstract void update(Character character);

    /// <summary>
    /// Will regularly update this AI.
    /// </summary>
    public void update()
    {
      //if this AI is patrolling
      if (this.is_patrolling == true)
      {
        //then patrol
        this.patrol();
      }
      //otherwise, don't do anything.
    }

    /// <summary>
    /// Will start this AI's patrol run.
    /// </summary>
    public void patrol()
    {
      this.is_patrolling = true;
      this.owner.walk();

      //if this AI isn't done patrolling
      if (this.reachedEndOfPatrol() == false)
      {
        //if this AI is patrolling left
        if (this.patrol_direction < 0)
        {
          //move left
          this.owner.setLeftMove();
        }
        //or if this AI is patrolling right
        else if (this.patrol_direction > 0)
        {
          //move right
          this.owner.setRightMove();
        }
      }
      //otherwise, the end is reached
      else
      {
        //turn around
        this.patrol_direction = this.patrol_direction * -1;
        this.patrol_destination.X = this.patrol_destination.X + (this.patrol_direction * this.patrol_distance);
      }
    }

    /// <summary>
    /// Will stop this AI's patrol run.
    /// </summary>
    public void stopPatrol()
    {
      this.is_patrolling = false;
      this.patrol_direction = 0;
    }
  }
}
