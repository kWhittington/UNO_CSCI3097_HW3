using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CSCI3097_HW3
{
  /// <summary>
  /// This will model the game's rules and physics system,
  /// tying together all components of the game and
  /// processing input and output.
  /// </summary>
  public class Game : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    private Managers.LevelManager level_manager;
    private int bunnies_left;
    private SpriteFont font;
    private Vector2 text_position;

    public Game()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here
      level_manager = new Managers.LevelManager(this, "starman_small", new Vector2(50, 50), "tileset-platformer", "level_one.txt");
      Components.Add(level_manager);
      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      Services.AddService(typeof(SpriteBatch), spriteBatch);
      // TODO: use this.Content to load your game content here
      this.font = Content.Load<SpriteFont>("Courier New");
      this.text_position = new Vector2(0, 0);
      base.LoadContent();
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here
      this.bunnies_left = this.level_manager.currentLevel().bunniesLeft();

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here
      spriteBatch.Begin();
      String message = string.Format("Bunnies Left: {0}", this.bunnies_left);
      spriteBatch.DrawString(font, message, text_position, Color.White);
      //if the game is over
      if (this.bunnies_left == 0)
      {
        //draw the game over message
        spriteBatch.DrawString(font, "Congratz!", new Vector2(385, 200), Color.White);
        spriteBatch.DrawString(font, "You have all teh bunnies!", new Vector2(385, 264), Color.White);
      }
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
