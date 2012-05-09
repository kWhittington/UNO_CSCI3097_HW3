using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCI3097_HW3.Character.AI
{
  class Aggressive:AbstractAI
  {
    /// <summary>
    /// Will update this character based on the given character's state.
    /// REQUIRE:  given character != null && given character is within sight
    /// </summary>
    public override void update(Character character)
    {
      if (this.owner.isGrounded() == true)
      {
        //stop patrolling
        this.is_patrolling = false;

        //run to the character

        //calculate which direction to run to
        float distance = (character.currentPosition().X - this.owner.currentPosition().X);

        //make this character run
        this.owner.run();
        //this.owner.jump();

        //if the distance is negative
        if (distance < 0)
        {
          //the character is to the left
          //run to the left
          //this.owner.setLeftMove();
          this.owner.setLeftJump();

          //patrol the opposite way
          this.setPatrolDirection(-1);
        }
        //otherwise,
        else
        {
          //the character is to the right
          //run to the right
          //this.owner.setRightMove();
          this.owner.setRightJump();

          //patrol the opposite way
          this.setPatrolDirection(1);
        }

        this.resetPatrolDestination();
        this.patrol();
      }
    }
  }
}
