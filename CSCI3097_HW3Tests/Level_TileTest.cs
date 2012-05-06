using CSCI3097_HW3.Level;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework;

namespace CSCI3097_HW3Tests
{
    
    
    /// <summary>
    ///This is a test class for Level_TileTest and is intended
    ///to contain all Level_TileTest Unit Tests
    ///</summary>
  [TestClass()]
  public class Level_TileTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///A test for Tile Constructor
    ///</summary>
    [TestMethod()]
    public void Level_TileConstructorTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width);
      Assert.Inconclusive("TODO: Implement code to verify target");
    }

    /// <summary>
    ///A test for makePassable
    ///</summary>
    [TestMethod()]
    public void makePassableTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      target.makePassable();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for makePlatform
    ///</summary>
    [TestMethod()]
    public void makePlatformTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      target.makePlatform();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for makeSolid
    ///</summary>
    [TestMethod()]
    public void makeSolidTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      target.makeSolid();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }

    /// <summary>
    ///A test for collisionType
    ///</summary>
    [TestMethod()]
    public void collisionTypeTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      Level.TileCollision actual;
      actual = target.collisionType;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for height
    ///</summary>
    [TestMethod()]
    public void heightTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      int actual;
      actual = target.height;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for horizontal_position
    ///</summary>
    [TestMethod()]
    public void horizontal_positionTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      int actual;
      actual = target.horizontal_position;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for textureSource
    ///</summary>
    [TestMethod()]
    public void textureSourceTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      Rectangle actual;
      actual = target.textureSource;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for vertical_position
    ///</summary>
    [TestMethod()]
    public void vertical_positionTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      int actual;
      actual = target.vertical_position;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    ///A test for width
    ///</summary>
    [TestMethod()]
    public void widthTest()
    {
      Rectangle source = new Rectangle(); // TODO: Initialize to an appropriate value
      int horizontal_position = 0; // TODO: Initialize to an appropriate value
      int vertical_position = 0; // TODO: Initialize to an appropriate value
      int width = 0; // TODO: Initialize to an appropriate value
      Level.Tile target = new Level.Tile(source, horizontal_position, vertical_position, width); // TODO: Initialize to an appropriate value
      int actual;
      actual = target.width;
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
