using CSCI3097_HW3.Character;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CSCI3097_HW3Tests
{
    
    
    /// <summary>
    ///This is a test class for EnemyTest and is intended
    ///to contain all EnemyTest Unit Tests
    ///</summary>
  [TestClass()]
  public class EnemyTest
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
    ///A test for Enemy Constructor
    ///</summary>
    [TestMethod()]
    public void EnemyConstructorTest()
    {
      //Texture2D texture = null; // TODO: Initialize to an appropriate value
      //Vector2 position = new Vector2(); // TODO: Initialize to an appropriate value
      //float walk_speed = 0F; // TODO: Initialize to an appropriate value
      //float run_speed = 0F; // TODO: Initialize to an appropriate value
      //float jump_speed = 0F; // TODO: Initialize to an appropriate value
      //Enemy target = new Enemy(texture, position, walk_speed, run_speed, jump_speed);
      //Assert.Inconclusive("TODO: Implement code to verify target");
    }
  }
}
