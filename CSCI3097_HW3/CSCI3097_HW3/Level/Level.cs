using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CSCI3097_HW3.Level
{
  /// <summary>
  /// This is a game component that implements IUpdateable.
  /// </summary>
  public class Level : Microsoft.Xna.Framework.DrawableGameComponent, IDisposable
  {
    //STATIC VARIABLES
    private readonly int TILE_SIZE = 32;
    internal enum TileCollision { SOLID_COLLISION, PLATFORM_COLLISION, NO_COLLISION };
    //INSTANCE VARIABLES
    private Managers.CharacterManager character_manager;
    private Tile[,] level_tiles;
    private Texture2D tile_sheet;
    private Stream level_sheet_stream;
    private String tile_sheet_name;
    private int tile_sheet_width_in_tiles;
    private int tile_sheet_height_in_tiles;

    public Level(Game game, Texture2D player_texture, Vector2 player_position,
      String tile_sheet, Stream level_sheet)
      : base(game)
    {
      //give the character manager a player skin and location
      this.character_manager = new Managers.CharacterManager(game, player_texture, player_position);
      this.tile_sheet_name = tile_sheet;
      this.level_sheet_stream = level_sheet;
      
    }

    /// <summary>
    /// Will return the number of bunnies left on this level.
    /// </summary>
    public int bunniesLeft()
    {
      //return the number of alive characters, minus one for the player
      return this.character_manager.aliveCharacters().Count - 1;
    }

    /// <summary>
    /// Will read in the data from the given level_sheet file, instantiate this
    /// level's tile array, and populate it with newly created tiles.
    /// The given data stream must be from a text file of number values.
    /// The values must be positive, real numbers and in rows and columns
    /// of uniform size.  The numbers can be separated by spaces within row.
    /// REQUIRE:  given.level_sheet != null
    /// ENSURE:   this.level_tiles.width
    /// </summary>
    private void loadLevel(Stream level_sheet)
    {
      //check of the rows are of a uniform width
      int width;
      List<List<int>> tile_values = new List<List<int>>();
      using (StreamReader reader = new StreamReader(level_sheet))
      {
        
        //read the first line of the file
        string line = reader.ReadLine();
        //set the row width value to whatever the first line's length was
        width = parseStringToIntArray(line).Capacity;
        //while there are still lines left
        while (line != null)
        {
          List<int> line_values = parseStringToIntArray(line);
          tile_values.Add(line_values);
          if (line_values.Capacity != width)
            throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", tile_values.Count));
          line = reader.ReadLine();
        }
      }

      // Allocate the tile grid.
      this.level_tiles = new Tile[tile_values[0].Count, tile_values.Count];

      // Loop over every tile position,
      for (int y = 0; y < this.level_tiles.GetLength(1); ++y)
      {
        for (int x = 0; x < this.level_tiles.GetLength(0); ++x)
        {
          // to load each tile.
          int tile_value = tile_values[y][x];
          // if there is a negative value
          if (tile_value < 0)
          {
            //load a blank tile
            this.level_tiles[x, y] = this.LoadBlankTile(x, y);
          }
          //or if the tile value is 9, 10, 11, 50, 51, 52, or 57
          else if ((9 <= tile_value && tile_value <= 11) 
            || (50 <= tile_value && tile_value <= 52)
            || tile_value == 57)
          {
            Tile new_tile = LoadTile(tile_value, x, y);
            new_tile.makePassable();
            this.level_tiles[x, y] = new_tile;
          }
          //otherwise, the tile texture exists on the tile sheet
          else
          {
            this.level_tiles[x, y] = LoadTile(tile_value, x, y);
          }
        }
      }
    }

    /// <summary>
    /// Will try to parse the given string, converting the numerical values
    /// found into integers and storing them in an array the order in which
    /// they are found.
    /// REQUIRE:  the given string is of positive,
    ///            real numbers separated by spaces
    /// ENSURE:   an array of ints is returned
    /// </summary>
    private List<int> parseStringToIntArray(String line)
    {
      List<int> result = new List<int>();
      //split the line into separate numbers
      string[] numbers = line.Split(' ');

      //for each number in the line
      foreach (string number in numbers) {
        //try to convert the number into an int
        try
        {
          result.Add(Int32.Parse(number));
        }
        //if number is not the right format
        catch (FormatException)
        {
          Console.WriteLine("Unable to format '{0}', in incorrect format.", number);
        }
        //if number is too large
        catch (OverflowException)
        {
          Console.WriteLine("Unable to format '{0}', the value is too large.", number);
        }
      }
      
      return result;
    }

    /// <summary>
    /// Will create and return a new instance of a 
    /// passable tile with a blank texture.
    /// ENSURE:   returns a new tile with a blank texture,
    ///            && new.tile.collisionType() == TileCollision.NO_COLLISION
    /// </summary>
    private Tile LoadBlankTile(int x, int y)
    {
      //make a new tile with texture source of 116, which is blank
      Tile blank_tile = this.LoadTile(116, x, y);
      //now remove collision from the tile
      blank_tile.makePassable();

      return blank_tile;
    }

    /// <summary>
    /// Will take the given tile value, x, and y placement and
    /// return a newly instantiated Tile object with the
    /// appropriate texture, X, and Y values.
    /// REQUIRE:  given.tile_value is a positive real number
    ///            && given X and Y are positive real numbers.
    /// ENSURE:   returns a new tile with the correct texture, X and Y values.
    /// </summary>
    private Tile LoadTile(int tile_value, int x, int y)
    {
      // calculate the tile's horizontal placement
      int horizontal_position = (this.TILE_SIZE * x);
      // now calculate the tile's vertical placement
      int vertical_position = (this.TILE_SIZE * y);
      //NOTE: Remember, the origin for objects is the top-left corner
      
      //calculate the source texture's dimensions on the tile sheet
      Rectangle source_dimensions = sourceTexture(tile_value);
      //return a new Tile object
      return new Tile(source_dimensions, horizontal_position, vertical_position, this.TILE_SIZE);
    }

    /// <summary>
    /// Will return a new rectangle with the dimensions of the proper
    /// texture from the tile sheet based on the given tile value.
    /// REQUIRE:  give tile value a positive, real number
    /// ENSURE:   a rectangle fitting within the dimensions
    ///            of the sprite sheet is returned
    /// </summary>
    private Rectangle sourceTexture(int tile_number)
    {
      Rectangle result = new Rectangle();

      //first compute which column on the sprite sheet the texture is
      int column = tile_number % this.tile_sheet_width_in_tiles;
      //then compute which row the texture is in
      int row = tile_number / this.tile_sheet_width_in_tiles;

      //now compute the horizontal placement of the texture
      int horizontal_position = column * this.TILE_SIZE;
      //then the vertical placement
      int vertical_position = row * this.TILE_SIZE;

      //now set the rectangle to that x,y position and give it the proper width
      result = new Rectangle(horizontal_position, vertical_position,
        this.TILE_SIZE, this.TILE_SIZE);

      return result;
    }

    /// <summary>
    /// Allows the game component to perform any initialization it needs to before starting
    /// to run.  This is where it can query for any required services and load content.
    /// </summary>
    public override void Initialize()
    {
      this.tile_sheet = Game.Content.Load<Texture2D>(tile_sheet_name);
      //compute how many tiles wide the sprite sheet is
      this.tile_sheet_width_in_tiles = this.tile_sheet.Width / this.TILE_SIZE;
      //compute how many tiles high the sprite sheet is
      this.tile_sheet_height_in_tiles = this.tile_sheet.Height / this.TILE_SIZE;

      //add the manager
      Game.Components.Add(this.character_manager);
      base.Initialize();
    }

    protected override void LoadContent()
    {
      
      //load the level
      this.loadLevel(level_sheet_stream);

      //there will be 7 bunnies
      Texture2D enemy_texture = Game.Content.Load<Texture2D>("bunny_brown");
      Vector2[] enemy_start = new Vector2[7];
      enemy_start[0] = new Vector2(TILE_SIZE*3, TILE_SIZE*2);
      enemy_start[1] = new Vector2(TILE_SIZE*11, TILE_SIZE*2);
      enemy_start[2] = new Vector2(TILE_SIZE*16, TILE_SIZE*6);
      enemy_start[3] = new Vector2(TILE_SIZE*4, TILE_SIZE*9);
      enemy_start[4] = new Vector2(TILE_SIZE*22, TILE_SIZE*9);
      enemy_start[5] = new Vector2(TILE_SIZE*4, TILE_SIZE*13);
      enemy_start[6] = new Vector2(TILE_SIZE*6, TILE_SIZE*13);

      this.character_manager.addEnemy(enemy_texture, enemy_start[0],
        new Character.AI.Neutral(), 30, -1, false );
      this.character_manager.addEnemy(enemy_texture, enemy_start[1],
        new Character.AI.Neutral(), 60, -1, true);
      this.character_manager.addEnemy(enemy_texture, enemy_start[2],
        new Character.AI.Shy(), 1, 1, true);
      this.character_manager.addEnemy(enemy_texture, enemy_start[3],
        new Character.AI.Aggressive(), 50, 1, true);
      this.character_manager.addEnemy(enemy_texture, enemy_start[4],
        new Character.AI.Shy(), 240, -1, true);
      this.character_manager.addEnemy(enemy_texture, enemy_start[5],
        new Character.AI.Aggressive(), 30, 1, true);
      this.character_manager.addEnemy(enemy_texture, enemy_start[6],
        new Character.AI.Shy(), 10, 1, true);

      base.LoadContent();
    }

    /// <summary>
    /// Allows the game component to update itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime)
    {
      //check for collisions
      //this.checkPlayerCollision();
      this.checkTileCollision(this.character_manager.playerCharacter());
      this.checkCharacterCollision(this.character_manager.playerCharacter());
      this.checkForSolidGround(this.character_manager.playerCharacter());
      foreach (Character.Enemy enemy in this.character_manager.enemyList())
      {
        this.checkTileCollision(enemy);
        this.checkForSolidGround(enemy);
      }
      //move the characters
      this.character_manager.playerCharacter().moveCharacter();
      foreach (Character.Enemy enemy in this.character_manager.enemyList())
      {
        enemy.moveCharacter();
      }
      
      base.Update(gameTime);
    }

    /// <summary>
    /// Will check if the given character has collided with another character.
    /// If so, the opposing character will be removed from play.
    /// </summary>
    private void checkCharacterCollision(Character.Character character)
    {
      //for every enemy alive
      foreach (Character.Enemy enemy in this.character_manager.enemyList())
      {
        //if the enemy is alive
        if (enemy.isAlive() == true)
        {
          //check if it is overlapping with the given character
          if (enemy.BoundingBox().Intersects(character.BoundingBox()))
          {
            //then remove the enemy from play
            enemy.kill();
          }
        }
      }
    }

    /// <summary>
    /// Will check if the given character has collided with a tile
    /// in the level and then handle the position correction.
    /// ENSURE:   the given character will not be colliding with any tiles.
    /// </summary>
    private void checkTileCollision(Character.Character character)
    {
      //for every tile in the level
      foreach (Tile tile in this.level_tiles)
      {
        //save the character's bounding box
        Rectangle character_bounds = character.BoundingBox();

        //now get the character's current velocity
        Vector2 character_velocity = character.currentSpeed();
        //compute the future position of the character
        Rectangle future_bounds = new Rectangle(
          character_bounds.X + (int)character_velocity.X,
          character_bounds.Y + (int)character_velocity.Y,
          character_bounds.Width, character_bounds.Height);

        //check if the character WILL collide with this tile
        if (tile.collidesWith(future_bounds)){
          //find how much overlap is occuring
          Rectangle intersection = Rectangle.Intersect(tile.destinationRectangle, future_bounds);

          //alter the characters velocity to take the collision into account
          Vector2 offset = new Vector2();
          //if the character has horizontal velocity
          //and isn't jumping or falling
          if ((character.isJumping() == false && character.isFalling() == false)
            && character_velocity.X != 0)
          {
            //compute the horizontal offset
            offset.X = (character_velocity.X > 0) ?
              (intersection.X - intersection.Right) : //negative offset if positive velocity
              (intersection.Right - intersection.X); //positive offset if negative velocity

            //add the offset to the velocity value
            //character_velocity.X = character_velocity.X + offset.X;
          }
          //if the character has vertical velocity
          if (character_velocity.Y != 0)
          {
            //they are jumping or falling into this object
            
            //first fix their vertical forces
            //if the character has a positive vertical velocity
            if (character_velocity.Y > 0)
            {
              //then the offset needs to be negative
              offset.Y = (intersection.Y - intersection.Bottom) -1;
              //this also means the character has hit ground
              //character.ground();
            }
            //otherwise, the velocity is negative
            else
            {
              //so the offset needs to be positive
              offset.Y = (intersection.Bottom - intersection.Y);
              //the character hit the bottom of something, fall
              character.fall();
            }

            //now they might have horizontal forces,
            //before we fix them, see if the character is no longer colliding
            //compute the future position of the character
            Rectangle new_future_bounds = new Rectangle(
              future_bounds.X + (int)offset.X,
              future_bounds.Y + (int)offset.Y,
              future_bounds.Width, future_bounds.Height);
            if (tile.collidesWith(new_future_bounds))
            {
              //then fix the horizontal forces
              Rectangle new_intersection = Rectangle.Intersect(tile.destinationRectangle, new_future_bounds);

              //compute the horizontal offset
              offset.X = (character_velocity.X > 0) ?
                (intersection.X - intersection.Right) : //negative offset if positive velocity
                (intersection.Right - intersection.X); //positive offset if negative velocity
            }
          }
          character_velocity.X = character_velocity.X + offset.X;
          character_velocity.Y = character_velocity.Y + offset.Y;
          //give the player it's new position
          character.setVelocity(character_velocity);
        }
      }
    }

    /// <summary>
    /// Will check if the given character is standing on solid ground.
    /// If not, they will fall.
    /// </summary>
    private void checkForSolidGround(Character.Character character)
    {
      //this only matters if the character is grounded
      if (character.isJumping() == false)
      {
        //for every tile in the level
        foreach (Tile tile in this.level_tiles)
        {
          //if they will collide with character and is below the character
          //and is not solid
          if (tile.isUnder(character))
          {
            //if the tile is not solid
            if (tile.collisionType.Equals(TileCollision.NO_COLLISION))
            {
              //make the character fall
              character.fall();
            }
            //otherwise, ground the character
            else
            {
              character.ground();
            }
          }
        }
      }
    }

    /// <summary>
    /// Allows the game component to draw itself.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
      //draw the player character
      SpriteBatch spriteBatch = Game.Services.GetService(
        typeof(SpriteBatch)) as SpriteBatch;

      spriteBatch.Begin();

      //loop through and draw the level tiles
      foreach (Tile tile in this.level_tiles)
      {
        spriteBatch.Draw(this.tile_sheet, tile.destinationRectangle, tile.textureSource, Color.White);
      }

      //below draws the player character with no scaling
      spriteBatch.Draw(this.character_manager.playerCharacter().Texture(),
        this.character_manager.playerCharacter().currentPosition(), Color.White);

      //loop through and draw each enemy
      foreach (Character.Enemy enemy in this.character_manager.enemyList())
      {
        if (enemy.isAlive() == true)
        {
          spriteBatch.Draw(enemy.Texture(), enemy.currentPosition(), Color.White);
        }
      }

      //could be taken out
      //below draws the player character with 1/2 scaling
      //spriteBatch.Draw(this.character_manager.playerCharacter().Texture(),
      //  this.character_manager.playerCharacter().currentPosition(), null,
      //  Color.White, 0f, new Vector2(0, 0), .5f, SpriteEffects.None, 0);

      spriteBatch.End();

      base.Draw(gameTime);
    }

    #region TILE

    /// <summary>
    /// Will model a Tile object to hold the placement, source,
    /// and collision values neccessary for implementation.
    /// </summary>
    internal class Tile
    {
      private TileCollision collision;
      private Rectangle dimensions;
      private Rectangle source_dimensions;

      /// <summary>
      /// Will create a new Tile object with the given position
      /// values and width/height values.
      /// REQUIRE:  The tile's width and height are like sizes.
      /// ENSURE:   This tile's position.X && Y == given.horizontal_position
      ///            && given.vertical_position respectively
      ///           This tile's width and height equal the given
      ///            width.
      /// </summary>
      /// <param name="?"></param>
      internal Tile(Rectangle source, int horizontal_position, int vertical_position, int width)
      {
        this.source_dimensions = source;
        this.dimensions = new Rectangle(horizontal_position, vertical_position, width, width);
        //by default, every tile is solid
        this.collision = TileCollision.SOLID_COLLISION;
      }

      /// <summary>
      /// Will set this tile's collision to solid.
      /// ENSURE:   this tile's collision == TileCollision.SOLID_COLLISION
      /// </summary>
      internal void makeSolid()
      {
        this.collision = TileCollision.SOLID_COLLISION;
      }

      /// <summary>
      /// Will set this tile's collision to platform.
      /// ENSURE:   this tile's collision == TileCollision.PLATFORM_COLLISION
      /// </summary>
      internal void makePlatform()
      {
        this.collision = TileCollision.PLATFORM_COLLISION;
      }

      /// <summary>
      /// Will remove this tile's collision property.
      /// ENSURE:   this tile's collision == TileCollision.NO_COLLISION
      /// </summary>
      internal void makePassable()
      {
        this.collision = TileCollision.NO_COLLISION;
      }

      /// <summary>
      /// Will return this tile's collision property.
      /// ENSURE:   returns this.collision
      /// </summary>
      internal TileCollision collisionType
      {
        get { return this.collision; }
      }

      /// <summary>
      /// Will return whether or not this tile collides
      /// with the given bounding box.
      /// REQUIRE:  the given bounding box != null
      /// ENSURE:   if the given rectangle overlaps with this tile's box,
      ///            return true
      ///           otherwise,
      ///            return false
      /// </summary>
      internal bool collidesWith(Rectangle object_box)
      {
        //initially set the result to false
        bool result = false;

        //if this tile is able to collide with the object
        if (this.collision.Equals(TileCollision.NO_COLLISION) == false)
        {
          //and if the object overlaps with this tile's box
          if (this.dimensions.Intersects(object_box) == true)
          {
            //then there is a collision occuring
            result = true;
          }
        }

        //return whatever the computed result is
        return result;
      }

      /// <summary>
      /// Returns whether or not the tile is under the given character.
      /// ENSURE:   if the tile is immidiately below the character,
      ///            return true
      ///           otherwise,
      ///            return false
      /// </summary>
      internal bool isUnder(Character.Character character)
      {
        bool result = false;
        Rectangle box = character.BoundingBox();

        //if the box is within the correct horizontal scope
        if ((box.Left <= this.destinationRectangle.Right &&
          box.Left >= this.destinationRectangle.Left) ||
          (box.Right <= this.destinationRectangle.Right &&
          box.Right >= this.destinationRectangle.Left))
        {
          //if this tile is immidiate below the character
          if (box.Bottom < this.destinationRectangle.Top &&
            box.Bottom > (this.destinationRectangle.Top - 5))
          {
            result = true;
          }
        }

        return result;
      }

      /// <summary>
      /// Will return this tile's horizontal position.
      /// ENSURE:   returns this.dimension.X
      /// </summary>
      internal int horizontal_position
      {
        get { return this.dimensions.X; }
      }

      /// <summary>
      /// Will return this tile's veritcal position.
      /// ENSURE:   returns this.dimension.Y
      /// </summary>
      internal int vertical_position
      {
        get { return this.dimensions.Y; }
      }

      /// <summary>
      /// Will return this tile's width.
      /// ENSURE:   returns this.dimension.Width
      /// </summary>
      internal int width
      {
        get { return this.dimensions.Width; }
      }

      /// <summary>
      /// Will return this tile's height.
      /// ENSURE:   returns this.dimension.Height
      /// </summary>
      internal int height
      {
        get { return this.dimensions.Height; }
      }

      ///<summary>
      /// Will return this tile's destination rectangle.
      /// ENSURE:   returns this.dimensions
      ///</summary>
      internal Rectangle destinationRectangle
      {
        get { return this.dimensions; }
      }

      /// <summary>
      /// Will return this tile's source rectangle.
      /// ENSURE:   returns this.source_dimension
      /// </summary>
      internal Rectangle textureSource
      {
        get { return this.source_dimensions; }
      }

    }
    #endregion TILE
  }
}
