namespace Lrp.Core.Net.Units
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SoftFX.Lrp;

	/// <summary>
    /// Summary description for TextStreamUnit
	/// </summary>
	[TestClass]
	public class TextStreamUnit
	{
		#region Initialization and Cleanup

		[TestInitialize()]
		public void Initialize()
		{
			this.stream = new TextStream();
		}

		[TestCleanup()]
		public void Cleanup()
		{
			this.stream = null;
		}

		#endregion

		[TestMethod]
		public void ReadInt32()
		{
			this.stream.Initialize("000345");
            Assert.IsTrue(this.stream.ReadInt32() == 345);

			this.stream.Initialize("345");
            Assert.IsTrue(this.stream.ReadInt32() == 345);

			this.stream.Initialize("-000345");
            Assert.IsTrue(this.stream.ReadInt32() == -345);

			this.stream.Initialize("-345");
            Assert.IsTrue(this.stream.ReadInt32() == -345);
		}

		#region Fields

		TextStream stream;

		#endregion
	}
}
