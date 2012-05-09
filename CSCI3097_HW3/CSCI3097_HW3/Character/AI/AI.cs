using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCI3097_HW3.Character.AI
{
  interface AI
  {
    /// <summary>
    /// Will return whether this AI is patrolling.
    /// ENSURE:   return this.patrolDirection() == 0
    /// </summary>
    bool isPatrolling();

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
    int patrolDirection();

    /// <summary>
    /// Will return whether or not this AI has reached the end
    /// of their patrol and needs to turn around.
    /// ENSURE:   if the end of patrol route is reached,
    ///            return true
    ///           otherwise,
    ///            return false
    /// </summary>
    bool reachedEndOfPatrol();

    /// <summary>
    /// Will set the distance and of this AI's patrol.
    /// REQUIRE:  given distance != null
    /// </summary>
    void setPatrolDistance(int distance);

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
    void setPatrolDirection(int direction);

    /// <summary>
    /// Will reset the patrol destination in relation to the AI's current position.
    /// </summary>
    void resetPatrolDestination();

    /// <summary>
    /// Will register the given character to this ai,
    /// it is necessary to perform any update procedures.
    /// REQUIRE:  given character != null
    /// ENSURE:   the character will react according to this AI's rules
    /// </summary>
    void register(Character character);

    /// <summary>
    /// Will update this character based on the given character's state.
    /// REQUIRE:  given character != null
    /// </summary>
    void update(Character character);

    /// <summary>
    /// Will regularly update this AI.
    /// </summary>
    void update();

    /// <summary>
    /// Will start this AI's patrol run.
    /// </summary>
    void patrol();

    /// <summary>
    /// Will stop this AI's patrol run.
    /// </summary>
    void stopPatrol();
  }
}
