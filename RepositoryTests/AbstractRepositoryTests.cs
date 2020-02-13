using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Repository;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Tests
{
    [TestClass()]
    public class AbstractRepositoryTests
    {
        [TestMethod()]
        public void TestAllMethods()
        {
            AbstractRepository<Device> repo = new DeviceRepository();
            int count = repo.GetAll().Count;
            Console.WriteLine(count);
            repo.Insert(new Device { Id = -1, Name = "newPhone", Type = 1, Manufacturer = 1, OS = 1, Processor = 1, RAM = 2 });
            Assert.AreEqual(count + 1, repo.GetAll().Count);
            Device device = repo.GetAll().Where(x => x.Name == "newPhone").FirstOrDefault();
            repo.Update(device.Id, new Device { Id = -1, Name = "newPhone", Type = 1, Manufacturer = 1, OS = 1, Processor = 1, RAM = 3 });
            Assert.AreEqual(device.RAM, 3);
            repo.Delete(device.Id);
            Assert.AreEqual(count, repo.GetAll().Count);
        }
    }
}