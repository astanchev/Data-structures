namespace _02.VaniPlanning.Tests
{
    using NUnit.Framework;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tests
    {
        [Test]
        public void AddInvoice_Should_Increase_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));

            //Act

            agency.Create(invoice);

            //Assert

            Assert.AreEqual(1, agency.Count());
        }

        [Test]
        public void CreateInvoice_With_Existing_Number_Should_Throw_Exception()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));

            //Act

            agency.Create(invoice);

            //Assert

            Assert.Throws<ArgumentException>(() => agency.Create(invoice2));
        }

        [Test]
        public void Contains_Should_Return_True_With_Valid_Computer_Number()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);

            //Assert

            Assert.IsTrue(agency.Contains("first"));
        }

        [Test]
        public void Contains_Should_Return_False_With_Invalid_Number()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);

            //Assert

            Assert.IsFalse(agency.Contains("second"));
        }

        [Test]
        public void Count_Should_Work_Correct()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            var expectedCount = 3;
            var actualCount = agency.Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void PayInvoice_Should_SetSubtotal_To_Zero()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 20), new DateTime(2000, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            var expectedSubtotal = 0;
            agency.PayInvoice(new DateTime(2000, 10, 28));

            //Assert

            Assert.AreEqual(expectedSubtotal, invoice3.Subtotal);
        }

        [Test]
        public void PayInvoice_Should_Throw_Argument_Exception()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);

            //Assert

            Assert.Throws<ArgumentException>(() => agency.PayInvoice(new DateTime(2003, 10, 28)));
        }

        [Test]
        public void ThrowInvoice_Should_Decrease_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.ThrowInvoice("first");

            var expectedCount = 2;
            var actualCount = agency.Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ThrowInvoice_Agency_Should_Not_Contain_Deleted_Invoice()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.ThrowInvoice("first");



            //Assert

            Assert.IsFalse(agency.Contains("first"));
        }

        [Test]
        public void ThrowInvoice_Should_Throw_Exception_With_Invalid_Number()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);


            //Assert

            Assert.Throws<ArgumentException>(() => agency.ThrowInvoice("invalid"));
        }

        [Test]
        public void ThrowPayed_Should_Decrease_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2003, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 11, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.PayInvoice(new DateTime(2001, 10, 28));
            agency.PayInvoice(new DateTime(2001, 11, 28));
            agency.ThrowPayed();
            var expectedCount = 1;
            var actualCount = agency.Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ThrowPayed_Agency_Should_Not_Contain_Payed_Invoices()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2003, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 11, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.PayInvoice(new DateTime(2001, 10, 28));
            agency.PayInvoice(new DateTime(2001, 11, 28));
            agency.ThrowPayed();


            //Assert

            Assert.IsFalse(agency.Contains("second"));
            Assert.IsFalse(agency.Contains("third"));
        }

        [Test]
        public void GetAllInvoiceInPeriod_Should_Return_Correct_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("fourth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("fifth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expectedCount = 4;
            var actual = agency.GetAllInvoiceInPeriod(new DateTime(2000, 11, 28), new DateTime(2000, 12, 30));
            var actualCount = actual.Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GetAllInvoiceInPeriod_Should_Return_Correct_Order()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("fourth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("fifth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expected = new List<Invoice>()
        {
            new Invoice("fifth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28)),
            new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28)),
             new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28)),
             new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28))

        };
            var actual = agency.GetAllInvoiceInPeriod(new DateTime(2000, 11, 28), new DateTime(2000, 12, 30));


            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllInvoiceInPeriod_Should_Return_Empty_Collection_With_Invalid_Period()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("fourth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("fifth", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expected = Enumerable.Empty<Invoice>();
            var actual = agency.GetAllInvoiceInPeriod(new DateTime(2001, 11, 28), new DateTime(2003, 12, 30));


            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void SearchBySerialNumber_Should_Return_Corect_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expectedCount = 3;
            var actualCount = agency.SearchBySerialNumber("3").Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void SearchBySerialNumber_Should_Return_Correct_Order()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expected = new List<Invoice>()
        {
            invoice,
            invoice2,
            invoice4
        }.OrderByDescending(c => c.SerialNumber);
            var actual = agency.SearchBySerialNumber("3");

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void SearchBySerialNumber_Should_Throw_Exception_With_Inalid_Number()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2001, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 10, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            //Assert

            Assert.Throws<ArgumentException>(() => agency.SearchBySerialNumber("zssd"));
        }

        [Test]
        public void ThrowInvoiceInPeriod_Should_Decrease_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            agency.ThrowInvoiceInPeriod(new DateTime(2001, 09, 20), new DateTime(2001, 11, 27));

            var expectedCount = 3;
            var actualCount = agency.Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ThrowInvoiceInPeriod_Agency_Should_Not_Contain_Deleted()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            agency.ThrowInvoiceInPeriod(new DateTime(2001, 09, 20), new DateTime(2001, 11, 27));



            //Assert

            Assert.IsFalse(agency.Contains("444"));
            Assert.IsFalse(agency.Contains("test3"));
        }

        [Test]
        public void ThrowInvoiceInPeriod_Should_Return_Empty_Collection_With_Invalid_Period()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            //Assert

            Assert.Throws<ArgumentException>(() => agency.ThrowInvoiceInPeriod(new DateTime(2003, 09, 20), new DateTime(2004, 11, 27)));
        }

        [Test]
        public void GetAllFromDepartment_Should_Return_Correct_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expectedCount = 2;
            var actualCount = agency.GetAllFromDepartment(Department.Sells).Count();

            //Assert

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GetAllFromDepartment_Should_Return_Correct_Order()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expected = new List<Invoice>()
        {
            new Invoice("test3", "SoftUni", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28)),
            new Invoice("test", "SoftUni", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27))
        }.OrderByDescending(x => x.Subtotal).ThenBy(x => x.IssueDate);
            var actual = agency.GetAllFromDepartment(Department.Sells).ToList();

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllFromDepartment_Should_Return_Empty_Collection_With_NonExisting_Department()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "SoftUni", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "SoftUni", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expected = Enumerable.Empty<Invoice>();
            var actual = agency.GetAllFromDepartment(Department.Others);

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllByCompany_Should_Return_Correct_Count()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expected = 3;
            var actual = agency.GetAllByCompany("SoftUni").Count();

            //Assert

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllByCompany_Should_Return_Correct_Order()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expected = new List<Invoice>()
        {
            new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28)),
            new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28)),
            new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28))

        }.OrderByDescending(x => x.SerialNumber);
            var actual = agency.GetAllByCompany("SoftUni");

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void GetAllByCompany_Should_Return_Empty_Collection_With_Non_Existing_Company()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));
            var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 11, 28), new DateTime(2001, 11, 27));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            var expected = Enumerable.Empty<Invoice>();
            var actual = agency.GetAllByCompany("Invalid");

            //Assert

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [Test]
        public void ExtendDeadline_Should_Work_Correct()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 11, 20));
            var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 05, 28), new DateTime(2001, 11, 20));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);
            var expectedDate = new DateTime(2001, 11, 25);
            agency.ExtendDeadline(new DateTime(2001, 11, 20), 5);

            Assert.AreEqual(expectedDate, invoice4.DueDate);
            Assert.AreEqual(expectedDate, invoice5.DueDate);
        }

        [Test]
        public void ExtendDeadline_Should_Return_Empty_Collection_With_Invalid_Date()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("123", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2000, 10, 28));
            var invoice2 = new Invoice("435", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 29), new DateTime(2000, 10, 28));
            var invoice3 = new Invoice("444", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 30), new DateTime(2001, 09, 28));
            var invoice4 = new Invoice("test3", "VMWare", 1200, Department.Sells, new DateTime(2000, 10, 28), new DateTime(2001, 11, 20));
            var invoice5 = new Invoice("test", "Musala", 1200, Department.Sells, new DateTime(2000, 05, 28), new DateTime(2001, 11, 20));


            //Act

            agency.Create(invoice);
            agency.Create(invoice2);
            agency.Create(invoice3);
            agency.Create(invoice4);
            agency.Create(invoice5);

            //Assert

            Assert.Throws<ArgumentException>(() => agency.ExtendDeadline(new DateTime(2004, 11, 20), 5));
        }

        [Test]
        public void AddInvoice_Agency_Should_Contain_New_Invoice()
        {
            //Arrange

            var agency = new Agency();
            var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));

            //Act

            agency.Create(invoice);

            //Assert

            Assert.IsTrue(agency.Contains("first"));
        }

    }
}