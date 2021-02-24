namespace _01.Microsystem.Tests
{
    using NUnit.Framework;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tests
    {
        [Test]
        public void Create_Computer_Count_Should_Increase()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer1 = new Computer(1, Brand.ACER, 1120, 15.6, "grey");
            var computer2 = new Computer(2, Brand.ASUS, 2000, 15.6, "red");
            //Act

            var expectedCount = 2;
            microsystems.CreateComputer(computer1);
            microsystems.CreateComputer(computer2);
            var actualCount = microsystems.Count();

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Create_Computer_Should_Throw_Exception_With_Existing_Id()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computerValid = new Computer(1, Brand.ASUS, 1800, 14, "black");
            var invalidComputer = new Computer(1, Brand.ASUS, 1800, 14, "black");

            //Act

            microsystems.CreateComputer(computerValid);

            //Arrange

            Assert.Throws<ArgumentException>(() => microsystems.CreateComputer(invalidComputer));
        }

        [Test]
        public void Contains_Should_Return_True_With_Valid_Number()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);

            //Assert

            Assert.IsTrue(microsystems.Contains(1));
        }

        [Test]
        public void Contains_Should_Return_False_With_Invalid_Number()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);

            //Assert

            Assert.IsFalse(microsystems.Contains(3));
        }

        [Test]
        public void Count_Should_Work_Correct()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);

            //Assert

            Assert.AreEqual(3, microsystems.Count());
        }

        [Test]
        public void Remove_Should_Decrease_Count()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.Remove(1);

            //Assert

            Assert.AreEqual(2, microsystems.Count());
        }

        [Test]
        public void Remove_Should_Throw_Exception_With_Invalid_Id()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.Remove(1);

            //Assert

            Assert.Throws<ArgumentException>(() => microsystems.Remove(1));
        }

        [Test]
        public void After_Remove_Computer_Should_Not_Exist()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.Remove(1);

            //Assert

            Assert.IsFalse(microsystems.Contains(1));
        }

        [Test]
        public void Remove_With_Brand_Should_Decrease_Count()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            microsystems.RemoveWithBrand(Brand.DELL);

            //Assert

            Assert.AreEqual(1, microsystems.Count());
        }

        [Test]
        public void Remove_With_Non_Existing_Brand_Should_Throw_Exception()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);


            //Assert

            Assert.Throws<ArgumentException>(() => microsystems.RemoveWithBrand(Brand.HP));
        }

        [Test]
        public void Remove_With_Brand_Should_Not_Contain_Removed_Computers()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            microsystems.RemoveWithBrand(Brand.DELL);


            //Assert

            Assert.IsFalse(microsystems.Contains(1));
            Assert.IsFalse(microsystems.Contains(3));
            Assert.IsFalse(microsystems.Contains(4));
        }

        [Test]
        public void Upgrade_Ram_Should_Change_Ram()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            microsystems.UpgradeRam(16, 1);
            var expectedRam = 16;
            var actualRam = microsystems.GetComputer(1).RAM;

            //Assert

            Assert.AreEqual(expectedRam, actualRam);
        }

        [Test]
        public void Upgrade_Ram_Should_Not_Change_Ram()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            microsystems.UpgradeRam(4, 1);
            var expectedRam = 8;
            var actualRam = microsystems.GetComputer(1).RAM;

            //Assert

            Assert.AreEqual(expectedRam, actualRam);
        }

        [Test]
        public void Upgrade_Ram_Should_Throw_Exception_With_Invalid_Number()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);

            //Assert

            Assert.Throws<ArgumentException>(() => microsystems.UpgradeRam(16, 12));
        }

        [Test]
        public void GetAllFromBrand_Should_Return_Correct_Count()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expectedCount = 3;
            var actualCount = microsystems.GetAllFromBrand(Brand.DELL).Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GetAllFromBrand_Should_Return_Correct_Order()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expected = new List<Computer>()
        {
            new Computer(4, Brand.DELL, 2800, 15.6, "grey"),
             new Computer(1, Brand.DELL, 2300, 15.6, "grey"),
             new Computer(3, Brand.DELL, 2200, 15.6, "grey")
        };
            var actual = microsystems.GetAllFromBrand(Brand.DELL).ToList();

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllFromBrand_Should_Return_Empty_Collection()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expected = Enumerable.Empty<Computer>();
            var actual = microsystems.GetAllFromBrand(Brand.ASUS);

            //Assert

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllWithScreenSize_Should_Return_Correct_Count()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expected = 4;
            var actual = microsystems.GetAllWithScreenSize(15.6).Count();

            //Assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllWithScreenSize_Should_Return_Correct_Order()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(7, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
            var computer3 = new Computer(6, Brand.DELL, 2800, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expected = new List<Computer>()
        {
            new Computer(7, Brand.DELL, 2300, 15.6, "grey"),
            new Computer(6, Brand.DELL, 2800, 15.6, "grey"),
        new Computer(5, Brand.ACER, 2300, 15.6, "grey"),
        new Computer(3, Brand.DELL, 2200, 15.6, "grey")
    };
            var actual = microsystems.GetAllWithScreenSize(15.6).ToList();

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllWithScreenSize_Should_Return_Empty_Collection()
        {
            //Arrange

            var microsystems = new Microsystems();
            var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
            var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
            var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
            var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

            //Act
            microsystems.CreateComputer(computer);
            microsystems.CreateComputer(computer2);
            microsystems.CreateComputer(computer3);
            microsystems.CreateComputer(computer4);
            var expected = Enumerable.Empty<Computer>();
            var actual = microsystems.GetAllWithScreenSize(14);

            //Assert

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
    public void GetComputer_Should_Return_Correct_Computer()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
        var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);

        var expectedNumber = 3;
        var expectedBrand = Brand.DELL;
        var actualNumber = microsystems.GetComputer(3).Number;
        var actualBrand = microsystems.GetComputer(3).Brand;

        //Assert

        Assert.AreEqual(expectedNumber, actualNumber);
        Assert.AreEqual(expectedBrand, actualBrand);

    }

        [Test]
    public void GetComputer_Should_Throw_Exception_With_Invalid_Number()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2300, 15.6, "grey");
        var computer3 = new Computer(4, Brand.DELL, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);

       
        //Assert

        Assert.Throws<ArgumentException>(() => microsystems.GetComputer(13));
       
    }

        [Test]
    public void GetAllWithColor_Should_Return_Correct_Count()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "blue");
        var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = 3;
        var actual = microsystems.GetAllWithColor("grey").Count();

        //Assert

        Assert.AreEqual(expected, actual);
    }

        [Test]
    public void GetAllWithColor_Should_Return_Correct_Order()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(7, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "blue");
        var computer3 = new Computer(6, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2500, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = new List<Computer>()
        {
            new Computer(6, Brand.DELL, 2800, 15.6, "grey"),
       new Computer(5, Brand.ACER, 2500, 15.6, "grey"),
       new Computer(7, Brand.DELL, 2300, 15.6, "grey")
    };
        var actual = microsystems.GetAllWithColor("grey").ToList();

        //Assert

        Assert.IsTrue(actual.SequenceEqual(expected));
    }

         [Test]
    public void GetAllWithColor_Should_Return_Empty_Collection()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
        var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = Enumerable.Empty<Computer>();
        var actual = microsystems.GetAllWithColor("blue");

        //Assert

        CollectionAssert.AreEqual(expected, actual);
    }

        [Test]
    public void GetInRangePrice_Should_Return_Correct_Count()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "blue");
        var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = 3;
        var actual = microsystems.GetInRangePrice(2200,2300).Count();

        //Assert

        Assert.AreEqual(expected, actual);
    }

        [Test]
    public void GetInRangePrice_Should_Return_Empty_Collection()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(1, Brand.DELL, 2300, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "grey");
        var computer3 = new Computer(4, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2300, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = Enumerable.Empty<Computer>();
        var actual = microsystems.GetInRangePrice(2100,2199);

        //Assert

        CollectionAssert.AreEqual(expected, actual);
    }

        [Test]
    public void GetInRangePrice_Should_Return_Correct_Order()
    {
        //Arrange

        var microsystems = new Microsystems();
        var computer = new Computer(7, Brand.DELL, 2400, 15.6, "grey");
        var computer2 = new Computer(3, Brand.DELL, 2200, 15.6, "blue");
        var computer3 = new Computer(6, Brand.DELL, 2800, 15.6, "grey");
        var computer4 = new Computer(5, Brand.ACER, 2500, 15.6, "grey");

        //Act
        microsystems.CreateComputer(computer);
        microsystems.CreateComputer(computer2);
        microsystems.CreateComputer(computer3);
        microsystems.CreateComputer(computer4);
        var expected = new List<Computer>()
        {
            new Computer(5, Brand.ACER, 2500, 15.6, "grey"),
            new Computer(7, Brand.DELL, 2400, 15.6, "grey"),
            new Computer(3, Brand.DELL, 2200, 15.6, "blue")

    };
        var actual = microsystems.GetInRangePrice(2200, 2500);

        //Assert

        CollectionAssert.AreEqual(expected, actual);
    }
    }
}