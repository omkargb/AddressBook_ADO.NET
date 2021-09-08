using AddressBookSystem;
using NUnit.Framework;

namespace AddressBookTest
{
    public class AddressBookTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenAddressBookDB_WhenRetrieved_ReturnsNumOfContacts()
        {
            ContactOperations cops = new ContactOperations();
            int result = cops.GetContactDetails();
            int expect = 7;
            Assert.AreEqual(result, expect);
        }
    }
}