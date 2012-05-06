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

      base.LoadContent();
    }

    /// <summary>
    /// Allows the game component to update itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime)
    {
      
      base.Update(gameTime);
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
      //below draws the player character with no scaling
      //spriteBatch.Draw(this.character_manager.playerCharacter().Texture(),
        //this.character_manager.playerCharacter().currentPosition(), Color.White);

      //below draws the player character with 1/2 scaling
      spriteBatch.Draw(this.character_manager.playerCharacter().Texture(),
        this.character_manager.playerCharacter().currentPosition(), null,
        Color.White, 0f, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
      
      //loop through and draw the level tiles
      foreach (Tile tile in this.level_tiles)
      {
        spriteBatch.Draw(this.tile_sheet, tile.destinationRectangle, tile.textureSource, Color.White);
      }

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
