using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CSCI3097_HW3.Managers
{
  /// <summary>
  /// This objects will model
  /// </summary>
  static class PhysicsManager
  {
    //This will be the gravity value in the game world
    //the value is in pixels
    private static readonly float gravity = 9.8f;

    public static float Gravity
    {
      get { return gravity; }
    }

    /// <summary>
    /// This will calculate the ending position of the given
    /// starting position with the given force applied to it.
    /// REQUIRE:  given starting position && force != null
    /// ENSURE:   the correct ending position is calculated
    /// </summary>
    public static Vector2 endPosition(Vector2 start, Vector2 force)
    {
      //make a list object to hold the force
      List<Vector2> list = new List<Vector2>();
      //add the force to the list
      list.Add(force);
      
      //now call the other endPosition method
      return endPosition(start, list);
    }

    /// <summary>
    /// This will calculate the ending position of the given
    /// starting position with the given forces applied to it.
    /// REQUIRE:  given starting position, forces != null
    /// ENSURE:   the correct ending position is calculated
    /// </summary>
    public static Vector2 endPosition(Vector2 start, List<Vector2> forces)
    {
      //start the end off at the starting position
      Vector2 end = new Vector2(start.X, start.Y);

      //for every force in the list
      foreach (Vector2 force in forces)
      {
        //apply it to the position
        end.X = end.X + force.X;
        end.Y = end.Y + force.Y;
      }

      //now end should be in the correct position
      return end;
    }

    /// <summary>
    /// This will calculate the ending position of the given
    /// starting position with the given forces applied to it
    /// without letting the end position exceede the given boundaries.
    /// REQUIRE:  given starting position, forces, boundary != null
    /// ENSURE:   the correct ending position is calculated
    public static Vector2 endPositionWithinBoundary(Vector2 start,
        List<Vector2> forces, Rectangle boundary)
    {
      //start the end off at the starting position
      Vector2 end = new Vector2(start.X, start.Y);

      //for every force in the list
      foreach (Vector2 force in forces)
      {
        //temporarily calculate the values
        float temp_x = end.X + force.X;
        float temp_y = end.Y + force.Y;

        //apply it to the position, within the bounds
        //if the horizontal force will be greater than the boundary
        if (temp_x > boundary.Right)
        {
          //set the end position to the max value
          end.X = boundary.Right;
        }
        //or if the horizontal force will be less than the boundary
        else if (temp_x < boundary.Left)
        {
          //set the ned position to the min value
          end.X = boundary.Left;
        }
        //otherwise, the calculation is within the bounds
        else
        {
          end.X = temp_x;
        }

        //now do the same for the vertical position
        if (temp_y > boundary.Bottom)
        {
          end.Y = boundary.Bottom;
        }
        else if (temp_y < boundary.Top)
        {
          end.Y = boundary.Top;
        }
        else
        {
          end.Y = temp_y;
        }
      }

      //now end should be in the correct position
      return end;
    }

  }
}
