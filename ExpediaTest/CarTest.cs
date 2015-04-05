using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestGetCarLocationFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            string firstCarLocation = "Seattle, Washington";
            string secondCarLocation = "Clear Water, Florida";

            Expect.Call(mockDB.getCarLocation(7)).Return(firstCarLocation);
            Expect.Call(mockDB.getCarLocation(21)).Return(secondCarLocation);

            mocks.ReplayAll();

            Car target = ObjectMother.BMW();

            target.Database = mockDB;

            String result;

            result = target.getCarLocation(7);
            Assert.AreEqual(firstCarLocation, result);

            result = target.getCarLocation(21);
            Assert.AreEqual(secondCarLocation, result);

            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestMileagePropertyFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int Miles = 500;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = Miles;

            var target = new Car(10);
            target.Database = mockDatabase;

            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);

            mocks.VerifyAll();
        }
	}
}
