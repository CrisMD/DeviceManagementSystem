using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Model;
using Repository.Implementations;
using Repository;

namespace Service
{
    public class WCFService : IWCFService
    {
        IRepository<Device> deviceRepository = new DeviceRepository();
        IRepository<Manufacturer> manufacturerRepository = new ManufacturerRepository();
        IRepository<DeviceType> deviceTypeRepository = new DeviceTypeRepository();
        IRepository<OS> OSRepository = new OperatingSystemRepository();
        IRepository<Processor> processorRepository = new ProcessorRepository();
        IRepository<Role> roleRepository = new RoleRepository();
        IRepository<Location> locationRepository = new LocationRepository();
        IRepository<User> userRepository = new UserRepository();
        IRepository<UserDevice> userDeviceRepository = new UserDeviceRepository();

        const int initialId = -1;

        public void AcceptOptions()
        {

        }

        #region Devices
        public List<Device> GetAllDevices()
        {
            return deviceRepository.GetAll();
        }

        public Device GetOneDevice(int id)
        {
            return deviceRepository.GetOne(id);
        }

        public bool InsertDevice(string name, int manufacturer, int type, int OS, int processor, int RAM)
        {
            return deviceRepository.Insert(new Device { Id = initialId, Name = name, Manufacturer = manufacturer, Type = type, OS = OS, Processor = processor, RAM = RAM });
        }

        public bool UpdateDevice(int id, string name, int manufacturer, int type, int OS, int processor, int RAM)
        {
            return deviceRepository.Update(id, new Device { Id = initialId, Name = name, Manufacturer = manufacturer, Type = type, OS = OS, Processor = processor, RAM = RAM });
        }

        public bool DeleteDevice(int id)
        {
            return deviceRepository.Delete(id);
        }
        #endregion

        #region Manufacturers       
        public List<Manufacturer> GetAllManufacturers()
        {
            return manufacturerRepository.GetAll();
        }
        #endregion

        #region DeviceTypes
        public List<DeviceType> GetAllTypes()
        {
            return deviceTypeRepository.GetAll();
        }
        #endregion

        #region OperatingSystems
        public List<OS> GetAllOS()
        {
            return OSRepository.GetAll();
        }
        #endregion

        #region Processors
        public List<Processor> GetAllProcessors()
        {
            return processorRepository.GetAll();
        }
        #endregion

        #region Roles
        public List<Role> GetAllRoles()
        {
            return roleRepository.GetAll();
        }
        #endregion

        #region Locations
        public List<Location> GetAllLocations()
        {
            return locationRepository.GetAll();
        }
        #endregion

        #region Users
        public User Login(string username, string password)
        {
            List<User> users = userRepository.GetAll();
            foreach(var user in users)
            {
                if(user.Username.Equals(username) && user.Password.Equals(password))
                {
                    return user;
                }
            }

            return null;
        }

        public User GetOneUser(int id)
        {
            return userRepository.GetOne(id);
        }

        public bool InsertUser(string username, string password, string name, int role, int location)
        {
            return userRepository.Insert(new User{ Id=initialId, Username=username, Password=password, Name=name, Role=role, Location=location});
        }
        #endregion

        #region UsersDevices
        public User GetUserForDevice(int id)
        {
            var assignments = userDeviceRepository.GetAll();
            foreach(var assignment in assignments) 
            {
                if (assignment.DeviceId.Equals(id) && assignment.EndTime == null)
                {
                    return userRepository.GetOne(assignment.UserId);
                }
            }

            return null;
        }

        public bool Assign(int device, int user)
        {
            return userDeviceRepository.Insert(new UserDevice { Id = initialId, DeviceId = device, UserId = user, StartTime = DateTime.Now, EndTime=null});
        }

        public bool Unassign(int device, int user)
        {
            var assignment = userDeviceRepository.GetAll().Where(x => x.DeviceId == device && x.UserId == user && x.EndTime == null).FirstOrDefault();
            return userDeviceRepository.Update(assignment.Id, new UserDevice { Id=initialId, DeviceId = assignment.DeviceId, UserId = assignment.UserId, StartTime = assignment.StartTime, EndTime = DateTime.Now });
        }
        #endregion
    }
}
